import { Component, OnDestroy, OnInit } from '@angular/core';
import { MsalBroadcastService, MsalService } from '@azure/msal-angular';
import { AuthenticationResult, EventMessage, EventType, InteractionStatus } from '@azure/msal-browser';
import { filter, Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'lantor-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit, OnDestroy {
  isCollapsed: boolean = true;
  isAuthenticated: boolean = false;
  userName: string | null | undefined = "";

  private unsubscribe = new Subject<void>();

  constructor(private msalService: MsalService, private msalBroadcastService: MsalBroadcastService) {

  }

  ngOnInit() {
    this.msalBroadcastService.inProgress$.pipe(
      filter((status: InteractionStatus) => status === InteractionStatus.None),
      takeUntil(this.unsubscribe)
    ).subscribe(() => {
      this.setAuthStatus();
    });

    this.msalBroadcastService.msalSubject$.pipe(
      filter((message: EventMessage) => message.eventType === EventType.LOGIN_SUCCESS),
      takeUntil(this.unsubscribe)
    ).subscribe((message: EventMessage) => {
      const authResult = message.payload as AuthenticationResult;
      this.msalService.instance.setActiveAccount(authResult.account);
    });
  }

  ngOnDestroy() {
    this.unsubscribe.next();
    this.unsubscribe.complete();
  }

  login(): void {
    this.msalService.instance.loginRedirect({
      scopes: ["User.Read"]
    });
  }

  logout(): void {
    this.msalService.instance.logoutRedirect();
  }

  setAuthStatus() {
    let activeAccount = this.msalService.instance.getActiveAccount();

    if (!activeAccount && this.msalService.instance.getAllAccounts().length > 0) {
      activeAccount = this.msalService.instance.getAllAccounts()[0];
      this.msalService.instance.setActiveAccount(activeAccount);
    }

    this.isAuthenticated = !!activeAccount;
    this.userName = activeAccount?.username;
  }
}
