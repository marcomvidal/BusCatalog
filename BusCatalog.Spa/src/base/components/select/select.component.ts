import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'bus-select',
  templateUrl: './select.component.html'
})
export class SelectComponent {
  selectedItems: string[] = [];

  @Input()
  id = '';

  @Input()
  name = '';

  @Input()
  collection: any[] = [];

  @Input()
  displayProperty = '';

  @Input()
  valueProperty = '';

  @Output()
  onItemSelectedChanges = new EventEmitter<string[]>();

  onItemSelect($event: string[]) {
    this.onItemSelectedChanges.emit($event);
  }
}
