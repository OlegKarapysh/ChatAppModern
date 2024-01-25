import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
    selector: 'chat-register',
    templateUrl: './register.component.html',
    styleUrl: './register.component.scss',
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RegisterComponent {}
