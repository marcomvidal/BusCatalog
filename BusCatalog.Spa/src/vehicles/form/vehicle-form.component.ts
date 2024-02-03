import { Component } from '@angular/core';
import { BaseModule } from '../../base/base.module';

@Component({
  selector: 'app-vehicle-form',
  standalone: true,
  imports: [BaseModule],
  templateUrl: './vehicle-form.component.html',
  styleUrl: './vehicle-form.component.css'
})
export class VehicleFormComponent {}
