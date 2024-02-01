import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotFoundComponent } from './modules/not-found/not-found.component';

const routes: Routes = [
    {
        path: 'home',
        loadChildren: () =>
            import('./modules/home/home.module').then((m) => m.HomeModule),
    },
    {
        path: 'auth',
        loadChildren: () =>
            import('./modules/authentication/authentication.module').then(
                (m) => m.AuthenticationModule
            ),
    },
    { path: '', pathMatch: 'full', redirectTo: 'home' },
    { path: '**', component: NotFoundComponent },
];

@NgModule({
    imports: [
        RouterModule.forRoot(routes, {
            //enableTracing: true,
        }),
    ],
    exports: [RouterModule],
})
export class AppRoutingModule {}
