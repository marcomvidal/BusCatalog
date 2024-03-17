import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'bus-search-input',
  templateUrl: './search-input.component.html'
})
export class SearchInputComponent {
  value = '';

  @Output()
  onValueChanges = new EventEmitter<string>();

  onChange() {
    this.onValueChanges.emit(this.value);
  }
}