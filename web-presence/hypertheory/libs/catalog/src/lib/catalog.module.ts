import { NgModule } from '@angular/core';
import { StoreModule } from '@ngrx/store';

import { CatalogComponent } from './catalog/catalog.component';
import { CatalogDisplayComponent } from './catalog-display/catalog-display.component';
import { BrowserModule } from '@angular/platform-browser';
import { CatalogEffects } from './state/effects/catalog.effects';
import { EffectsModule } from '@ngrx/effects';
import { FEATURE_NAME, reducers } from './state';

@NgModule({
  imports: [
    BrowserModule,
    StoreModule.forFeature(FEATURE_NAME, reducers),
    EffectsModule.forFeature([CatalogEffects]),
  ],
  declarations: [CatalogComponent, CatalogDisplayComponent],
  exports: [CatalogComponent],
})
export class CatalogModule {}
