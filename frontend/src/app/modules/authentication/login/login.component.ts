import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
    selector: 'chat-login',
    templateUrl: './login.component.html',
    styleUrl: './login.component.scss',
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LoginComponent {}
