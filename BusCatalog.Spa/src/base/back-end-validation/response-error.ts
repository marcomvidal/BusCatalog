import { ValidationError } from "./validation-error";

export class ResponseError {
  constructor(
    public isUnexpectedError = false,
    public validationErrors: ValidationError[] = []) {}

  get hasErrors() {
    return this.isUnexpectedError || Object.keys(this.validationErrors).length > 0;
  }

  static fromValidationErrors(errors: ValidationError[]) {
    return new ResponseError(false, errors);
  }

  static fromUnexpectedError() {
    return new ResponseError(true);
  }
}
