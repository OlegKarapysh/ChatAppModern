import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
    selector: 'chat-group-chats-page',
    templateUrl: './group-chats-page.component.html',
    styleUrl: './group-chats-page.component.scss',
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class GroupChatsPageComponent {}
