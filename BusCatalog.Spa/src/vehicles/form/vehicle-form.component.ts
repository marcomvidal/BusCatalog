import { Component, OnInit } from '@angular/core';
import { BaseModule } from '../../base/base.module';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { VehiclesService } from '../services/vehicles.service';
import { CommonModule } from '@angular/common';
import { BackEndValidationService } from 'base/back-end-validation/back-end-validation.service';
import { Vehicle } from 'vehicles/vehicle';
import { VehicleFormValidation } from './vehicle-form.validation';
import { filter, switchMap, take } from 'rxjs';
import { VehicleForm } from './vehicle-form.interface';

@Component({
  selector: 'app-vehicle-form',
  standalone: true,
  imports: [BaseModule, ReactiveFormsModule, CommonModule],
  templateUrl: './vehicle-form.component.html'
})
export class VehicleFormComponent implements OnInit {
  id?: number;
  form: FormGroup<VehicleForm>;
  messages = VehicleFormValidation.MESSAGES;

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    public service: VehiclesService,
    public validator: BackEndValidationService) {
      this.form = VehicleFormValidation.generate();
    }

  ngOnInit() {
    this.validator.reset();
    this.fillFormOnPut();
  }

  onSubmit() {
    const vehicle = Vehicle.fromForm(this.form);

    this.service.save(vehicle, this.id)
      .subscribe({
        next: _ => this.router.navigate(['vehicles']),
        error: error => this.validator.handleError(error)
      });
  }

  onDelete($event: MouseEvent) {
    $event.preventDefault();
    
    this.service.delete(this.id!)
      .subscribe({
        next: _ => this.router.navigate(['vehicles']),
        error: error => this.validator.handleError(error)
      })
  }

  private fillFormOnPut() {
    this.activatedRoute.params
      .pipe(
        take(1),
        filter(params => params['identification']),
        switchMap(params => this.service.getByIdentification(params['identification'])))
      .subscribe(({ id, description }) => {
        this.id = id;
        this.form.setValue({ description });
      });
  }
}
