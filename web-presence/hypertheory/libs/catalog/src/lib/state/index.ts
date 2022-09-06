export const FEATURE_NAME = 'catalog';
import {
  ActionReducerMap,
  createFeatureSelector,
  createSelector,
} from '@ngrx/store';
import * as fromCatalog from './reducers/courses.reducer';
import { CourseOfferingDetails } from './types';

export interface CatalogState {
  catalog: fromCatalog.State;
}

export const reducers: ActionReducerMap<CatalogState> = {
  catalog: fromCatalog.reducers,
};

const selectFeature = createFeatureSelector<CatalogState>(FEATURE_NAME);

const selectCatalogBranch = createSelector(selectFeature, (f) => f.catalog);

export const selectCourses = createSelector(
  selectCatalogBranch,
  fromCatalog.selectAll
);

export const selectCourseForRegistration = (offeringId: string) => {
  return createSelector(selectCourses, (courses) => {
    const matches = courses.filter((c) =>
      c.offerings.some((o) => o.id === offeringId)
    );
    if (matches.length > 0) {
      const offeringDetails = matches[0].offerings.find(
        (o) => o.id === offeringId
      );
      const result: CourseOfferingDetails = {
        id: matches[0].id,
        title: matches[0].title,
        category: matches[0].category,
        description: matches[0].description,
        startDate: offeringDetails?.startDate || '' ,
        endDate: offeringDetails?.endDate || '' ,
        numberOfDays: offeringDetails?.numberOfDays || 0,
        price: offeringDetails?.price || 0,
        location: offeringDetails?.location || '',
      };
      return result;
    } else {
      return undefined;
    }
  });
};
