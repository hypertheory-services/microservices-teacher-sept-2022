import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Actions, concatLatestFrom, createEffect, ofType } from '@ngrx/effects';
import {
  selectUserToken,
  UserLoginEvents,
} from '@hypertheory/web-users';
import {
  filter,
  switchMap,
  tap,
} from 'rxjs';
import { CatalogEvents } from '@hypertheory/catalog';
import { Store } from '@ngrx/store';
import { HubService } from '@hypertheory/utils';

@Injectable()
export class AppEffects {
  goToCatalogOnSuccessfulLogin$ = createEffect(
    () => {
      return this.actions$.pipe(
        ofType(UserLoginEvents.succeeded),
        tap(() => this.router.navigateByUrl('/catalog'))
      );
    },
    { dispatch: false }
  );

  goToRegistrationRequest$ = createEffect(
    () => {
      return this.actions$.pipe(
        ofType(CatalogEvents.registrationrequested),
        tap(({ payload }) =>
          this.router.navigate(['registrations', 'request', payload.offeringId])
        )
      );
    },
    { dispatch: false }
  );
  startHub$ = createEffect(
    () => {
      return this.actions$.pipe(
        concatLatestFrom(() => this.store.select(selectUserToken)),
        filter(([, token]) => !!token),
        switchMap(([, token]) => this.hub.startConnection(token || ''))
      );
    },
    { dispatch: false }
  );
  constructor(
    private actions$: Actions,
    private readonly client: HttpClient,
    private router: Router,
    private store: Store,
    private readonly hub: HubService
  ) {}
}
