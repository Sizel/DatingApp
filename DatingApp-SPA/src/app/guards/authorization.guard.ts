import { AlertService } from './../services/alert.service';
import { AuthService } from './../services/auth.service';
import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthorizationGuard implements CanActivate {
  constructor(
    private auth: AuthService,
    private router: Router,
    private alertify: AlertService
  ) {}

  canActivate(snapshot: ActivatedRouteSnapshot): boolean {
    const rolesForRoute = snapshot.data.roles;
    const isAuthorized = this.auth.isAuthorized(rolesForRoute);
    if (!isAuthorized) {
      this.alertify.error('You don\'t have permissions to access this');
      this.router.navigate(['/members']);
    }
    return isAuthorized;
  }
}
