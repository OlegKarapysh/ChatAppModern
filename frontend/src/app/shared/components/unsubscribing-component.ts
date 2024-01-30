import { Component, DestroyRef, inject } from '@angular/core';

@Component({
    template: '',
})
export class UnsubscribingComponent {
    protected destroyRef = inject(DestroyRef);
}
