import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { switchMap, map, catchError, of, tap } from 'rxjs';
import {
  LoginSuccessResponse,
  UserLoginEvents,
  UserRegistrationEvents,
} from '../user.actions';
@Injectable()
export class UserEffects {
  login$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(UserLoginEvents.requested),
      switchMap((action) =>
        this.client
          .post<LoginSuccessResponse>(
            'http://localhost:4201/login',
            action.payload
          )
          .pipe(map((p) => UserLoginEvents.succeeded({ payload: p })))
      )
    );
  });

  register$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(UserRegistrationEvents.requested),
      switchMap((e) =>
        this.client.post('http://localhost:4201/register', e.payload).pipe(
          map(() => UserRegistrationEvents.succeeded()),
          catchError(() => of(UserRegistrationEvents.failed()))
        )
      )
    );
  });

  registerSucceeded$ = createEffect(
    () => {
      return this.actions$.pipe(
        ofType(UserRegistrationEvents.succeeded),
        tap(() => this.router.navigate(['/users/login']))
      );
    },
    { dispatch: false }
  );

  constructor(
    private readonly actions$: Actions,
    private readonly client: HttpClient,
    private readonly router: Router
  ) {}
}
