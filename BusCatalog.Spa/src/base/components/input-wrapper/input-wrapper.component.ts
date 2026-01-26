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
  form!: FormGroup;

  @Input()
  name = '';

  @Input()
  messages: Record<string, string> = {};

  get control() {
    return this.form.get(this.name);
  }

  get errors() {
    if (!this.control || !this.control.errors || !this.control.dirty) {
      return [];
    }
    
    return Object
      .keys(this.control?.errors!)
      .map(error => Object.hasOwn(this.messages, error) ? this.messages[error] : null);
  }
}
