import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Place } from 'places/place';

@Injectable({
  providedIn: 'root'
})
export class PlacesService {
  static readonly BASE_URL = `${environment.apiUrl}/places`;

  constructor(private http: HttpClient) {}

  getAll() {
    return this.http.get<Place[]>(PlacesService.BASE_URL);
  }

  getByIdentification(identification: string) {
    return this.http.get<Place>(`${PlacesService.BASE_URL}/${identification}`);
  }

  save(place: Place, id?: number) {
    return id === undefined
      ? this.http.post(PlacesService.BASE_URL, place)
      : this.http.put(`${PlacesService.BASE_URL}/${id}`, place);
  }
}
