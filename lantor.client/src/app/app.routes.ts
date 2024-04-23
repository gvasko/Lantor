import { Routes } from '@angular/router'
import { CustomDetectorComponent } from './custom-detector/custom-detector.component';
import { LanguageAdminComponent } from './language-admin/language-admin.component';
import { DefaultDetectorComponent } from './default-detector/default-detector.component';
import { LanguageSampleCollectionComponent } from './language-sample-collection/language-sample-collection.component';
import { LanguageSampleComponent } from './language-sample/language-sample.component';

export const routes: Routes = [
  { path: 'default-detector', component: DefaultDetectorComponent },
  { path: 'custom-detector', component: CustomDetectorComponent },
  { path: 'language-admin', component: LanguageAdminComponent },
  { path: 'language-sample-collection', component: LanguageSampleCollectionComponent },
  { path: 'language-sample-collection/:id', component: LanguageSampleCollectionComponent },
  { path: 'language-sample-collection/:id/language/:languageId', component: LanguageSampleComponent },
  { path: '', redirectTo: '/default-detector', pathMatch: 'full' }
];
