import { Component, Input } from '@angular/core';
import { Store } from '@ngrx/store';
import { CatalogEvents } from '../state/catalog.actions';
import { Course } from '../state/types';

@Component({
  selector: 'hypertheory-catalog-display',

  templateUrl: './catalog-display.component.html',
  styleUrls: ['./catalog-display.component.css'],
})
export class CatalogDisplayComponent {
  @Input() catalog: Course[] = [];

  constructor(private store: Store) {}

  requestRegistration(courseId: string, offeringId: string) {
    this.store.dispatch(
      CatalogEvents.registrationrequested({ payload: { courseId, offeringId } })
    );
  }
}
