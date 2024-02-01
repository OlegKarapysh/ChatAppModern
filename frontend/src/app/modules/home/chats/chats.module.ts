import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ChatsRoutingModule } from './chats-routing.module';
import { DialogChatsPageComponent } from './dialog-chats-page/dialog-chats-page.component';
import { GroupChatsPageComponent } from './group-chats-page/group-chats-page.component';

@NgModule({
    declarations: [DialogChatsPageComponent, GroupChatsPageComponent],
    imports: [CommonModule, ChatsRoutingModule],
})
export class ChatsModule {}
