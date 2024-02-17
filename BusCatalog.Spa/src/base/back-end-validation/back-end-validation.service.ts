import { HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BackEndValidationErrors } from "./back-end-validation-error";
import { ResponseError } from "./response-error";

@Injectable({
  providedIn: 'root'
})
export class BackEndValidationService {
  error = new ResponseError();

  reset() {
    this.error = new ResponseError();
  }

  handleError(response: HttpErrorResponse) {
    return response.status == 400
      ? this.populateValidationErrors(response.error as BackEndValidationErrors)
      : this.populateUnexpectedError();
  }

  private populateValidationErrors(errors: BackEndValidationErrors) {
    this.error = ResponseError.fromValidationErrors(errors.errors);
  }

  private populateUnexpectedError() {
    this.error = ResponseError.fromUnexpectedError();
  }
}
