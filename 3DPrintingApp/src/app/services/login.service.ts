import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class LoginService {

  public url = 'https://localhost:44308/api/Account/login';
  public url1 = 'https://localhost:44308/api/User/GetUserRole/';


  constructor(
    public http: HttpClient,
    
  ) { }

  
  public addLogin(log): Observable<any> {

    return this.http.post(`${this.url}`,log);
  }
  public getRole(email): Observable<any> {

    return this.http.get(`${this.url1}` + email);
  }
}
