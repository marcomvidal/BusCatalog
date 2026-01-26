import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { Line } from '../line';
import { LinesService } from '../services/lines.service';
import { BaseModule } from '../../base/base.module';
import { SearchFilterable } from 'base/pipes/search-filter/search-filterable.interface';

@Component({
  selector: 'app-lines-listing',
  standalone: true,
  imports: [RouterModule, BaseModule],
  providers: [LinesService],
  templateUrl: './lines-listing.component.html',
  styleUrl: './lines-listing.component.css'
})
export class LinesListingComponent implements OnInit, SearchFilterable {
  lines: Line[] = [];
  searchTerm = '';

  constructor(private service: LinesService) {}

  ngOnInit() {
    this.service.getAll().subscribe(lines => this.lines = lines);
  }

  onSearchTermChanges(searchTerm: string) {
    this.searchTerm = searchTerm;
  }
}
