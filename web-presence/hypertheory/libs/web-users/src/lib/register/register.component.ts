import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Store } from '@ngrx/store';
import {
  RegistrationRequest,
  UserRegistrationEvents,
} from '../state/user.actions';

@Component({
  selector: 'hypertheory-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent {
  model = new FormGroup<RegisterModel>({
    firstName: new FormControl('', { nonNullable: true }),
    lastName: new FormControl('', { nonNullable: true }),
    email: new FormControl('', { nonNullable: true }),
    password: new FormControl('', { nonNullable: true }),
  });

  constructor(private readonly store: Store) {}

  create() {
    console.log(this.model.value);
    const request: RegistrationRequest = {
      firstName: this.model.controls.firstName.value,
      lastName: this.model.controls.lastName.value,
      email: this.model.controls.email.value,
      password: this.model.controls.password.value,
    };

    this.store.dispatch(UserRegistrationEvents.requested({ payload: request }));
  }
}

interface RegisterModel {
  firstName: FormControl<string>;
  lastName: FormControl<string>;
  email: FormControl<string>;
  password: FormControl<string>;
}
