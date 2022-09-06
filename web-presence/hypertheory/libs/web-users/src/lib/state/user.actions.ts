import { createActionGroup, emptyProps, props } from '@ngrx/store';

export const UserRegistrationEvents = createActionGroup({
  source: 'User Registration Events',
  events: {
    requested: props<{ payload: RegistrationRequest }>(),
    succeeded: emptyProps(),
    failed: emptyProps(),
  },
});

export const UserLoginEvents = createActionGroup({
  source: 'User Login Events',
  events: {
    requested: props<{ payload: LoginRequest }>(),
    succeeded: props<{ payload: LoginSuccessResponse }>(),
    logoutRequested: emptyProps(),
  },
});

export type RegistrationRequest = {
  firstName: string;
  lastName: string;
  email: string;
  password: string;
};

export type LoginRequest = {
  email: string;
  password: string;
};

export type LoginSuccessResponse = {
  token: string;
  expiration: string;
};
