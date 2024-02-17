import { Component, Input } from '@angular/core';
import { ResponseError } from 'base/back-end-validation/response-error';

@Component({
  selector: 'bus-back-end-alert',
  templateUrl: './back-end-alert.component.html'
})
export class BackEndAlertComponent {
  static readonly BASE_STYLE = 'bg-gray-100 border-gray-500 text-gray-900 border-t-4 rounded-b px-4 py-3 shadow-md mb-8';
  
  @Input()
  error = new ResponseError();

  get color() {
    return this.error.isUnexpectedError ? 'red' : 'teal';
  }

  get validationErrors() {
    return Object.values(this.error.validationErrors);
  }

  get computedElementClass() {
    return BackEndAlertComponent.BASE_STYLE.replaceAll('gray', this.color);
  }
}
