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
  templateUrl: './vehicle-form.component.html',
  styleUrl: './vehicle-form.component.css'
})
export class VehicleFormComponent implements OnInit {
  form?: FormGroup<VehicleForm>;
  messages = VehicleFormValidation.MESSAGES;
  vehicle = new Vehicle();

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private service: VehiclesService,
    public validator: BackEndValidationService) {}

  ngOnInit() {
    this.form = VehicleFormValidation.generate();
    this.validator.reset();
    this.fillFormOnPut();
  }

  onSubmit() {
    this.vehicle?.withForm(this.form!);
    console.log(this.vehicle);

    this.service.save(this.vehicle!).subscribe({
      next: _ => this.router.navigate(['vehicles']),
      error: error => this.validator.handleError(error)
    });
  }

  private fillFormOnPut() {
    this.activatedRoute.params
      .pipe(
        take(1),
        filter(params => params['id']),
        switchMap(params => this.service.getByIdentification(params['id'])))
      .subscribe(vehicle => {
        console.log('request made');
        this.vehicle = Vehicle.fromRequest(vehicle);
        this.form!.setValue({ description: this.vehicle.description });
      });
  }
}
