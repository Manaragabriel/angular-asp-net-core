import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http'
import {JwtHelperService} from '@auth0/angular-jwt'
import {map} from 'rxjs/operators'
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  url= "http://localhost:5000/api/user/"
  jwtHelper= new JwtHelperService()
  token: any
  constructor(private http: HttpClient) { }

  login(model:any){
    return this.http.post(`${this.url}Login`,model).pipe(
      map((response:any)=>{
        console.log(response)
        if(response){
          localStorage.setItem('token',response.token)
          
          this.token= this.jwtHelper.decodeToken(response.token)
          
        }
      })
    )
  }

  register(model: any){
    return this.http.post(`${this.url}Register`,model)
  }

  loggedIn(){
    const token= localStorage.getItem("token")
    var bool= this.jwtHelper.isTokenExpired(token)
    
    return !bool
  }
}
