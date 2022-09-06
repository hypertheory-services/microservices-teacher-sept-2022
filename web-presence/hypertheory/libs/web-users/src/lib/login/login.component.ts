import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Store } from '@ngrx/store';
import { LoginRequest, UserLoginEvents } from '../state/user.actions';

@Component({
  selector: 'hypertheory-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  model = new FormGroup<LoginRequestModel>({
    email: new FormControl<string>('', { nonNullable: true }),
    password: new FormControl('', { nonNullable: true }),
  });

  constructor(private readonly store: Store) {}
  login() {
    const loginRequest: LoginRequest = {
      email: this.model.controls.email.value,
      password: this.model.controls.password.value,
    };
    this.store.dispatch(UserLoginEvents.requested({ payload: loginRequest }));
  }
}

interface LoginRequestModel {
  email: FormControl<string>;
  password: FormControl<string>;
}
