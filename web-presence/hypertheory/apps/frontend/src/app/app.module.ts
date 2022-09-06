import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { CatalogModule } from '@hypertheory/catalog';
import { RegistrationsModule } from '@hypertheory/registrations';
import { HubService } from '@hypertheory/utils';
import { WebUsersModule } from '@hypertheory/web-users';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { AuthInterceptor } from './auth.interceptor';
import { HeaderComponent } from './components/header.component';
import { HomeComponent } from './components/home.component';
import { NavComponent } from './components/nav.component';
import { reducers } from './state';
import { AppEffects } from './state/app.effects';

@NgModule({
  declarations: [AppComponent, HeaderComponent, NavComponent, HomeComponent],
  imports: [
    BrowserModule,
    RegistrationsModule,
    WebUsersModule,
    CatalogModule,
    HttpClientModule,
    StoreModule.forRoot(reducers),
    EffectsModule.forRoot([AppEffects]),
    StoreDevtoolsModule.instrument(),
    AppRoutingModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
    HubService,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
