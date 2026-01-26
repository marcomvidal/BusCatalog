import { FormControl, FormGroup, Validators } from "@angular/forms";
import { VehicleForm } from "./vehicle-form.interface";
import { DEFAULT_MESSAGES } from "base/front-end-validation/default-messages";

const MIN_LENGTH = 4;
const MAX_LENGTH = 50;

export class VehicleFormValidation {
  static MESSAGES: Record<string, string> = {
    ...DEFAULT_MESSAGES,
    minlength: `This field must have at least ${MIN_LENGTH} characters.`,
    maxlength: `This field must have a maximum of ${MAX_LENGTH} characters.`,
  };

  static generate() {
    return new FormGroup<VehicleForm>({
      description: new FormControl('',
        {
          nonNullable: true,
          validators: [
            Validators.required,
            Validators.minLength(MIN_LENGTH),
            Validators.maxLength(MAX_LENGTH)
          ]
        }),
    });
  }
}
