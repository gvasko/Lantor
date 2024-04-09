import { Routes } from '@angular/router'
import { CustomDetectorComponent } from './custom-detector/custom-detector.component';
import { LanguageAdminComponent } from './language-admin/language-admin.component';
import { DefaultDetectorComponent } from './default-detector/default-detector.component';

export const routes: Routes = [
  { path: 'default-detector', component: DefaultDetectorComponent },
  { path: 'custom-detector', component: CustomDetectorComponent },
  { path: 'language-admin', component: LanguageAdminComponent },
  { path: '', redirectTo: '/default-detector', pathMatch: 'full' }
];
