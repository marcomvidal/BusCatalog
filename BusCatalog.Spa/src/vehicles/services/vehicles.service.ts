import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Vehicle } from '../vehicle';

@Injectable({
  providedIn: 'root'
})
export class VehiclesService {
  constructor(private http: HttpClient) {}

  getAll() {
    return this.http.get<Vehicle[]>(`${environment.apiUrl}/vehicles`);
  }
}
