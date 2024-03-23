import { Directive, HostBinding } from '@angular/core';

@Directive({
  selector: '[busWarningButton]'
})
export class WarningButtonDirective {
  @HostBinding('class')
  elementClass = 'bg-red-500 hover:bg-red-700 text-white font-bold py-2 px-4 rounded mt-2 w-full';
}