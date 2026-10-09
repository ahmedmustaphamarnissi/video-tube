import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./views/home/home.component').then(m => m.HomeComponent),
  },
  {
    path: 'categories/:id',
    loadComponent: () =>
      import('./views/categories/categories.component').then(m => m.CategoriesComponent),
  },
  {
    path: 'videos/:id',
    loadComponent: () =>
      import('./views/video-details/video-details.component').then(m => m.VideoDetailsComponent),
  },
  {
    path: '**',
    redirectTo: '',
  },
];
