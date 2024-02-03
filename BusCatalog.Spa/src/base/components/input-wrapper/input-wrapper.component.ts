import { Component, HostBinding } from '@angular/core';

@Component({
  selector: 'bus-input-wrapper',
  templateUrl: './input-wrapper.component.html'
})
export class InputWrapperComponent {
  @HostBinding('class')
  elementClass = 'block w-full mb-6';
}
