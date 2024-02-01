import {
    HttpErrorResponse,
    HttpHandlerFn,
    HttpInterceptorFn,
    HttpRequest,
} from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, switchMap, throwError } from 'rxjs';
import { AuthService } from '../services/auth.service';

export const authErrorInterceptor: HttpInterceptorFn = (
    request: HttpRequest<unknown>,
    next: HttpHandlerFn
) => {
    const unauthorizedErrorStatusCode = 401;
    const forbiddenErrorStatusCode = 403;
    const authService = inject(AuthService);

    return next(request).pipe(
        catchError((error: HttpErrorResponse) => {
            if (
                error.status === unauthorizedErrorStatusCode &&
                error.headers.has('Token-Expired')
            ) {
                return authService
                    .refreshTokens()
                    .pipe(switchMap(() => next(request)));
            }

            if (error.status === forbiddenErrorStatusCode) {
                authService.logout();
            }

            return throwError(() => error);
        })
    );
};
