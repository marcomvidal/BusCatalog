import { Routes } from '@angular/router';
import { LinesListingComponent } from '../lines/listing/lines-listing.component';
import { VehiclesListingComponent } from '../vehicles/listing/vehicles-listing.component';
import { VehicleFormComponent } from '../vehicles/form/vehicle-form.component';

export const routes: Routes = [
  { path: '', component: LinesListingComponent },
  { path: 'vehicles', component: VehiclesListingComponent },
  { path: 'vehicles/edit/:id', component: VehicleFormComponent },
  { path: 'vehicles/create', component: VehicleFormComponent }
];
