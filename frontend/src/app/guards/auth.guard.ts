import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

export const authGuard: CanActivateFn = (route, state) => {
    const authService = inject(AuthService);
    const canActivate = authService.checkAuthenticated();
    if (!canActivate) {
        authService.logout();
    }

    return canActivate;
};