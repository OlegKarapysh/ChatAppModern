import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GroupChatsPageComponent } from './group-chats-page/group-chats-page.component';
import { DialogChatsPageComponent } from './dialog-chats-page/dialog-chats-page.component';

const routes: Routes = [
    { path: 'dialogs', component: DialogChatsPageComponent },
    { path: 'groups', component: GroupChatsPageComponent },
    { path: '', pathMatch: 'full', redirectTo: 'dialogs' },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class ChatsRoutingModule {}
