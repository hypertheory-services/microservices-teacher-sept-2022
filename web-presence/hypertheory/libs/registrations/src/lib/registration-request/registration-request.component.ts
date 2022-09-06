import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import {
  CourseOfferingDetails,
  selectCourseForRegistration,
} from '@hypertheory/catalog';
import { Observable } from 'rxjs';
import { selectUserEmail } from '@hypertheory/web-users';
import {
  RegistrationEvents,
  RegistrationRequest,
} from '../state/registration.actions';

@Component({
  selector: 'hypertheory-registration-request',
  templateUrl: './registration-request.component.html',
  styleUrls: ['./registration-request.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RegistrationRequestComponent implements OnInit {
  offeringId = '';
  course$!: Observable<CourseOfferingDetails | undefined>;
  email$ = this.store.select(selectUserEmail);
  constructor(
    private readonly store: Store,
    private readonly route: ActivatedRoute,
    private readonly router:Router
  ) {}

  ngOnInit(): void {
    this.offeringId =
      this.route.snapshot.paramMap.get('offeringId') || 'Not Found';

    this.course$ = this.store.select(
      selectCourseForRegistration(this.offeringId)
    );
  }

  confirmRegistrationRequest(courseId: string) {
    const payload: RegistrationRequest = {
      courseId,
      offeringId: this.offeringId,
    };
    this.store.dispatch(RegistrationEvents.request({ payload }));
    this.router.navigate(['..', 'registrations'])
  }
}
