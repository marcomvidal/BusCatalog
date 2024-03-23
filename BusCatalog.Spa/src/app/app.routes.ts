import { Routes } from '@angular/router';
import { LinesListingComponent } from '../lines/listing/lines-listing.component';
import { VehiclesListingComponent } from '../vehicles/listing/vehicles-listing.component';
import { VehicleFormComponent } from '../vehicles/form/vehicle-form.component';
import { PlacesListingComponent } from 'places/listing/places-listing.component';
import { PlaceFormComponent } from 'places/form/place-form.component';

export const routes: Routes = [
  { path: '', component: LinesListingComponent },
  { path: 'vehicles', component: VehiclesListingComponent },
  { path: 'vehicles/edit/:identification', component: VehicleFormComponent },
  { path: 'vehicles/create', component: VehicleFormComponent },
  { path: 'places', component: PlacesListingComponent },
  { path: 'places/edit/:identification', component: PlaceFormComponent },
  { path: 'places/create', component: PlaceFormComponent }
];
