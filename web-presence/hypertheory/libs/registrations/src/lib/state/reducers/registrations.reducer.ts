import { createEntityAdapter, EntityState } from '@ngrx/entity';
import { createReducer, on } from '@ngrx/store';
import { RegistrationDocuments } from '../registration.actions';
import { RegistrationEntity } from '../types';

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface RegistrationState extends EntityState<RegistrationEntity> {}

export const adapter = createEntityAdapter<RegistrationEntity>();

const initialState: RegistrationState = adapter.getInitialState();

export const reducer = createReducer(
  initialState,
  on(RegistrationDocuments.registration, (state, { payload }) =>
    adapter.upsertOne(payload, state)
  ),
  on(RegistrationDocuments.registrations, (s,a) => adapter.upsertMany(a.payload,s))
);
