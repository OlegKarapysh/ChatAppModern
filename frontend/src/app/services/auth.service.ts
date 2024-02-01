import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { Router } from '@angular/router';
import { LoginDto } from '../models/auth/login-dto';
import { UserAuthTokensDto } from '../models/auth/user-auth-tokens-dto';
import { Observable, shareReplay, tap } from 'rxjs';
import { RegisterDto } from '../models/auth/register-dto';
import { JwtPayload, jwtDecode } from 'jwt-decode';
import { JwtAuthPayload } from '../models/auth/jwt-auth-payload';

@Injectable({
    providedIn: 'root',
})
export class AuthService {
    private readonly baseUrl = '/auth';
    private readonly accessTokenKey = 'accessToken';
    private readonly refreshTokenKey = 'refreshToken';

    constructor(
        private readonly httpService: HttpService,
        private readonly router: Router
    ) {}

    public getCurrentUserId(): string | null {
        const accessToken = this.getAccessToken();
        if (!accessToken) {
            return null;
        }

        const jwt = jwtDecode(accessToken);
        if (!this.checkJwtNotExpired(jwt)) {
            this.logout();
            return null;
        }

        return (jwt as JwtAuthPayload).id;
    }

    public checkAuthenticated(): boolean {
        const accessToken = this.getAccessToken();
        if (!accessToken || !this.getRefreshToken()) {
            return false;
        }

        return this.checkJwtNotExpired(jwtDecode(accessToken));
    }

    public login(loginDto: LoginDto): Observable<UserAuthTokensDto> {
        return this.httpService
            .post<UserAuthTokensDto>(`${this.baseUrl}/login`, loginDto)
            .pipe(shareReplay(), tap(this.handleAuthResponse.bind(this)));
    }

    public register(registerDto: RegisterDto): Observable<UserAuthTokensDto> {
        return this.httpService
            .post<UserAuthTokensDto>(`${this.baseUrl}/register`, registerDto)
            .pipe(shareReplay(), tap(this.handleAuthResponse.bind(this)));
    }

    public logout(): void {
        this.removeTokens();
        this.router.navigateByUrl(`${this.baseUrl}/login`);
    }

    public getAccessToken(): string | null {
        return localStorage.getItem(this.accessTokenKey);
    }

    private getRefreshToken(): string | null {
        return localStorage.getItem(this.refreshTokenKey);
    }

    private handleAuthResponse(userTokens: UserAuthTokensDto): void {
        if (
            userTokens &&
            userTokens.userId &&
            userTokens.accessToken &&
            userTokens.refreshToken
        ) {
            this.saveTokens(userTokens.accessToken, userTokens.refreshToken);
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

    private checkJwtNotExpired(jwt: JwtPayload): boolean {
        if (!jwt.exp) {
            return false;
        }
        const milliSecondsInSecond = 1000;
        const jwtExpMilliSeconds = jwt.exp * milliSecondsInSecond;
        const utcNowMilliSeconds = Date.parse(new Date().toUTCString());

        return jwtExpMilliSeconds > utcNowMilliSeconds;
    }
}
