import { Directive, HostBinding } from '@angular/core';

@Directive({
  selector: '[busFormFieldsWrapper]'
})
export class FormFieldsWrapperDirective {
  @HostBinding('class')
  elementClass = 'mb-8 w-full';
}

