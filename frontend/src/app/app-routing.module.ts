import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotFoundComponent } from './modules/not-found/not-found.component';

const routes: Routes = [
    {
        path: 'home',
        loadChildren: () =>
            import('./modules/home/home.module').then((m) => m.HomeModule),
    },
    { path: '', pathMatch: 'full', redirectTo: 'home' },
    { path: '**', component: NotFoundComponent },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
})
export class AppRoutingModule {}
