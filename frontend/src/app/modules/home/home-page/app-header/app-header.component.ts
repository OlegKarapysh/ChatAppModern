import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { AuthService } from '../../../../services/auth.service';
import { MatSidenav } from '@angular/material/sidenav';

@Component({
    selector: 'chat-app-header',
    templateUrl: './app-header.component.html',
    styleUrl: './app-header.component.scss',
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AppHeaderComponent {
    @Input() sidenav: MatSidenav | undefined;

    constructor(private readonly authService: AuthService) {}

    public isAuthenticated() {
        return this.authService.checkAuthenticated();
    }

    public logout() {
        this.authService.logout();
    }

    getUserId() {
        return this.authService.getCurrentUserId();
    }
}
