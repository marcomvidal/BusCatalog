import { FormGroup } from "@angular/forms";
import { VehicleForm } from "./form/vehicle-form.interface";

export class Vehicle {
  public identification: string;

  constructor(public description: string = '', public id?: number) {
    this.identification = this.generateIdentification();
  }

  static fromForm(form: FormGroup<VehicleForm>) {
    return new Vehicle(form.get('description')?.value);
  }

  static fromRequest(vehicle: Vehicle) {
    return new Vehicle(vehicle.description, vehicle.id);
  }

  private generateIdentification() {
    return this.description
      .trim()
      .toUpperCase()
      .normalize('NFD')
      .replace(/[\u0300-\u036f]/g, '')
      .replace(' ', '_');
  }
}
