import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Vehicle } from '../vehicle';

@Injectable({
  providedIn: 'root'
})
export class VehiclesService {
  static readonly BASE_URL = `${environment.apiUrl}/vehicles`;

  constructor(private http: HttpClient) {}

  getAll() {
    return this.http.get<Vehicle[]>(VehiclesService.BASE_URL);
  }

  post(vehicle: Vehicle) {
    return this.http.post(VehiclesService.BASE_URL, vehicle);
  }
}
