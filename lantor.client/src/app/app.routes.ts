import { Routes } from '@angular/router'
import { CustomDetectorComponent } from './custom-detector/custom-detector.component';
import { LanguageAdminComponent } from './language-admin/language-admin.component';
import { LanguageDetectorComponent } from './language-detector/language-detector.component';

export const routes: Routes = [
  { path: 'default-detector', component: LanguageDetectorComponent },
  { path: 'custom-detector', component: CustomDetectorComponent },
  { path: 'language-admin', component: LanguageAdminComponent },
  { path: '', redirectTo: '/default-detector', pathMatch: 'full' }
];
