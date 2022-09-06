import { ChangeDetectionStrategy, Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { selectCourses } from '../state';
import { CatalogEvents } from '../state/catalog.actions';

@Component({
  selector: 'hypertheory-catalog',
  templateUrl: './catalog.component.html',
  styleUrls: ['./catalog.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CatalogComponent {
  catalog$ = this.store.select(selectCourses);
  constructor(private store: Store) {
    store.dispatch(CatalogEvents.entered());
  }
}
