import { Component, OnInit } from '@angular/core';

import { Line } from '../line';
import { TableComponent } from '../../components/table/table.component';
import { TableRowComponent } from '../../components/table-row/table-row.component';
import { TableCellComponent } from '../../components/table-cell/table-cell.component';
import { LinesService } from '../services/lines.service';

@Component({
  selector: 'app-lines-listing',
  standalone: true,
  imports: [TableComponent, TableRowComponent, TableCellComponent],
  providers: [LinesService],
  templateUrl: './lines-listing.component.html',
  styleUrl: './lines-listing.component.css'
})
export class LinesListingComponent implements OnInit {
  lines: Line[] = [];

  constructor(private linesService: LinesService) {}

  ngOnInit() {
    this.linesService.getAll().subscribe(lines => this.lines = lines);
  }
}
