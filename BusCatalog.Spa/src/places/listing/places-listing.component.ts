import { Component, OnInit } from '@angular/core';
import { PlacesService } from '../services/places.service';
import { RouterModule } from '@angular/router';
import { BaseModule } from '../../base/base.module';
import { take } from 'rxjs';
import { FormsModule } from '@angular/forms';
import { Place } from 'places/place';

@Component({
  selector: 'app-places-listing',
  standalone: true,
  imports: [RouterModule, BaseModule, FormsModule],
  templateUrl: './places-listing.component.html'
})
export class PlacesListingComponent implements OnInit {
  places: Place[] = [];
  searchTerm = '';

  constructor(private service: PlacesService) {}

  ngOnInit() {
    this.service.getAll()
      .pipe(take(1))
      .subscribe(places => this.places = this.places = places);
  }

  onSearchTermChanges(searchTerm: string) {
    this.searchTerm = searchTerm;
  }
}
