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

  save(vehicle: Vehicle, id?: number) {
    return id === undefined
      ? this.http.post(VehiclesService.BASE_URL, vehicle)
      : this.http.put(`${VehiclesService.BASE_URL}/${id}`, vehicle);
  }

  delete(id: number) {
    return this.http.delete(`${VehiclesService.BASE_URL}/${id}`);
  }
}
