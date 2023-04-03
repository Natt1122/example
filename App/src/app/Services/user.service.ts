import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Login } from '../Interfaces/login';
import { User } from '../Interfaces/user';
import { ResponseApi } from '../Interfaces/response-api';




@Injectable({
  providedIn: 'root'
})
export class UserService {
  private urlApi:string=environment.endpoint+"User/";



  constructor(private http:HttpClient) { }

  Login(request: Login): Observable<ResponseApi>{
    return this.http.post<ResponseApi>(`${this.urlApi}Login`,request)
  }
  ListUser(): Observable<ResponseApi>{
    return this.http.get<ResponseApi>(`${this.urlApi}ListUser`)
  }
  Save(request: User): Observable<ResponseApi>{
    return this.http.post<ResponseApi>(`${this.urlApi}Save`,request)
  }
  Update(request: User): Observable<ResponseApi>{
    return this.http.put<ResponseApi>(`${this.urlApi}Update`,request)
  }
  Delete( id: number ): Observable<ResponseApi>{
    return this.http.delete<ResponseApi>(`${this.urlApi}Delete/${id}`)
  }



}
