import { ChangeDetectionStrategy, Component, ViewChild } from '@angular/core';

@Component({
    selector: 'chat-navigation-menu',
    templateUrl: './navigation-menu.component.html',
    styleUrl: './navigation-menu.component.scss',
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class NavigationMenuComponent {
}
