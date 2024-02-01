import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { Router } from '@angular/router';
import { LoginDto } from '../models/auth/login-dto';
import { UserAuthTokensDto } from '../models/auth/user-auth-tokens-dto';
import { Observable, catchError, map, of, shareReplay, tap } from 'rxjs';
import { RegisterDto } from '../models/auth/register-dto';
import { JwtPayload, jwtDecode } from 'jwt-decode';
import { JwtAuthPayload } from '../models/auth/jwt-auth-payload';
import { AuthTokensDto } from '../models/auth/auth-tokens-dto';

@Injectable({
    providedIn: 'root',
})
export class AuthService {
    private readonly authPath = '/auth';
    private readonly accessTokenKey = 'accessToken';
    private readonly refreshTokenKey = 'refreshToken';

    constructor(
        private readonly httpService: HttpService,
        private readonly router: Router
    ) {}

    public getCurrentUserId(): string | null {
        const accessToken = this.getAccessToken();
        return !accessToken
            ? null
            : (jwtDecode(accessToken) as JwtAuthPayload).id;
    }

    public checkAuthenticated(): Observable<boolean> {
        const accessToken = this.getAccessToken();
        if (!accessToken || !this.getRefreshToken()) {
            return of(false);
        }

        if (this.checkJwtNotExpired(jwtDecode(accessToken))) {
            return of(true);
        }

        return this.refreshTokens().pipe(
            catchError(() =>
                of({
                    userId: '',
                    accessToken: '',
                    refreshToken: '',
                })
            ),
            map(
                (tokens: UserAuthTokensDto) =>
                    !!(tokens && tokens.accessToken && tokens.refreshToken)
            )
        );
    }

    public login(loginDto: LoginDto): Observable<UserAuthTokensDto> {
        return this.httpService
            .post<UserAuthTokensDto>(`${this.authPath}/login`, loginDto)
            .pipe(shareReplay(), tap(this.handleAuthResponse.bind(this)));
    }

    public register(registerDto: RegisterDto): Observable<UserAuthTokensDto> {
        return this.httpService
            .post<UserAuthTokensDto>(`${this.authPath}/register`, registerDto)
            .pipe(shareReplay(), tap(this.handleAuthResponse.bind(this)));
    }

    public logout(): void {
        this.removeTokens();
        this.router.navigateByUrl(`${this.authPath}/login`);
    }

    public refreshTokens(): Observable<UserAuthTokensDto> {
        const tokensDto: AuthTokensDto = {
            accessToken: this.getAccessToken() ?? '',
            refreshToken: this.getRefreshToken() ?? '',
        };

        return this.httpService
            .post<UserAuthTokensDto>(`${this.authPath}/refresh`, tokensDto)
            .pipe(tap(this.handleAuthResponse.bind(this)));
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
