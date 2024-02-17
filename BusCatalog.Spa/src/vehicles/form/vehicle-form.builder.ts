import { FormControl, FormGroup, Validators } from "@angular/forms";
import { VehicleForm } from "./vehicle-form.interface";

const MIN_LENGTH = 4;
const MAX_LENGTH = 50;

export const FORM = new FormGroup<VehicleForm>({
  identification: new FormControl('',
    {
      nonNullable: true,
      validators: [
        Validators.required,
        Validators.minLength(MIN_LENGTH),
        Validators.maxLength(MAX_LENGTH)
      ]
    }
  ),
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
