import { Component, OnInit } from '@angular/core';
import { BaseModule } from '../../base/base.module';
import { ReactiveFormsModule } from '@angular/forms';
import { FORM } from './vehicle-form.builder';
import { Router } from '@angular/router';
import { VehiclesService } from '../services/vehicles.service';
import { CommonModule } from '@angular/common';
import { BackEndValidationService } from 'base/back-end-validation/back-end-validation.service';
import { Vehicle } from 'vehicles/vehicle';

@Component({
  selector: 'app-vehicle-form',
  standalone: true,
  imports: [BaseModule, ReactiveFormsModule, CommonModule],
  templateUrl: './vehicle-form.component.html',
  styleUrl: './vehicle-form.component.css'
})
export class VehicleFormComponent implements OnInit {
  form = FORM;

  constructor(
    private router: Router,
    private service: VehiclesService,
    public validator: BackEndValidationService) {}

  ngOnInit() {
    this.validator.reset();
  }

  onSubmit() {
    const request = Vehicle.fromForm(this.form);

    this.service.post(request).subscribe({
      next: _ => this.router.navigate(['vehicles']),
      error: error => this.validator.handleError(error)
    });
  }
}
