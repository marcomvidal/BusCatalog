import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Line } from '../line';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LinesService {
  constructor(private http: HttpClient) { }

  getAll() {
    return this.http.get<Line[]>(`${environment.apiUrl}/lines`);
  }
}
