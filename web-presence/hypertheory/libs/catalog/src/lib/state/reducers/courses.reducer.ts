import { createEntityAdapter, EntityState } from '@ngrx/entity';
import { createReducer, on } from '@ngrx/store';
import { CatalogDocuments } from '../catalog.actions';
import { Course } from '../types';

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface State extends EntityState<Course> {}

export const adapter = createEntityAdapter<Course>();

const initialState: State = adapter.getInitialState();

export const reducers = createReducer(
  initialState,
  on(CatalogDocuments.catalog, (state, { payload }) =>
    adapter.upsertMany(payload, state)
  ),
  on(CatalogDocuments.course, (state, { payload }) =>
    adapter.upsertOne(payload, state)
  )
);

export const { selectAll } = adapter.getSelectors();
