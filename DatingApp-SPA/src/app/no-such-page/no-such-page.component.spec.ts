/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { NoSuchPageComponent } from './no-such-page.component';

describe('NoSuchPageComponent', () => {
  let component: NoSuchPageComponent;
  let fixture: ComponentFixture<NoSuchPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NoSuchPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NoSuchPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
