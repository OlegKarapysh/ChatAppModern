import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { catchError, tap } from 'rxjs';

export const authGuard: CanActivateFn = (route, state) => {
    const authService = inject(AuthService);
    return authService.checkAuthenticated().pipe(
        tap((canActivate: boolean) => {
            if (!canActivate) {
                authService.logout();
            }
        }),
        catchError((error) => {
            console.log('Error in authGuard: ');
            console.log(error);
            throw error;
        })
    );
};
