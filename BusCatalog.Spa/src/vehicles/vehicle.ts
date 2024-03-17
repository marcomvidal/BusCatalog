import { FormGroup } from "@angular/forms";
import { VehicleForm } from "./form/vehicle-form.interface";

export class Vehicle {
  constructor(public description: string = '', public id?: number) {}

  public get identification() {
    return this.description
      .trim()
      .toUpperCase()
      .normalize('NFD')
      .replace(/[\u0300-\u036f]/g, '')
      .replace(' ', '_');
  }

  public withForm(form: FormGroup<VehicleForm>) {
    this.description = form.get('description')?.value!;
  }

  static fromRequest(vehicle: Vehicle) {
    return new Vehicle(vehicle.description, vehicle.id);
  }
}
