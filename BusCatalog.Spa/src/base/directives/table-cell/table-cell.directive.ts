import { Directive, HostBinding } from '@angular/core';

@Directive({
  selector: '[busTableCell]'
})
export class TableCellDirective {
  @HostBinding('class')
  elementClass = 'px-6 py-4 whitespace-nowrap text-sm text-gray-800 hover:cursor-pointer';
}
