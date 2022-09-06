import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { Store } from '@ngrx/store';
import { selectUserToken } from '@hypertheory/web-users';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  token: string | undefined = undefined;
  constructor(private readonly store: Store) {
    this.store
      .select(selectUserToken)
      .pipe(tap((token) => (this.token = token || '')))
      .subscribe();
  }

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    if (this.token) {
      // TODO: Check if token is expired, also check if URL is ours.
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${this.token}`,
        },
      });
      return next.handle(request);
    }
    return next.handle(request);
  }
}
