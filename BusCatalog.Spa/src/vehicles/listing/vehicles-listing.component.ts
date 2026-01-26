import { Component, OnInit } from '@angular/core';
import { Vehicle } from '../vehicle';
import { VehiclesService } from '../services/vehicles.service';
import { RouterModule } from '@angular/router';
import { BaseModule } from '../../base/base.module';
import { take } from 'rxjs';
import { FormsModule } from '@angular/forms';
import { SearchFilterable } from 'base/pipes/search-filter/search-filterable.interface';

@Component({
  selector: 'app-vehicles-listing',
  standalone: true,
  imports: [RouterModule, BaseModule, FormsModule],
  templateUrl: './vehicles-listing.component.html'
})
export class VehiclesListingComponent implements OnInit, SearchFilterable {
  vehicles: Vehicle[] = [];
  searchTerm = '';

  constructor(private service: VehiclesService) {}

  ngOnInit() {
    this.service.getAll()
      .pipe(take(1))
      .subscribe(vehicles => this.vehicles = vehicles);
  }

  onSearchTermChanges(searchTerm: string) {
    this.searchTerm = searchTerm;
  }
}
