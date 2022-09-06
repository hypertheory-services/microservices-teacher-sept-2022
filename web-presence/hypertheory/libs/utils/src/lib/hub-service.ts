import { Injectable } from '@angular/core';
import { selectUserToken } from '@hypertheory/web-users';
import * as signalR from '@microsoft/signalr';
import { Action, Store } from '@ngrx/store';
import { filter, from, tap } from 'rxjs';
// import { CatalogDocuments, Course } from '@hypertheory/catalog';
// import { RegistrationDocuments } from '@hypertheory/registrations';

@Injectable({ providedIn: 'root' })
export class HubService {
  private token = '';
  private connnection!: signalR.HubConnection;
  private messageSenders: MessageSenders = {};
  constructor(private readonly store: Store) {
    store
      .select(selectUserToken)
      .pipe(
        filter((t) => !!t),
        tap((token) => (this.token = token || ''))
      )
      .subscribe();
  }

  public addMessageHandler<T>(
    message: string,
    handler: MessageHandlerFunction<T>
  ) {
    this.messageSenders[message] = handler;
    if (this.connnection) {
      this.connnection.on(message, handler);
    }
  }
  private handleMessages() {
    // const handleCourse = (course: Course) =>
    //   CatalogDocuments.course({ payload: course });
    // // Todo - Abstract this stuff.
    // this.connnection.on('course', (course) =>
    //   this.store.dispatch(handleCourse(course))
    // );
    // this.connnection.on('registration', (registration) =>
    //   this.store.dispatch(
    //     RegistrationDocuments.registration({ payload: registration })
    //   )
    // );

    for (const message in this.messageSenders) {
      this.connnection.on(message, (data) =>
        this.store.dispatch(this.messageSenders[message](data))
      );
    }
  }

  public send(method: string, ...args: unknown[]) {
    return from(this.connnection.send(method, ...args));
  }

  startConnection(token: string) {
    if (!this.connnection) {
      const options: signalR.IHttpConnectionOptions = {
        accessTokenFactory: () => token,
      };
      this.connnection = new signalR.HubConnectionBuilder()
        .withUrl('http://localhost:4201/courses-hub', options)
        .configureLogging(signalR.LogLevel.Trace)
        .build();
      this.handleMessages();
      console.log('Doing the deed');
      return from(this.connnection.start());
    }
    return from(Promise.resolve());
  }

  stopConnection() {
    console.log('Stopping connection');
    if (this.connnection?.state === signalR.HubConnectionState.Connected) {
      return this.connnection.stop();
    } else {
      return Promise.resolve();
    }
  }
}

export type MessageSenders = {
  [key: string]: MessageHandlerFunction<any>;
};

type MessageHandlerFunction<T> = (payload: T) => Action;
