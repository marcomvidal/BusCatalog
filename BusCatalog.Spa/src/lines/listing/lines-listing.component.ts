import { Component, OnInit } from '@angular/core';

import { Line } from '../line';
import { LinesService } from '../services/lines.service';
import { BaseModule } from '../../base/base.module';

@Component({
  selector: 'app-lines-listing',
  standalone: true,
  imports: [BaseModule],
  providers: [LinesService],
  templateUrl: './lines-listing.component.html',
  styleUrl: './lines-listing.component.css'
})
export class LinesListingComponent implements OnInit {
  lines: Line[] = [];

  constructor(private service: LinesService) {}

  ngOnInit() {
    this.service.getAll().subscribe(lines => this.lines = lines);
  }
}
