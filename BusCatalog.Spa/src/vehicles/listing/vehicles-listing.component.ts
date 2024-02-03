import { Component, OnInit } from '@angular/core';
import { Vehicle } from '../vehicle';
import { VehiclesService } from '../services/vehicles.service';
import { RouterModule } from '@angular/router';
import { BaseModule } from '../../base/base.module';

@Component({
  selector: 'app-vehicles-listing',
  standalone: true,
  imports: [RouterModule, BaseModule],
  templateUrl: './vehicles-listing.component.html',
  styleUrl: './vehicles-listing.component.css'
})
export class VehiclesListingComponent implements OnInit {
  vehicles: Vehicle[] = [];

  constructor(private service: VehiclesService) {}

  ngOnInit() {
    this.service.getAll().subscribe(vehicles => this.vehicles = vehicles);
  }
}
