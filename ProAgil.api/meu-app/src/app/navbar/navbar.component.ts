import { Component, OnInit } from '@angular/core';
import { Route,Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';
@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  constructor(public router: Router,public auth: AuthService) { }
 
  ngOnInit() {
  }
  loggedIn(){
    
    return this.auth.loggedIn()
  }
  logout(){
    localStorage.removeItem("token")
    this.router.navigate(['/users/login'])
  }
}
