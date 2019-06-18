import { Component, OnInit } from '@angular/core';
import { Route,Router } from '@angular/router';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  titulo='Login'
  model={}

  constructor(public router:Router,public auth: AuthService) { }

  ngOnInit() {
    if(localStorage.getItem('token') !== null){
      this.router.navigate(['/dashboard'])
    }
  }
  login(){
   
    this.auth.login(this.model).subscribe(()=>{
      
      this.router.navigate(['/dashboard'])
    },error=>{
      console.log(error)
    })
  }
}

