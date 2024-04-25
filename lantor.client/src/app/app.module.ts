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
    provideRouter(routes)
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
