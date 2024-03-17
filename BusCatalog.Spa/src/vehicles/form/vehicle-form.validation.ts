import { FormControl, FormGroup, Validators } from "@angular/forms";
import { VehicleForm } from "./vehicle-form.interface";
import { Vehicle } from "vehicles/vehicle";

const MIN_LENGTH = 4;
const MAX_LENGTH = 50;

export class VehicleFormValidation {
  static MESSAGES: Record<string, string> = {
    required: 'This field is required.',
    minlength: `This field must have at least ${MIN_LENGTH} characters.`,
    maxlength: `This field must have a maximum of ${MAX_LENGTH} characters.`,
  };

  static generate(vehicle?: Vehicle) {
    return new FormGroup<VehicleForm>({
      description: new FormControl('',
        {
          nonNullable: true,
          validators: [
            Validators.required,
            Validators.minLength(MIN_LENGTH),
            Validators.maxLength(MAX_LENGTH)
          ]
        }
      ),
    });
  }
}

export function generateForm() {
  return new FormGroup<VehicleForm>({
    description: new FormControl('',
      {
        nonNullable: true,
        validators: [
          Validators.required,
          Validators.minLength(MIN_LENGTH),
          Validators.maxLength(MAX_LENGTH)
        ]
      }
    ),
  });
}

export const VALIDATION_MESSAGES: Record<string, string> = {
  required: 'This field is required.',
  minlength: `This field must have at least ${MIN_LENGTH} characters.`,
  maxlength: `This field must have a maximum of ${MAX_LENGTH} characters.`,
};
