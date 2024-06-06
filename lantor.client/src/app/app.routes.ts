import { Routes } from '@angular/router'
import { CustomDetectorComponent } from './custom-detector/custom-detector.component';
import { LanguageAdminComponent } from './language-admin/language-admin.component';
import { DefaultDetectorComponent } from './default-detector/default-detector.component';
import { LanguageSampleCollectionComponent } from './language-sample-collection/language-sample-collection.component';
import { LanguageSampleComponent } from './language-sample/language-sample.component';
import { MsalGuard, MsalRedirectComponent } from '@azure/msal-angular';

export const routes: Routes = [
  { path: 'default-detector', component: DefaultDetectorComponent },
  { path: 'custom-detector', component: CustomDetectorComponent, canActivate: [MsalGuard] },
  { path: 'language-admin', component: LanguageAdminComponent, canActivate: [MsalGuard] },
  { path: 'language-sample-collection', component: LanguageSampleCollectionComponent, canActivate: [MsalGuard] },
  { path: 'language-sample-collection/:id', component: LanguageSampleCollectionComponent, canActivate: [MsalGuard] },
  { path: 'language-sample-collection/:id/language', component: LanguageSampleComponent, canActivate: [MsalGuard] },
  { path: 'language-sample-collection/:id/language/:languageId', component: LanguageSampleComponent, canActivate: [MsalGuard] },
  { path: 'auth', component: MsalRedirectComponent },
  { path: '', redirectTo: '/default-detector', pathMatch: 'full' }
];
