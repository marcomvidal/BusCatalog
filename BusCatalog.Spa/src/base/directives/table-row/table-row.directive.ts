import { Directive, HostBinding } from '@angular/core';

@Directive({
  selector: '[busTableRow]'
})
export class TableRowDirective {
  @HostBinding('class')
  elementClass = 'odd:bg-white even:bg-gray-100 hover:bg-gray-100';
}
