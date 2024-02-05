import { ChangeDetectionStrategy, Component } from '@angular/core';
import { AuthService } from '../../../services/auth.service';

@Component({
    selector: 'chat-home',
    templateUrl: './home.component.html',
    styleUrl: './home.component.scss',
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HomeComponent {
    constructor(private readonly authService: AuthService) {}

    public isAuthenticated() {
        return this.authService.checkAuthenticated();
    }
}
