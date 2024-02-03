import { Directive, HostBinding } from '@angular/core';

@Directive({
  selector: '[busInput]'
})
export class InputDirective {
  @HostBinding('class')
  elementClass = 'appearance-none block w-full bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 leading-tight focus:outline-none focus:bg-white focus:border-gray-500';
}
