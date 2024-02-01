import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatSidenavModule } from '@angular/material/sidenav';

import { HomeRoutingModule } from './home-routing.module';
import { HomeComponent } from './home-page/home.component';
import { TestComponent } from './test-page/test.component';

@NgModule({
    declarations: [HomeComponent, TestComponent],
    imports: [CommonModule, HomeRoutingModule, MatSidenavModule],
})
export class HomeModule {}
