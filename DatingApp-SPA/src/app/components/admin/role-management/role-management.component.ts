import { AlertService } from './../../../services/alert.service';
import { AdminService } from './../../../services/admin.service';
import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/user';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { RoleManagementModalComponent } from './role-management-modal/role-management-modal.component';

@Component({
  selector: 'app-role-management',
  templateUrl: './role-management.component.html',
  styleUrls: ['./role-management.component.css'],
})
export class RoleManagementComponent implements OnInit {
  users: User[];

  constructor(
    private adminService: AdminService,
    private alertify: AlertService,
    private modalService: NgbModal
  ) {}

  ngOnInit() {
    this.getUsersWithRoles();
  }

  getUsersWithRoles() {
    this.adminService.getUsersWithRoles().subscribe((users) => {
      this.users = users;
    }, () => {
      this.alertify.error(`Can't get users from the server`);
    });
  }

  openRolesEditModal(user: User) {
    const modalRef = this.modalService.open(RoleManagementModalComponent);
    modalRef.componentInstance.roles = user.roles;
    modalRef.componentInstance.username = user.username;
    modalRef.result.then(roles => {
      this.adminService.updateRoles(user.id, roles).subscribe(() => {
        const index = this.users.map(u => {
          return u.id;
        }).indexOf(user.id);

        const userWithNewRoles = Object.assign({}, user);
        userWithNewRoles.roles = roles;
        this.users[index] = userWithNewRoles;
        this.alertify.success('Roles for ' + user.username + ' have been modified');
      }, () => {
        this.alertify.error('Failed to modify roles');
      });
    });
  }

  deleteUser(id: number) {
    this.adminService.deleteUser(id).subscribe(() => {
      const index = this.users.map(user => {
        return user.id;
      }).indexOf(id);

      this.users.splice(index, 1);
      this.alertify.success('User has been deleted');
    }, () => {
      this.alertify.error('Failed to delete user');
    });
  }
}
