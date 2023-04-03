import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ResponseApi } from '../Interfaces/response-api';
import { Rol } from '../Interfaces/rol';

@Injectable({
  providedIn: 'root'
})
export class RolService {
  private urlApi:string=environment.endpoint+"Rol/";

  constructor(private http:HttpClient) { }

  List(): Observable<ResponseApi>{
    return this.http.get<ResponseApi>(`${this.urlApi}RolList`)
  }
  save(request: Rol): Observable<ResponseApi>{
    return this.http.post<ResponseApi>(`${this.urlApi}save`,request)
  }
  Update(request: Rol): Observable<ResponseApi>{
    return this.http.put<ResponseApi>(`${this.urlApi}Update`,request)
  }
  Delete(id: Rol): Observable<ResponseApi>{
    return this.http.delete<ResponseApi>(`${this.urlApi}Delete/${id}`)
  }
}
