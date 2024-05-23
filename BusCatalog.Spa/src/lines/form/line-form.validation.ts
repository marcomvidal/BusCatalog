import { FormControl, FormGroup, Validators } from "@angular/forms";
import { LineForm } from "./line-form.interface";
import { DEFAULT_MESSAGES } from "base/front-end-validation/default-messages";

const MIN_LENGTH = 2;
const MAX_LENGTH = 50;

export class LineFormValidation {
  static MESSAGES: Record<string, string> = {
    ...DEFAULT_MESSAGES,
    minlength: `This field must have at least ${MIN_LENGTH} characters.`,
    maxlength: `This field must have a maximum of ${MAX_LENGTH} characters.`,
  };

  static generate() {
    return new FormGroup<LineForm>({
      identification: new FormControl('',
        {
          nonNullable: true,
          validators: [
            Validators.required,
            Validators.minLength(MIN_LENGTH),
            Validators.maxLength(MAX_LENGTH)
          ]
        }),
      fromwards: new FormControl('',
        {
          nonNullable: true,
          validators: [
            Validators.required,
            Validators.minLength(MIN_LENGTH),
            Validators.maxLength(MAX_LENGTH)
          ]
        }),
      towards: new FormControl('',
        {
          nonNullable: true,
          validators: [
            Validators.required,
            Validators.minLength(MIN_LENGTH),
            Validators.maxLength(MAX_LENGTH)
          ]
        }),
      departuresPerDay: new FormControl(0,
        {
          nonNullable: true,
          validators: [
            Validators.required,
            Validators.minLength(MIN_LENGTH),
            Validators.maxLength(MAX_LENGTH)
          ]
        }),
      vehicles: new FormControl([], { nonNullable: true })
    });
  }
}
