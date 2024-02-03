import { Directive, HostBinding } from '@angular/core';

@Directive({
  selector: '[busButton]'
})
export class ButtonDirective {
  @HostBinding('class')
  elementClass = 'bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded';
}
