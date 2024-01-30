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
    public getUserId() {
        return this.authService.currentUserId;
    }
    public logout() {
        this.authService.logout();
    }
}
