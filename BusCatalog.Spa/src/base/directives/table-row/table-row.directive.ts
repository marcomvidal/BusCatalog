import { Directive, HostBinding } from '@angular/core';

@Directive({
  selector: '[busTableRow]'
})
export class TableRowDirective {
  @HostBinding('class')
  elementClass = 'hover:bg-gray-100';
}
