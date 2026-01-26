export class ResponseError {
  constructor(
    public isUnexpectedError = false,
    public validationErrors: Record<string, string[]>[] = []) {}

  get hasErrors() {
    return this.isUnexpectedError || Object.keys(this.validationErrors).length > 0;
  }

  static fromValidationErrors(errors: Record<string, string[]>[]) {
    return new ResponseError(false, errors);
  }

  static fromUnexpectedError() {
    return new ResponseError(true);
  }
}
