import { AuthorizationGuard } from './guards/authorization.guard';
import { MessagesResolver } from './resolvers/messages.resolver';
import { MemberListResolver } from './resolvers/member-list.resolver';
import { MemberEditComponent } from './components/members/member-edit/member-edit.component';
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
import { MemberEditResolver } from './resolvers/member-edit.resolver';
import { UnsavedChangesEditGuard } from './guards/unsaved-changes-edit.guard';
import { UserLikesResolver } from './resolvers/likes.resolver';
import { AdminComponent } from './components/admin/admin.component';

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
        path: 'admin',
        component: AdminComponent,
        canActivate: [AuthorizationGuard],
        data: { roles: ['Admin']}
      },
      {
        path: '',
        redirectTo: 'members',
        pathMatch: 'full'
      },
      {
        path: 'members',
        component: MembersComponent,
        resolve: {
          page: MemberListResolver
        }
      },
      {
        path: 'members/:id',
        component: MemberDetailedComponent,
        resolve: {
          detailedUser: MemberDetailResolver
        }
      },
      {
        path: 'member/edit/:id',
        component: MemberEditComponent,
        canDeactivate: [UnsavedChangesEditGuard],
        resolve: {
          userForEdit: MemberEditResolver
        }
      },
      {
        path: 'messages',
        component: MessagesComponent,
        resolve: {
          page: MessagesResolver
        }
      },
      {
        path: 'likes',
        component: LikesComponent,
        resolve: {
          page: UserLikesResolver
        }
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
