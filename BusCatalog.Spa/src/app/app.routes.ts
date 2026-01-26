import { Routes } from '@angular/router';
import { LinesListingComponent } from '../lines/listing/lines-listing.component';
import { VehiclesListingComponent } from '../vehicles/listing/vehicles-listing.component';
import { VehicleFormComponent } from '../vehicles/form/vehicle-form.component';
import { LineFormComponent } from 'lines/form/line-form.component';

export const routes: Routes = [
  { path: '', component: LinesListingComponent },
  { path: 'lines', component: LinesListingComponent },
  { path: 'lines/create', component: LineFormComponent },
  { path: 'lines/edit/:identification', component: LineFormComponent },
  { path: 'vehicles', component: VehiclesListingComponent },
  { path: 'vehicles/edit/:identification', component: VehicleFormComponent },
  { path: 'vehicles/create', component: VehicleFormComponent }
];
