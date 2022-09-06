import { createFeature, createReducer, createSelector, on } from '@ngrx/store';
import { WebUser } from './types';
import { UserLoginEvents } from './user.actions';

interface WebUserState {
  user: WebUser | undefined;
}

const initialState: WebUserState = {
  user: undefined,
};

export const webUserFeature = createFeature({
  name: 'webUser',
  reducer: createReducer(
    initialState,
    on(UserLoginEvents.logoutrequested, () => initialState),
    on(UserLoginEvents.succeeded, (state, action) => {
      const email = JSON.parse(atob(action.payload.token.split('.')[1]))[
        'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'
      ];
      return {
        ...state,
        user: {
          token: action.payload.token,
          email: email,
        },
      };
    })
  ),
});

const { selectUser } = webUserFeature;

export const selectUserLoggedIn = createSelector(selectUser, (user) => !!user);
export const selectUserEmail = createSelector(
  selectUser,
  (user) => user?.email
);
export const selectUserToken = createSelector(
  selectUser,
  (user) => user?.token
);
