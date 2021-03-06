import { PhotoService } from './services/photo.service';
import { RoleManagementModalComponent } from './components/admin/role-management/role-management-modal/role-management-modal.component';
import { RoleManagementComponent } from './components/admin/role-management/role-management.component';
import { AdminComponent } from './components/admin/admin.component';
import { MemberConversationWindowComponent } from './components/members/member-detailed/member-conversation-window/member-conversation-window.component';
import { MessagesResolver } from './resolvers/messages.resolver';
import { appRoutes } from './routes';

import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { JwtModule } from '@auth0/angular-jwt';
import { NgxGalleryModule } from 'ngx-gallery-9';
import { TimeagoModule } from 'ngx-timeago';
import { FileUploadModule } from 'ng2-file-upload';

import { AuthService } from './services/auth.service';
import { AlertService } from './services/alert.service';
import { UserService } from './services/user.service';
import { MemberDetailResolver } from './resolvers/member-detail.resolver';
import { MemberEditResolver } from './resolvers/member-edit.resolver';
import { ErrorInterceptorProvider } from './ErrorInterceptor';

import { AppComponent } from './app.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { LoginFormComponent } from './components/navbar/login-form/login-form.component';
import { HomeComponent } from './components/home/home.component';
import { RegisterFormComponent } from './components/home/register-form/register-form.component';
import { MembersComponent } from './components/members/members-list/members.component';
import { LikesComponent } from './components/likes/likes.component';
import { MessagesComponent } from './components/messages/messages.component';
import { NoSuchPageComponent } from './components/no-such-page/no-such-page.component';
import { MemberCardComponent } from './components/members/member-card/member-card/member-card.component';
import { MemberDetailedComponent } from './components/members/member-detailed/member-detailed.component';
import { MemberEditComponent } from './components/members/member-edit/member-edit.component';
import { MemberListResolver } from './resolvers/member-list.resolver';
import { UserLikesResolver } from './resolvers/likes.resolver';
import { IsAuthorizedDirective } from './directives/is-authorized.directive';
import { AdminService } from './services/admin.service';
import { MemberEditPhotosComponent } from './components/members/member-edit/member-edit-photos/member-edit-photos.component';

export function tokenGetter() {
   return localStorage.getItem('token');
}

@NgModule({
   declarations: [
      AppComponent,
      NavbarComponent,
      LoginFormComponent,
      HomeComponent,
      RegisterFormComponent,
      MembersComponent,
      MemberEditPhotosComponent,
      LikesComponent,
      MessagesComponent,
      NoSuchPageComponent,
      MemberCardComponent,
      MemberDetailedComponent,
      MemberEditComponent,
      MemberConversationWindowComponent,
      AdminComponent,
      RoleManagementComponent,
      RoleManagementModalComponent,
      IsAuthorizedDirective,
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      HttpClientModule,
      NgxGalleryModule,
      FileUploadModule,
      NgbModule,
      ReactiveFormsModule,
      TimeagoModule.forRoot(),
      RouterModule.forRoot(appRoutes),
      JwtModule.forRoot({
         config: {
            tokenGetter,
            whitelistedDomains: ['localhost:5000'],
            blacklistedRoutes: ['localhost:5000/api/auth']
         }
      })
   ],
   providers: [
      AuthService,
      AlertService,
      UserService,
      AdminService,
      PhotoService,
      ErrorInterceptorProvider,
      MemberDetailResolver,
      MemberEditResolver,
      MemberListResolver,
      UserLikesResolver,
      MessagesResolver,
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
