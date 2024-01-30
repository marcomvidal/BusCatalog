import { Component, Input } from '@angular/core';

@Component({
  selector: 'bus-table',
  standalone: true,
  imports: [],
  templateUrl: './table.component.html',
  styleUrl: './table.component.css'
})
export class TableComponent {
  @Input()
  headers: string[] = [];
}
