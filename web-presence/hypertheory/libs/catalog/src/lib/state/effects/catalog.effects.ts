import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { HubService } from '@hypertheory/utils';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { map, switchMap } from 'rxjs';

import {
  CatalogCommands,
  CatalogDocuments,
  CatalogEvents,
} from '../catalog.actions';

import { Course } from '../types';

@Injectable()
export class CatalogEffects {
  loadOnEnter = createEffect(() => {
    return this.actions$.pipe(
      ofType(CatalogEvents.entered),
      map(() => CatalogCommands.load())
    );
  });

  load$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(CatalogCommands.load),
      switchMap(() =>
        this.client
          .get<{ data: Course[] }>('http://localhost:4201/courses')
          .pipe(
            map((payload) =>
              CatalogDocuments.catalog({ payload: payload.data })
            )
          )
      )
    );
  });

  constructor(
    private readonly actions$: Actions,
    private readonly client: HttpClient,
    hub: HubService
  ) {
    hub.addMessageHandler<Course>('course', (course) =>
      CatalogDocuments.course({ payload: course })
    );
  }
}
