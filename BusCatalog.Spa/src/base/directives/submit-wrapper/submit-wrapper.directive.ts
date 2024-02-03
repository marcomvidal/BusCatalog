import { Directive, HostBinding } from '@angular/core';

@Directive({
  selector: '[busSubmitWrapper]'
})
export class SubmitWrapperDirective {
  @HostBinding('class')
  elementClass = 'block w-full mt-6';
}

