import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ContClientService {
  panelOpenState = false;

  public url = 'https://localhost:44308/api/Clienti/ComenziClient?mail=';
  public url2 = 'https://localhost:44308/api/Clienti/byMail?mail=';
  public url3 = 'https://localhost:44308/api/Clienti/';
  public url4 = 'https://localhost:44308/api/Comenzi';
  public url5 = 'https://localhost:44308/api/DetaliiComanda';
  public url6 = 'https://localhost:44308/api/Clienti/GetUserById/';
  public url7 = 'https://localhost:44308/api/Clienti/GetUserOrders/';
  public url8 = 'https://localhost:44308/api/Account/VerifyEmployeePassword/';
  constructor(
    public http: HttpClient,
    
  ) { }

  public getComenzi(mail): Observable<any> {
    
    return this.http.get(`${this.url + mail}`);
  }
  public getPasswordAngajat(password): Observable<any> {
    
    return this.http.get(`${this.url8 + password}`);
  }
  public getInfo(mail): Observable<any> {
    
    return this.http.get(`${this.url6 + mail}`);
  }

  public GetClientOrders(id): Observable<any> {
    
    return this.http.get(`${this.url7 + id}`);
  }

  public getClient(mail): Observable<any> {
    
    return this.http.get(`${this.url2 + mail}`);
  }
  public updateTel(user,tel): Observable<any> {
    
    
    return this.http.patch(`${this.url3 + user + '?tel=' + tel}`, tel);
  }
  
  public putComanda(comanda): Observable<any> {
    
    
    return this.http.post(`${this.url4}`, comanda);
  }

  public putDetalii(comanda): Observable<any> {


    return this.http.post('${this.url5}', comanda);
  }
}
