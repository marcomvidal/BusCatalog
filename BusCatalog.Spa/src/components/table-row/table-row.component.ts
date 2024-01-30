import { Component } from '@angular/core';

@Component({
  selector: '[bus-table-row]',
  standalone: true,
  imports: [],
  templateUrl: './table-row.component.html',
  host: { 'class': 'odd:bg-white even:bg-gray-100 hover:bg-gray-100' }
})
export class TableRowComponent {}
