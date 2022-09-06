import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { selectRegistrationListModel } from './state';
import { RegistrationEvents } from './state/registration.actions';

@Component({
  selector: 'hypertheory-registrations',
  templateUrl: './registrations.component.html',
  styleUrls: ['./registrations.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RegistrationsComponent  {
  model$ = this.store.select(selectRegistrationListModel);
  constructor(private store:Store) {
    store.dispatch(RegistrationEvents.entered())
  }


}
