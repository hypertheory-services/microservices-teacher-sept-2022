import { createActionGroup, emptyProps, props } from '@ngrx/store';
import { Course } from './types';

export const CatalogEvents = createActionGroup({
  source: 'Catalog Events',
  events: {
    entered: emptyProps(),
    registrationRequested: props<{
      payload: { courseId: string; offeringId: string };
    }>(),
  },
});

export const CatalogCommands = createActionGroup({
  source: 'Catalog Commands',
  events: {
    load: emptyProps(),
  },
});

export const CatalogDocuments = createActionGroup({
  source: 'Catalog Documents',
  events: {
    catalog: props<{ payload: Course[] }>(),
    course: props<{ payload: Course }>(),
  },
});
