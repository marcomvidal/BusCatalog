import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Line } from '../line';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LinesService {
  static readonly BASE_URL = `${environment.apiUrl}/lines`;

  constructor(private http: HttpClient) {}

  getAll() {
    return this.http.get<Line[]>(LinesService.BASE_URL);
  }

  getById(id: string) {
    return this.http.get<Line>(`${LinesService.BASE_URL}/${id}`);
  }

  save(place: Line, id?: number) {
    return id === undefined
      ? this.http.post(LinesService.BASE_URL, place)
      : this.http.put(`${LinesService.BASE_URL}/${id}`, place);
  }

  delete(id: number) {
    return this.http.delete(`${LinesService.BASE_URL}/${id}`);
  }
}
