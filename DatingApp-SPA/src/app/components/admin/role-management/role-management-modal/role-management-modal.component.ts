import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-role-management-modal',
  templateUrl: './role-management-modal.component.html',
  styleUrls: ['./role-management-modal.component.css']
})
export class RoleManagementModalComponent implements OnInit {
  @Input() roles: string[];
  @Input() username: string;
  availableRoles = ['Member', 'Moderator', 'Admin'];
  selectedRoles: any = {};

  constructor(public activeModal: NgbActiveModal) { }

  ngOnInit() {
  }

  // данный метод будет вызываться каждый раз когда меняется значение соответствующего чекбокса
  changeRole(role: string){
    // если роль уже есть в массиве, то ее нужно убрать
    if (this.roles.includes(role)) {
      const index = this.roles.indexOf(role);
      this.roles.splice(index, 1);
    }
    // если роли нет, то добавить
    else {
      this.roles.push(role);
    }
  }
}
