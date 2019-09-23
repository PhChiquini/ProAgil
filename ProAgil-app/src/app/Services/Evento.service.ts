import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Evento } from '../Models/Evento';

@Injectable({
  providedIn: 'root'
})
export class EventoService {

  baserURL = 'http://localhost:5000/api/evento';

  constructor(private http: HttpClient) { }

  getAllEvento(): Observable<Evento[]> {
    return this.http.get<Evento[]>(this.baserURL);
  }

  getEventoByTema(tema: string): Observable<Evento[]> {
    return this.http.get<Evento[]>(`${this.baserURL}/getByTema/${tema}`);
  }

  getEventoById(id: number): Observable<Evento> {
    return this.http.get<Evento>(`${this.baserURL}/${id}`);
  }

  postUpload(file: File, name: string){

    const fileToUpload = file[0] as File;
    const formData = new FormData();

    formData.append('file', fileToUpload, name);

    return this.http.post(`${this.baserURL}/upload`, formData);
  }

  postEvento(evento: Evento) {
    return this.http.post(this.baserURL, evento);
  }

  putEvento(id: number, evento: Evento) {
    return this.http.put(`${this.baserURL}/${id}`, evento);
  }

  deleteEvento(id: number) {
    return this.http.delete(`${this.baserURL}/${id}`);
  }
}
