import { Component, OnDestroy, OnInit } from '@angular/core';
import { Event, NavigationEnd, Router, RouterEvent } from '@angular/router';
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

  constructor(private msalService: MsalService, private msalBroadcastService: MsalBroadcastService, private router: Router) {

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

    this.router.events.pipe(
      filter((e: Event): e is NavigationEnd => e instanceof NavigationEnd),
      takeUntil(this.unsubscribe)
    ).subscribe((e: NavigationEnd) => {
      this.setAuthStatus();
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
    //this.msalService.instance.logoutRedirect();
    // soft-logout
    const keysToDelete = [];
    for (let i = 0; i < localStorage.length; i++) {
      const key: string | null = localStorage.key(i);

      if (!key) {
        continue;
      }

      if (key.indexOf("msal.") >= 0 || key.indexOf("login.windows.net") >= 0) {
        keysToDelete.push(key);
      }
    }

    keysToDelete.forEach((key) => localStorage.removeItem(key));

    this.router.navigate(["/"]);

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
