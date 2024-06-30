import { TestBed, fakeAsync } from '@angular/core/testing';
import { LineFormComponent } from './line-form.component';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { BaseModule } from 'base/base.module';
import { routes } from 'app/app.routes';
import { Spectator, byTestId, byText, createComponentFactory } from '@ngneat/spectator';
import { ActivatedRoute, Router } from '@angular/router';
import { Line } from 'lines/line';
import { NgSelectModule } from '@ng-select/ng-select';
import { VEHICLES } from 'vehicles/listing/vehicles-listing.component.spec';
import { ReactiveFormsModule } from '@angular/forms';
import { NgSelectTestHandler } from 'base/tests/ng-select-handler';
import { of } from 'rxjs';

export const LINE = new Line(
  '287',
  'Terminal Diadema',
  'Terminal Santo André Oeste',
  275,
  ['ARTICULADO', 'SUPER_ARTICULADO'],
  1);

describe('LineFormComponent', () => {
  const createComponent = createComponentFactory({
    component: LineFormComponent,
    imports: [
      BaseModule,
      RouterTestingModule.withRoutes(routes),
      HttpClientTestingModule,
      ReactiveFormsModule,
      NgSelectModule,
    ]
  });

  let spectator: Spectator<LineFormComponent>;

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

        const selectHandler = new NgSelectTestHandler(spectator);
        selectHandler.openSelect('#vehicles');
        selectHandler.selectOption('#vehicles');
        spectator.tick();
      }));

      it('should be submittable', fakeAsync(() => {
        expect(submitButton.disabled).toBeFalse();
      }));

      it('when back-end is ok should submit and redirect to listing', fakeAsync(() => {
        spectator.click(submitButton);

        TestBed.inject(HttpTestingController)
          .expectOne(req => !!req.url.match(/lines/))
          .flush(LINE);

        spectator.tick();
        const url = spectator.inject(Router).url;

        expect(url).toBe('/');
      }));

      it('when back-end fails should show errors', fakeAsync(() => {
        const errorMessage = 'Something went wrong';
        spectator.click(submitButton);

        TestBed.inject(HttpTestingController)
          .expectOne(req => !!req.url.match(/lines/))
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
            useValue: { params: of({ identification: LINE.identification }) }
          }
        ]
      });

      TestBed.inject(HttpTestingController)
        .expectOne(req => !!req.url.match(/lines/))
        .flush(LINE);

      spectator.tick();
    }));

    it('should create', () => {
      expect(spectator.component).toBeTruthy();
    });

    it('should populate form base on the url identifier', () => {
      const form = {
        id: spectator.component.id,
        identification: (spectator.query('#identification') as HTMLInputElement).value,
        fromwards: (spectator.query('#fromwards') as HTMLInputElement).value,
        towards: (spectator.query('#towards') as HTMLInputElement).value,
        departuresPerDay: (spectator.query('#departuresPerDay') as HTMLInputElement).value,
      };

      expect(form).toEqual(jasmine.objectContaining({
        id: LINE.id,
        identification: LINE.identification,
        fromwards: LINE.fromwards,
        towards: LINE.towards,
        departuresPerDay: LINE.departuresPerDay.toString()
      }));
    });

    it('should have delete button visible', () => {
      const deleteButton = spectator.query(byTestId('delete-button'));

      expect(deleteButton).toBeTruthy();
    });
  });
});


