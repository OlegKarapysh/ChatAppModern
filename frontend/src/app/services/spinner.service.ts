import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
    providedIn: 'root',
})
export class SpinnerService {
    private readonly isSpinnerShown$ = new BehaviorSubject(false);

    public get isShown(): Observable<boolean> {
        return this.isSpinnerShown$.asObservable();
    }

    public show(): void {
        this.isSpinnerShown$.next(true);
    }

    public hide(): void {
        this.isSpinnerShown$.next(false);
    }
}
