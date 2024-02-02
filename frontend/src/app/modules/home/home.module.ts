import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { HomeRoutingModule } from './home-routing.module';
import { HomeComponent } from './home-page/home.component';
import { TestComponent } from './test-page/test.component';
import { NavigationMenuComponent } from './home-page/navigation-menu/navigation-menu.component';

@NgModule({
    declarations: [HomeComponent, TestComponent, NavigationMenuComponent],
    imports: [
        CommonModule,
        HomeRoutingModule,
        MatSidenavModule,
        MatToolbarModule,
        MatIconModule,
        MatListModule,
    ],
})
export class HomeModule {}
