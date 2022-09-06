import { createActionGroup, emptyProps, props } from '@ngrx/store';
import { RegistrationEntity } from './types';

export const RegistrationEvents = createActionGroup({
  source: 'Registration Events',
  events: {
    entered: emptyProps(),
    request: props<{ payload: RegistrationRequest }>(),
  },
});

export const RegistrationDocuments = createActionGroup({
  source: 'Registration Documents',
  events: {
    registration: props<{ payload: RegistrationEntity }>(),
    registrations: props<{ payload: RegistrationEntity[]}>()
  },
});

export type RegistrationRequest = {
  offeringId: string;
  courseId: string;
};
