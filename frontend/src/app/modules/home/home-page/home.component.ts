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
    isAuth() {
        return this.authService.checkAuthenticated();
    }
    getUserId() {
        return this.authService.getCurrentUserId();
    }
    logout() {
        this.authService.logout();
    }
}
