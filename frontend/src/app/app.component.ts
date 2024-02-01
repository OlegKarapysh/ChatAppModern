import { Component, inject } from '@angular/core';
import { SpinnerService } from './services/spinner.service';

@Component({
    selector: 'chat-root',
    templateUrl: './app.component.html',
    styleUrl: './app.component.scss',
})
export class AppComponent {
    public readonly spinner: SpinnerService = inject(SpinnerService);
}
