import { ValidationError } from "./validation-error";

export interface BackEndValidationErrors {
  type: string,
  title: string,
  status: number,
  errors: ValidationError[]
}
