import { ErrorInterceptorProvider } from './ErrorInterceptor';
import { appRoutes } from './routes';

import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { AuthService } from './services/auth.service';
import { AlertService } from './services/alert.service';

import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { LoginFormComponent } from './login-form/login-form.component';
import { HomeComponent } from './home/home.component';
import { RegisterFormComponent } from './register-form/register-form.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { MembersComponent } from './members/members.component';
import { LikesComponent } from './likes/likes.component';
import { MessagesComponent } from './messages/messages.component';
import { NoSuchPageComponent } from './no-such-page/no-such-page.component';

@NgModule({
   declarations: [
      AppComponent,
      NavbarComponent,
      LoginFormComponent,
      HomeComponent,
      RegisterFormComponent,
      MembersComponent,
      LikesComponent,
      MessagesComponent,
      NoSuchPageComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      HttpClientModule,
      NgbModule,
      RouterModule.forRoot(appRoutes)
   ],
   providers: [
      AuthService,
      AlertService,
      ErrorInterceptorProvider
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
