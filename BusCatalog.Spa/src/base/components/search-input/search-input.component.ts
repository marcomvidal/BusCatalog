import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'bus-search-input',
  templateUrl: './search-input.component.html'
})
export class SearchInputComponent {
  value = '';

  @Output()
  onSearchTermChanges = new EventEmitter<string>();

  onChange() {
    this.onSearchTermChanges.emit(this.value);
  }
}