import { Injectable } from '@angular/core';
import { HubService } from '@hypertheory/utils';
import { HttpClient } from '@angular/common/http';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { nanoid } from 'nanoid';
import { concatMap, map, switchMap } from 'rxjs';
import {
  RegistrationDocuments,
  RegistrationEvents,
} from '../registration.actions';
import { RegistrationEntity } from '../types';

@Injectable()
export class RegistrationEffects {

  loadRegistrations$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(RegistrationEvents.entered),
      switchMap(()=> this.http.get<{data: RegistrationEntity[]}>('http://localhost:4201/my/registrations')
        .pipe(
          map(({data}) => data),
          map((payload)=> RegistrationDocuments.registrations({payload}))
        )
      )
    )
  });
  sendRegistrationRequest$ = createEffect(
    () => {
      return this.actions$.pipe(
        ofType(RegistrationEvents.request),
        map(
          ({ payload }) =>
            ({
              ...payload,
              id: nanoid(),
              status: 'sending',
            } as RegistrationEntity)
        ),
        concatMap((payload) =>
          this.hub
            .send('RegistrationRequested', payload)
            .pipe(map(() => RegistrationDocuments.registration({ payload })))
        )
      );
    },
    { dispatch: true }
  );

  constructor(
    private readonly actions$: Actions,
    private readonly hub: HubService,
    private readonly http: HttpClient
  ) {
    hub.addMessageHandler<RegistrationEntity>('registration', (registration) =>
      RegistrationDocuments.registration({ payload: registration })
    );
  }
}
