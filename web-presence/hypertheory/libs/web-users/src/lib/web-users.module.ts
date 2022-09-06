import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StoreModule } from '@ngrx/store';
import { webUserFeature } from './state';
import { RegisterComponent } from './register/register.component';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { UsersComponent } from './users/users.component';
import { HttpClientModule } from '@angular/common/http';
import { UserEffects } from './state/effects/user-effects';
import { EffectsModule } from '@ngrx/effects';
import { LoginComponent } from './login/login.component';
const routes: Routes = [
  {
    path: 'users',
    component: UsersComponent,
    children: [
      {
        path: 'register',
        component: RegisterComponent,
      },
      {
        path: 'login',
        component: LoginComponent,
      },
    ],
  },
];

@NgModule({
  imports: [
    CommonModule,
    StoreModule.forFeature(webUserFeature),
    EffectsModule.forFeature([UserEffects]),
    ReactiveFormsModule,
    RouterModule.forChild(routes),
    HttpClientModule,
  ],
  declarations: [RegisterComponent, UsersComponent, LoginComponent],
  exports: [RegisterComponent, UsersComponent],
})
export class WebUsersModule {}
