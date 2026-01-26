import { Component, Input } from '@angular/core';
import { ResponseError } from 'base/back-end-validation/response-error';

@Component({
  selector: 'bus-back-end-alert',
  templateUrl: './back-end-alert.component.html'
})
export class BackEndAlertComponent {
  static readonly UNEXPECTED_ERROR_STYLE = 'bg-red-100 border-red-500 text-red-900 border-t-4 rounded-b px-4 py-3 shadow-md mb-8';
  static readonly EXPECTED_ERROR_STYLE = 'bg-teal-100 border-teal-500 text-teal-900 border-t-4 rounded-b px-4 py-3 shadow-md mb-8';
  
  @Input()
  error = new ResponseError();

  get color() {
    return this.error.isUnexpectedError ? 'red' : 'teal';
  }

  get validationErrors() {
    return Object.values(this.error.validationErrors);
  }

  get computedElementClass() {
    return this.error.isUnexpectedError
      ? BackEndAlertComponent.UNEXPECTED_ERROR_STYLE
      : BackEndAlertComponent.EXPECTED_ERROR_STYLE;
  }
}
