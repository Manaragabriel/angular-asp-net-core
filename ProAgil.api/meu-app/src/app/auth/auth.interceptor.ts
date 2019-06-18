import { Injectable } from "@angular/core";
import { HttpInterceptor } from '@angular/common/http';
import { Router,Route } from '@angular/router';
import { tap } from 'rxjs/internal/operators/tap';

@Injectable({
    providedIn: 'root'
})

export class AuthInterceptor implements HttpInterceptor{
    intercept(req: import("@angular/common/http").HttpRequest<any>, next: import("@angular/common/http").HttpHandler): import("rxjs").Observable<import("@angular/common/http").HttpEvent<any>> {
        if(localStorage.getItem("token") !== null){
            const clone= req.clone({headers: req.headers.set('Authorization',`Bearer ${localStorage.getItem("token")}`)})
            return next.handle(clone).pipe(
                tap(
                    succ=>{},
                    err=>{
                        if (err.status=== 401){
                            this.router.navigateByUrl('/users/login')
                        }
                    }
                )
            )
        }else{
            
            return next.handle(req.clone())
          }
    }
    constructor(private router: Router){

    }


}