import { Component, HostBinding, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'bus-input-wrapper',
  templateUrl: './input-wrapper.component.html'
})
export class InputWrapperComponent {
  @HostBinding('class')
  elementClass = 'block w-full mb-6';

  @Input()
  form?: FormGroup;

  @Input()
  name = '';

  get errors() {
    return this.form?.get(this.name)
      ? Object.keys(this.form?.get(this.name)?.errors!)
      : [];
  }
}
