import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotFoundComponent } from './core/not-found/not-found.component';
import { ServerErrorComponent } from './core/server-error/server-error.component';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
  { path: '', component: HomeComponent, data: { breadcrumb: 'Home' } },
  { path: 'server-error', component: ServerErrorComponent, data: { breadcrumb: 'Server Error' }},
  { path: 'not-found', component: NotFoundComponent, data: { breadcrumb: 'Not Found' } },
  // lazy loading: the shop module is loaded only when the /shop path is accessed
  { path: 'shop', loadChildren: () => import('./shop/shop.module').then(mod => mod.ShopModule), data: { breadcrumb: 'Shop' } },
  { path: '**', redirectTo: 'not-found', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
