import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
    providedIn: 'root',
})
export class LoadingService {
    private readonly isLoadingSubject$ = new BehaviorSubject(false);

    public get isLoading$(): Observable<boolean> {
        return this.isLoadingSubject$.asObservable();
    }

    public startLoading() {
        this.isLoadingSubject$.next(true);
    }

    public finishLoading() {
        this.isLoadingSubject$.next(false);
    }
}
