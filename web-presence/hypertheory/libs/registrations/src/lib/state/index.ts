import { selectCourses } from '@hypertheory/catalog';
import { ActionReducerMap, createFeatureSelector, createSelector } from '@ngrx/store';
import * as fromRegistrations from './reducers/registrations.reducer';
import { RegistrationSummaryItem } from './types';
export const FEATURE_NAME = 'registrations';
export interface RegistrationsState {
  registrations: fromRegistrations.RegistrationState;
}

export const reducers: ActionReducerMap<RegistrationsState> = {
  registrations: fromRegistrations.reducer,
};

const selectFeature = createFeatureSelector<RegistrationsState>(FEATURE_NAME);

const selectRegistrationsBranch = createSelector(selectFeature, f => f.registrations);

const {selectAll: selectAllRegistrations} = fromRegistrations.adapter.getSelectors(selectRegistrationsBranch);

export const selectRegistrationListModel = createSelector(
  selectAllRegistrations,
  selectCourses,
  (registrations, courses) => {
    return registrations.map(r => {
      const matchingOffering = courses.find(c=> c.id == r.courseId);
      const startDate = matchingOffering?.offerings.find(o => o.id === r.offeringId)?.startDate
      return {
        ...r,
        title: matchingOffering?.title,
        startDate: startDate

      } as RegistrationSummaryItem
    });

  }
)