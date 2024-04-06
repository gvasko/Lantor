import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { LanguageDetectorComponent } from './language-detector/language-detector.component';
import { NavbarComponent } from './navbar/navbar.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SpinnerComponent } from './spinner/spinner.component';
import { LoadingInterceptor } from './services/loading.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    LanguageDetectorComponent,
    NavbarComponent,
    SpinnerComponent
  ],
  imports: [
    BrowserModule, HttpClientModule, FormsModule, NgbModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
