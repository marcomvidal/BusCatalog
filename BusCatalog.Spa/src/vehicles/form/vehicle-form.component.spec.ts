import { TestBed, fakeAsync } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { VehicleFormComponent } from './vehicle-form.component';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { Spectator, byTestId, byText, createComponentFactory } from '@ngneat/spectator';
import { BaseModule } from 'base/base.module';
import { routes } from 'app/app.routes';
import { Vehicle } from 'vehicles/vehicle';
import { ActivatedRoute, Router } from '@angular/router';
import { of } from 'rxjs';

export const VEHICLE = new Vehicle('Articulado', 1);

describe('VehicleFormComponent', () => {
  const createComponent = createComponentFactory({
    component: VehicleFormComponent,
    imports: [
      BaseModule,
      RouterTestingModule.withRoutes(routes),
      HttpClientTestingModule,
    ]
  });

  let spectator: Spectator<VehicleFormComponent>;

  describe('when url has no parameters', () => {
    beforeEach(() => spectator = createComponent());

    it('should create', () => {
      expect(spectator.component).toBeTruthy();
    });

    describe('when form is invalid', () => {
      it('when form is unfilled should not be submittable', () => {
        const submitButton = spectator.query(byTestId('submit-button')) as HTMLButtonElement;

        expect(submitButton.disabled).toBeTrue();
      });

      it('when form is invalid should not be submittable', fakeAsync(() => {
        const submitButton = spectator.query(byTestId('submit-button')) as HTMLButtonElement;
        spectator.typeInElement('A', spectator.query('#description') as HTMLInputElement);
        spectator.tick();

        expect(submitButton.disabled).toBeTrue();
      }));
    });

    describe('when form is valid', () => {
      let submitButton: HTMLButtonElement;

      beforeEach(fakeAsync(() => {
        submitButton = spectator.query(byTestId('submit-button')) as HTMLButtonElement;
        spectator.typeInElement(VEHICLE.description, spectator.query('#description') as HTMLInputElement);
        spectator.tick();
      }));

      it('should be submittable', fakeAsync(() => {
        expect(submitButton.disabled).toBeFalse();
      }));

      it('when back-end is ok should submit and redirect to listing', fakeAsync(() => {
        spectator.click(submitButton);

        TestBed.inject(HttpTestingController)
          .expectOne(req => !!req.url.match(/vehicles/))
          .flush(VEHICLE);

        spectator.tick();
        const url = spectator.inject(Router).url;

        expect(url).toBe('/vehicles');
      }));

      it('when back-end fails should show errors', fakeAsync(() => {
        const errorMessage = 'Something went wrong';
        spectator.click(submitButton);

        TestBed.inject(HttpTestingController)
          .expectOne(req => !!req.url.match(/vehicles/))
          .flush(
            { errors: { someError: [errorMessage] } },
            { status: 400, statusText: 'Error' });

        spectator.tick();
        const message = spectator.queryLast(byText(errorMessage));

        expect(message).toBeTruthy();
      }));
    });
  });

  describe('when url has identifier parameter', () => {
    beforeEach(fakeAsync(() => {
      spectator = createComponent({
        providers: [
          {
            provide: ActivatedRoute,
            useValue: { params: of({ identification: VEHICLE.identification }) }
          }
        ]
      });

      TestBed.inject(HttpTestingController)
        .expectOne(req => !!req.url.match(/vehicles/))
        .flush(VEHICLE);

      spectator.tick();
    }));

    it('should create', () => {
      expect(spectator.component).toBeTruthy();
    });

    it('should populate form base on the url identifier', () => {
      const form = {
        id: spectator.component.id,
        description: (spectator.query('#description') as HTMLInputElement).value
      };

      expect(form).toEqual(jasmine.objectContaining({
        id: VEHICLE.id,
        description: VEHICLE.description
      }));
    });

    it('should have delete button visible', () => {
      const deleteButton = spectator.query(byTestId('delete-button'));

      expect(deleteButton).toBeTruthy();
    });
  });
});
