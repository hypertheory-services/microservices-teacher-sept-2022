import { Component } from '@angular/core';
import { selectUserEmail, selectUserLoggedIn } from '@hypertheory/web-users';
import { Store } from '@ngrx/store';

@Component({
  selector: 'hypertheory-home',
  template: `
    <section>
      <h2>This is a sample application for Hypertheory Training Courses.</h2>
      <p>Don't take this too seriously.</p>
      <div *ngIf="email$ | async as email">
        You are logged in as {{ email }}
      </div>
      <div *ngIf="(loggedIn$ | async) === false">
        <h3>You are not logged in!</h3>
        <div>
          <a class="btn btn-primary" routerLink="/users/login">Login</a>
        </div>
        <div>
          <a class="btn btn-primary" routerLink="/users/register">Register</a>
        </div>
      </div>
    </section>
  `,
})
export class HomeComponent {
  loggedIn$ = this.store.select(selectUserLoggedIn);
  email$ = this.store.select(selectUserEmail);

  constructor(private store: Store) {}
}
