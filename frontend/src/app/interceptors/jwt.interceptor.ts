import {
    HttpHandlerFn,
    HttpInterceptorFn,
    HttpRequest,
} from '@angular/common/http';
import { AuthService } from '../services/auth.service';
import { inject } from '@angular/core';

export const jwtInterceptor: HttpInterceptorFn = (
    request: HttpRequest<unknown>,
    next: HttpHandlerFn
) => {
    const authService = inject(AuthService);
    const accessToken = authService.getAccessToken();
    if (accessToken) {
        const clonedRequest = request.clone({
            setHeaders: { Authorization: `Bearer ${accessToken}` },
        });

        return next(clonedRequest);
    }

    return next(request);
};
