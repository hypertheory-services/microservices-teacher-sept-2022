import { Component } from '@angular/core';
import { selectUserLoggedIn } from '@hypertheory/web-users';
import { Store } from '@ngrx/store';

@Component({
  selector: 'hypertheory-nav',
  template: ` <nav>
    <ul class="nav nav-tabs">
      <li class="nav-item">
        <a
          class="nav-link"
          [routerLink]="['home']"
          [routerLinkActive]="['active']"
          >Home</a
        >
      </li>
      <li class="nav-item" *ngIf="loggedIn$ | async">
        <a
          class="nav-link"
          [routerLink]="['catalog']"
          [routerLinkActive]="['active']"
          >Catalog</a
        >
      </li>
      <li class="nav-item" *ngIf="loggedIn$ | async">
        <a
          class="nav-link"
          [routerLink]="['registrations']"
          [routerLinkActive]="['active']"
          >Registrations</a
        >
      </li>
    </ul>
  </nav>`,
})
export class NavComponent {
  loggedIn$ = this.store.select(selectUserLoggedIn);
  constructor(private readonly store: Store) {}
}
