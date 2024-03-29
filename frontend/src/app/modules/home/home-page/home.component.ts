import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
    selector: 'chat-home',
    templateUrl: './home.component.html',
    styleUrl: './home.component.scss',
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HomeComponent {}
