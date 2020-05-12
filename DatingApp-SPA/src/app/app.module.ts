import { ErrorInterceptorProvider } from './ErrorInterceptor';

import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AuthService } from './services/auth.service';

import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { LoginFormComponent } from './login-form/login-form.component';
import { HomeComponent } from './home/home.component';
import { RegisterFormComponent } from './register-form/register-form.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
   declarations: [
      AppComponent,
      NavbarComponent,
      LoginFormComponent,
      HomeComponent,
      RegisterFormComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      HttpClientModule,
      NgbModule,
   ],
   providers: [
      AuthService,
      ErrorInterceptorProvider,
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
