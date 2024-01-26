import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home-page/home.component';
import { TestComponent } from './test-page/test.component';
import { NotFoundComponent } from '../not-found/not-found.component';

const routes: Routes = [
    { path: 'test', title: 'Test', component: TestComponent },
    { path: '', title: 'Home', pathMatch: 'full', component: HomeComponent },
    { path: '**', component: NotFoundComponent },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class HomeRoutingModule {}
