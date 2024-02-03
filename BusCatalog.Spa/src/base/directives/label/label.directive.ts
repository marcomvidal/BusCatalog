import { Directive, HostBinding } from '@angular/core';

@Directive({
  selector: '[busLabel]'
})
export class LabelDirective {
  @HostBinding('class')
  elementClass = 'block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2';
}
