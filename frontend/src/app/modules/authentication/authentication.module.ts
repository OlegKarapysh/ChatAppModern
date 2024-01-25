import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthenticationRoutingModule } from './authentication-routing.module';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AuthLayoutComponent } from './auth-layout/auth-layout.component';

@NgModule({
    declarations: [LoginComponent, RegisterComponent, AuthLayoutComponent],
    imports: [CommonModule, AuthenticationRoutingModule],
})
export class AuthenticationModule {}
