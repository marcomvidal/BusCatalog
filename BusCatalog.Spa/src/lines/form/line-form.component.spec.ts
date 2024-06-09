import { TestBed, fakeAsync } from '@angular/core/testing';
import { LineFormComponent } from './line-form.component';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { BaseModule } from 'base/base.module';
import { routes } from 'app/app.routes';
import { Spectator, byTestId, byText, createComponentFactory } from '@ngneat/spectator';
import { Router } from '@angular/router';
import { Line } from 'lines/line';
import { NgSelectModule } from '@ng-select/ng-select';
import { CommonModule } from '@angular/common';
import { VEHICLES } from 'vehicles/listing/vehicles-listing.component.spec';
import { ReactiveFormsModule } from '@angular/forms';

export const LINE = new Line(
  '287',
  'Terminal Diadema',
  'Terminal Santo André Oeste',
  275,
  ['ARTICULADO', 'SUPER_ARTICULADO']);

describe('LineFormComponent', () => {
  const createComponent = createComponentFactory({
    component: LineFormComponent,
    imports: [
      BaseModule,
      RouterTestingModule.withRoutes(routes),
      HttpClientTestingModule,
      NgSelectModule
    ]
  });

  let spectator: Spectator<LineFormComponent>;

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
      spectator.typeInElement('A', spectator.query('#identification') as HTMLInputElement);
      spectator.tick();

      expect(submitButton.disabled).toBeTrue();
    }));
  });

  describe('when form is valid', () => {
    let submitButton: HTMLButtonElement;

    beforeEach(fakeAsync(() => {
      TestBed.inject(HttpTestingController)
        .expectOne(req => !!req.url.match(/vehicles/))
        .flush(VEHICLES);
      
      submitButton = spectator.query(byTestId('submit-button')) as HTMLButtonElement;
      
      spectator.typeInElement(LINE.identification, spectator.query('#identification') as HTMLInputElement);
      spectator.typeInElement(LINE.fromwards, spectator.query('#fromwards') as HTMLInputElement);
      spectator.typeInElement(LINE.towards, spectator.query('#towards') as HTMLInputElement);
      spectator.typeInElement(`${LINE.departuresPerDay}`, spectator.query('#departuresPerDay') as HTMLInputElement);
      
      // TODO Interact with ng-select

      spectator.tick();
    }));

    fit('should be submittable', fakeAsync(() => {
      debugger;

      expect(submitButton.disabled).toBeFalse();
    }));

    xit('when back-end is ok should submit and redirect to listing', fakeAsync(() => {
      spectator.click(submitButton);

      TestBed.inject(HttpTestingController)
        .expectOne(req => !!req.url.match(/vehicles/))
        .flush(LINE);
      
      spectator.tick();
      const url = spectator.inject(Router).url;
      
      expect(url).toBe('/vehicles');
    }));

    xit('when back-end fails should show errors', fakeAsync(() => {
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
