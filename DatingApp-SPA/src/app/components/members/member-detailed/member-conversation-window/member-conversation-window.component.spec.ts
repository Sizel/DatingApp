/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { MemberConversationWindowComponent } from './member-conversation-window.component';

describe('MemberConversationWindowComponent', () => {
  let component: MemberConversationWindowComponent;
  let fixture: ComponentFixture<MemberConversationWindowComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MemberConversationWindowComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MemberConversationWindowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
