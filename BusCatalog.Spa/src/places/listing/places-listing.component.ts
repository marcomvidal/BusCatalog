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
  filteredPlaces: Place[] = [];

  constructor(private service: PlacesService) {}

  ngOnInit() {
    this.service.getAll()
      .pipe(take(1))
      .subscribe(places => this.places = this.filteredPlaces = places);
  }

  onSearchChanges(search: string) {
    this.filteredPlaces = search.length > 0
      ? this.places.filter(
          place => place.identification.toLowerCase().startsWith(search.toLowerCase()))
      : this.places;
  }
}
