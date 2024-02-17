import { FormGroup } from "@angular/forms";
import { VehicleForm } from "./form/vehicle-form.interface";

export class Vehicle {
  constructor(
    public identification: string,
    public description: string) {}

  public static fromForm(form: FormGroup<VehicleForm>) {
    return new Vehicle(
      form.value.identification!,
      form.value.description!);
  }
}
