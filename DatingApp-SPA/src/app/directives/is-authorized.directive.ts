import { AuthService } from 'src/app/services/auth.service';
import { Directive, Input, ViewContainerRef, TemplateRef, OnInit } from '@angular/core';

@Directive({
  selector: '[appIsAuthorized]',
})
export class IsAuthorizedDirective implements OnInit {
  @Input('appIsAuthorized') requiredRoles;

  constructor(
    private containerRef: ViewContainerRef,
    private templateRef: TemplateRef<any>,
    private auth: AuthService
  ) {}

  ngOnInit(): void {
    if (this.auth.isAuthorized(this.requiredRoles)) {
      this.containerRef.createEmbeddedView(this.templateRef);
    }
  }
}
