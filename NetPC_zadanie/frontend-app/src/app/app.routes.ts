import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ContactListComponent } from './contact-list/contact-list.component';
import { LoginComponent } from './login/login.component';

export const routes: Routes = [
  { path: '', loadComponent: () => import('./home/home.component').then(m => m.HomeComponent) },
  { path: 'contacts', loadComponent: () => import('./contact-list/contact-list.component').then(m => m.ContactListComponent) },
  { path: 'login', loadComponent: () => import('./login/login.component').then(m => m.LoginComponent) },
  { path: 'register', loadComponent: () => import('./register/register.component').then(m => m.RegisterComponent) },
  { path: 'edit/:id', loadComponent: () => import('./edit/edit.component').then(m => m.EditComponent) },
  { path: 'add', loadComponent: () => import('./add/add.component').then(m => m.AddComponent) },
];

