import { VehiclesListingComponent } from './vehicles-listing.component';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { Spectator, SpectatorElement, byTestId, createComponentFactory } from '@ngneat/spectator';
import { BaseModule } from 'base/base.module';
import { routes } from 'app/app.routes';
import { DeferBlockBehavior, TestBed, fakeAsync } from '@angular/core/testing';
import { Vehicle } from 'vehicles/vehicle';
import { Router } from '@angular/router';

export const VEHICLES = [
  new Vehicle('Super Articulado', 1),
  new Vehicle('Articulado', 2),
  new Vehicle('Padron', 3)
];

describe('VehiclesListingComponent', () => {
  const createComponent = createComponentFactory({
    component: VehiclesListingComponent,
    imports: [
      BaseModule,
      RouterTestingModule.withRoutes(routes),
      HttpClientTestingModule
    ],
    deferBlockBehavior: DeferBlockBehavior.Manual
  });

  let spectator: Spectator<VehiclesListingComponent>;

  beforeEach(() => spectator = createComponent());

  it('should create', () => {
    expect(spectator.component).toBeTruthy();
  });

  it('should list no vehicles', fakeAsync(() => {
    TestBed.inject(HttpTestingController)
      .expectOne(req => !!req.url.match(/vehicles/))
      .flush([]);

    spectator.deferBlock().renderComplete();
    const vehicles = spectator.queryAll(byTestId('vehicle-description'));

    expect(vehicles.length).toBe(0);
  }));

  describe('when there are vehicles registered', () => {
    beforeEach(async () => {
      TestBed.inject(HttpTestingController)
        .expectOne(req => !!req.url.match(/vehicles/))
        .flush(VEHICLES);
  
      await spectator.deferBlock().renderComplete();
    });
    
    it('should show a list of vehicles', () => {
      const vehicles = spectator.queryAll(byTestId('vehicle-description'));
  
      expect(vehicles.length).toBe(VEHICLES.length);
    });

    it('when search field is populated should show only the vehicles that matches',
      () => {
        spectator.typeInElement('Articulado', byTestId('search-input'));
        const vehicles = spectator.queryAll(byTestId('vehicle-description'));

        expect(vehicles.length).toBe(2);
      });

    it('should navigate when a vehicle is clicked', fakeAsync(() => {
      const vehicle = spectator.queryLast(byTestId('vehicle-description'));
      spectator.click(vehicle as SpectatorElement);
      spectator.tick();

      const url = spectator.inject(Router).url;
      const lastVehicle = VEHICLES[VEHICLES.length - 1];
      expect(url).toBe(`/vehicles/edit/${lastVehicle.identification}`);
    }));
  });
});
