import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
    selector: 'chat-auth-layout',
    templateUrl: './auth-layout.component.html',
    styleUrl: './auth-layout.component.scss',
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AuthLayoutComponent {}
