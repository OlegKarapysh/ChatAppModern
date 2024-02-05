import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatButtonModule } from '@angular/material/button';
import { CdkMenuModule } from '@angular/cdk/menu';
import { HomeRoutingModule } from './home-routing.module';
import { HomeComponent } from './home-page/home.component';
import { TestComponent } from './test-page/test.component';
import { NavigationMenuComponent } from './home-page/navigation-menu/navigation-menu.component';
import { AppHeaderComponent } from './home-page/app-header/app-header.component';

@NgModule({
    declarations: [HomeComponent, TestComponent, NavigationMenuComponent, AppHeaderComponent],
    imports: [
        CommonModule,
        HomeRoutingModule,
        MatSidenavModule,
        MatToolbarModule,
        MatIconModule,
        MatListModule,
        MatButtonModule,
        CdkMenuModule,
    ],
})
export class HomeModule {}
