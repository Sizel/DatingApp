import { MemberDetailResolver } from './resolvers/member-detail.resolver';
import { RegistrationGuard } from './guards/registration.guard';
import { AuthGuard } from './guards/auth.guard';
import { NoSuchPageComponent } from './components/no-such-page/no-such-page.component';
import { LikesComponent } from './components/likes/likes.component';
import { MessagesComponent } from './components/messages/messages.component';
import { MembersComponent } from './components/members/members-list/members.component';
import { HomeComponent } from './components/home/home.component';
import { Routes } from '@angular/router';
import { MemberDetailedComponent } from './components/members/member-detailed/member-detailed.component';

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
        path: 'members/:id',
        component: MemberDetailedComponent,
        resolve: {
          user: MemberDetailResolver
        }
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
