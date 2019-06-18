import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router,Route } from '@angular/router';
import { Observable } from 'rxjs';
import { CanActivate } from '@angular/router/src/utils/preactivation';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  path: ActivatedRouteSnapshot[];  route: ActivatedRouteSnapshot;
  constructor(private router:Router){

  }

  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot):boolean{
    if(localStorage.getItem("token") !== null){
      return true
    }else{
      this.router.navigate(["/users/login"])
      return false
    }
  }
  
}
