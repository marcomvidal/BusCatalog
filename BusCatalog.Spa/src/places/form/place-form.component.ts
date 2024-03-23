import { Component, OnInit } from '@angular/core';
import { BaseModule } from '../../base/base.module';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PlacesService } from '../services/places.service';
import { CommonModule } from '@angular/common';
import { BackEndValidationService } from 'base/back-end-validation/back-end-validation.service';
import { PlaceFormValidation } from './place-form.validation';
import { filter, switchMap, take } from 'rxjs';
import { PlaceForm } from './place-form.interface';
import { Place } from 'places/place';

@Component({
  selector: 'app-place-form',
  standalone: true,
  imports: [BaseModule, ReactiveFormsModule, CommonModule],
  templateUrl: './place-form.component.html'
})
export class PlaceFormComponent implements OnInit {
  id?: number;
  form: FormGroup<PlaceForm>;
  messages = PlaceFormValidation.MESSAGES;

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private service: PlacesService,
    public validator: BackEndValidationService) {
      this.form = PlaceFormValidation.generate();
    }

  ngOnInit() {
    this.validator.reset();
    this.fillFormOnPut();
  }

  onSubmit() {
    const place = Place.fromForm(this.form);

    this.service.save(place, this.id)
      .subscribe({
        next: _ => this.router.navigate(['places']),
        error: error => this.validator.handleError(error)
      });
  }

  private fillFormOnPut() {
    this.activatedRoute.params
      .pipe(
        take(1),
        filter(params => params['identification']),
        switchMap(params => this.service.getByIdentification(params['identification'])))
      .subscribe(({ id, identification, city }) => {
        this.id = id;
        this.form.setValue({ identification, city });
      });
  }
}
