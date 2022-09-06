import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Route } from '@angular/router';
import { RegistrationsComponent } from './registrations.component';
import { RegistrationRequestComponent } from './registration-request/registration-request.component';
import { StoreModule } from '@ngrx/store';
import { FEATURE_NAME, reducers } from './state';
import { LoggedInGuard } from '@hypertheory/utils';
import { EffectsModule } from '@ngrx/effects';
import { RegistrationEffects } from './state/effects/registration.effects';
import { HttpClientModule } from '@angular/common/http';

export const registrationsRoutes: Route[] = [
  {
    path: 'registrations',
    component: RegistrationsComponent,
    canActivate: [LoggedInGuard],
    children: [
      {
        path: 'request/:offeringId',
        component: RegistrationRequestComponent,
      },
    ],
  },
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(registrationsRoutes),
    StoreModule.forFeature(FEATURE_NAME, reducers),
    EffectsModule.forFeature([RegistrationEffects]),
    HttpClientModule
  ],
  declarations: [RegistrationsComponent, RegistrationRequestComponent],
  exports: [RegistrationsComponent, RouterModule],
})
export class RegistrationsModule {}
