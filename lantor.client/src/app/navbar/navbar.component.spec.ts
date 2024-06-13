import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MsalBroadcastService, MsalService } from '@azure/msal-angular';
import { InteractionStatus, IPublicClientApplication } from '@azure/msal-browser';
import { NgbCollapse } from '@ng-bootstrap/ng-bootstrap';
import { of } from 'rxjs';

import { NavbarComponent } from './navbar.component';

describe('NavbarComponent', () => {
  let component: NavbarComponent;
  let fixture: ComponentFixture<NavbarComponent>;

  beforeEach(async () => {
    const msalServiceDummy = jasmine.createSpyObj<MsalService>('MsalService', ['loginRedirect']);
    msalServiceDummy.instance = jasmine.createSpyObj<IPublicClientApplication>('instance', ['setActiveAccount', 'loginRedirect', 'getActiveAccount', 'getAllAccounts', 'setActiveAccount']);

    const msalBroadcastServiceDummy = jasmine.createSpyObj<MsalBroadcastService>('MsalBroadcastService', ['inProgress$', 'msalSubject$']);
    msalBroadcastServiceDummy.inProgress$ = of(InteractionStatus.None);
    msalBroadcastServiceDummy.msalSubject$ = of();

    await TestBed.configureTestingModule({
      declarations: [NavbarComponent],
      imports: [NgbCollapse],
      providers: [
        { provide: MsalService, useValue: msalServiceDummy },
        { provide: MsalBroadcastService, useValue: msalBroadcastServiceDummy }]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NavbarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
