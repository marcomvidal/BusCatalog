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

  getByIdentification(identification: string) {
    return this.http.get<Vehicle>(`${VehiclesService.BASE_URL}/${identification}`);
  }

  save(vehicle: Vehicle) {
    const request = {
      identification: vehicle.identification,
      description: vehicle.description
    };
    
    return vehicle.id === undefined
      ? this.http.post(VehiclesService.BASE_URL, request)
      : this.http.put(`${VehiclesService.BASE_URL}/${vehicle.id}`, request);
  }
}
