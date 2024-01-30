import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { Router } from '@angular/router';
import { LoginDto } from '../models/auth/login-dto';
import { UserAuthTokensDto } from '../models/auth/user-auth-tokens-dto';
import { Observable, tap } from 'rxjs';
import { RegisterDto } from '../models/auth/register-dto';

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

    public register(registerDto: RegisterDto): Observable<UserAuthTokensDto> {
        return this.httpService
            .post<UserAuthTokensDto>(`${this.baseUrl}/register`, registerDto)
            .pipe(tap(this.handleAuthResponse.bind(this)));
    }

    public logout(): void {
        this.removeTokens();
        this.currentUserId = null;
        this.router.navigateByUrl(`${this.baseUrl}/login`);
    }

    private handleAuthResponse(userTokens: UserAuthTokensDto): void {
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

    private saveTokens(accessToken: string, refreshToken: string): void {
        localStorage.setItem(this.accessTokenKey, accessToken);
        localStorage.setItem(this.refreshTokenKey, refreshToken);
    }

    private removeTokens(): void {
        localStorage.removeItem(this.accessTokenKey);
        localStorage.removeItem(this.refreshTokenKey);
    }
}
