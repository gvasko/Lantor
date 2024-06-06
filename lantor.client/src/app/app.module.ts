import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { LanguageDetectorComponent } from './language-detector/language-detector.component';
import { NavbarComponent } from './navbar/navbar.component';
import { NgbDropdownModule, NgbModalModule, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SpinnerComponent } from './spinner/spinner.component';
import { LoadingInterceptor } from './services/loading.interceptor';
import { CustomDetectorComponent } from './custom-detector/custom-detector.component';
import { LanguageAdminComponent } from './language-admin/language-admin.component';

import { routes } from './app.routes';
import { provideRouter, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { DefaultDetectorComponent } from './default-detector/default-detector.component';
import { ConfirmationComponent } from './confirmation/confirmation.component';
import { LanguageSampleCollectionComponent } from './language-sample-collection/language-sample-collection.component';
import { LanguageSampleComponent } from './language-sample/language-sample.component';
import { CreateAlphabetComponent } from './create-alphabet/create-alphabet.component';
import { MsalBroadcastService, MsalGuard, MsalGuardConfiguration, MsalRedirectComponent, MsalService, MSAL_GUARD_CONFIG, MSAL_INSTANCE } from '@azure/msal-angular';
import { InteractionType, IPublicClientApplication, PublicClientApplication } from '@azure/msal-browser';
import { msalConfig } from './auth-config';

export function MSALInstanceFactory(): IPublicClientApplication {
  return new PublicClientApplication(msalConfig);
}

export function MsalGuardConfigurationFactory(): MsalGuardConfiguration {
  return {
    interactionType: InteractionType.Redirect,
    authRequest: {
      scopes: ["user.read"]
    },
    loginFailedRoute: ''
  };
}

@NgModule({
  declarations: [
    AppComponent,
    LanguageDetectorComponent,
    NavbarComponent,
    SpinnerComponent,
    CustomDetectorComponent,
    LanguageAdminComponent,
    DefaultDetectorComponent,
    ConfirmationComponent,
    LanguageSampleCollectionComponent,
    LanguageSampleComponent,
    CreateAlphabetComponent
  ],
  imports: [
    BrowserModule, HttpClientModule, FormsModule, RouterOutlet, RouterLink, RouterLinkActive,
    NgbModule, NgbDropdownModule, NgbModalModule, ReactiveFormsModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true
    },
    {
      provide: MSAL_INSTANCE,
      useFactory: MSALInstanceFactory
    },
    {
      provide: MSAL_GUARD_CONFIG,
      useFactory: MsalGuardConfigurationFactory
    },
    MsalService, MsalBroadcastService, MsalGuard,
    provideRouter(routes)
  ],
  bootstrap: [AppComponent, MsalRedirectComponent]
})
export class AppModule { }
