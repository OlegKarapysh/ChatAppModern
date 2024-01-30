import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root',
})
export class HttpService {
    public baseUrl: string = environment.apiUrl;

    public headers = new HttpHeaders();

    constructor(private httpClient: HttpClient) {}

    public get<T>(url: string, httpParams?: any): Observable<T> {
        return this.httpClient.get<T>(this.buildUrl(url), {
            headers: this.headers,
            params: httpParams,
        });
    }

    public getFull<T>(
        url: string,
        httpParams?: any
    ): Observable<HttpResponse<T>> {
        return this.httpClient.get<T>(this.buildUrl(url), {
            observe: 'response',
            headers: this.headers,
            params: httpParams,
        });
    }

    public post<T>(url: string, payload: object): Observable<T> {
        return this.httpClient.post<T>(this.buildUrl(url), payload, {
            headers: this.headers,
        });
    }

    public postFull<T>(
        url: string,
        payload: object
    ): Observable<HttpResponse<T>> {
        return this.httpClient.post<T>(this.buildUrl(url), payload, {
            headers: this.headers,
            observe: 'response',
        });
    }

    public put<T>(url: string, payload: object): Observable<T> {
        return this.httpClient.put<T>(this.buildUrl(url), payload, {
            headers: this.headers,
        });
    }

    public putFull<T>(
        url: string,
        payload: object
    ): Observable<HttpResponse<T>> {
        return this.httpClient.put<T>(this.buildUrl(url), payload, {
            headers: this.headers,
            observe: 'response',
        });
    }

    public delete<T>(url: string, httpParams?: any): Observable<T> {
        return this.httpClient.delete<T>(this.buildUrl(url), {
            headers: this.headers,
            params: httpParams,
        });
    }

    public deleteFull<T>(
        url: string,
        httpParams?: any
    ): Observable<HttpResponse<T>> {
        return this.httpClient.delete<T>(this.buildUrl(url), {
            headers: this.headers,
            observe: 'response',
            params: httpParams,
        });
    }

    public buildUrl(url: string): string {
        if (url.startsWith('http://') || url.startsWith('https://')) {
            return url;
        }

        return this.baseUrl + url;
    }
}
