import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { Router } from '@angular/router';
import { LoginDto } from '../models/auth/login-dto';
import { UserAuthTokensDto } from '../models/auth/user-auth-tokens-dto';
import { Observable, tap } from 'rxjs';

@Injectable({
    providedIn: 'root',
})
export class AuthService {
    public currentUserId: string | null = null;
    private readonly baseUrl = '/auth';
    private readonly accessTokenKey = 'accessToken';
    private readonly refreshTokenKey = 'refreshToken';

    constructor(
        private readonly httpService: HttpService,
        private readonly router: Router
    ) {}

    public get isAuthenticated(): boolean {
        return this.currentUserId !== null;
    }

    public login(loginDto: LoginDto): Observable<UserAuthTokensDto> {
        return this.httpService
            .post<UserAuthTokensDto>(`${this.baseUrl}/login`, loginDto)
            .pipe(tap(this.handleAuthResponse.bind(this)));
    }

    private handleAuthResponse(userTokens: UserAuthTokensDto) {
        if (
            userTokens &&
            userTokens.userId &&
            userTokens.accessToken &&
            userTokens.refreshToken
        ) {
            this.saveTokens(userTokens.accessToken, userTokens.refreshToken);
            this.currentUserId = userTokens.userId;
        } else {
            console.log('error while handling auth response:');
            console.log(userTokens);
        }
    }

    private saveTokens(accessToken: string, refreshToken: string) {
        localStorage.setItem(this.accessTokenKey, accessToken);
        localStorage.setItem(this.refreshTokenKey, refreshToken);
    }
}
