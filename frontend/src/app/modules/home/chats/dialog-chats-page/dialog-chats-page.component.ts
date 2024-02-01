import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
    selector: 'chat-dialog-chats-page',
    templateUrl: './dialog-chats-page.component.html',
    styleUrl: './dialog-chats-page.component.scss',
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class DialogChatsPageComponent {}
