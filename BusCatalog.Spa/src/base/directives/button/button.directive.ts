import { Directive, HostBinding } from '@angular/core';

@Directive({
  selector: '[busButton]'
})
export class ButtonDirective {
  @HostBinding('class')
  elementClass = 'bg-teal-500 hover:bg-teal-700 text-white font-bold py-2 px-4 rounded';
}
