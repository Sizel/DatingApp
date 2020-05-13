import { RegistrationGuard } from './guards/registration.guard';
import { AuthGuard } from './guards/auth.guard';
import { NoSuchPageComponent } from './no-such-page/no-such-page.component';
import { LikesComponent } from './likes/likes.component';
import { MessagesComponent } from './messages/messages.component';
import { MembersComponent } from './members/members.component';
import { HomeComponent } from './home/home.component';
import { Routes } from '@angular/router';

export const appRoutes: Routes = [
  {
    path: 'home',
    component: HomeComponent,
    canActivate: [RegistrationGuard],
  },
  {
    path: '',
    canActivate: [AuthGuard],
    children: [
      {
        path: '',
        redirectTo: 'members',
        pathMatch: 'full'
      },
      {
        path: 'members',
        component: MembersComponent,
      },
      {
        path: 'messages',
        component: MessagesComponent,
      },
      {
        path: 'likes',
        component: LikesComponent,
      },
    ],
  },
  {
    path: 'pageNotFound',
    component: NoSuchPageComponent,
  },
  {
    path: '**',
    redirectTo: 'pageNotFound',
    pathMatch: 'full',
  },
];
