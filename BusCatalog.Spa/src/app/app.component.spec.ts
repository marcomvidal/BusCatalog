import { Spectator, byText, createComponentFactory } from '@ngneat/spectator';
import { AppComponent } from './app.component';
import { RouterTestingModule } from '@angular/router/testing';
import { routes } from './app.routes';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { LinesListingComponent } from 'lines/listing/lines-listing.component';
import { VehiclesListingComponent } from 'vehicles/listing/vehicles-listing.component';
import { BaseModule } from 'base/base.module';

describe('AppComponent', () => {
  let spectator: Spectator<AppComponent>;
  const createComponent = createComponentFactory({
    component: AppComponent,
    imports: [
      BaseModule,
      RouterTestingModule.withRoutes(routes),
      HttpClientTestingModule,
      LinesListingComponent,
      VehiclesListingComponent
    ]
  });
  
  beforeEach(() => spectator = createComponent());

  it('should create', () => {
    expect(spectator.component).toBeTruthy();
  });
});
