import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/_models/User';
import { AuthService } from 'src/app/_services/auth.service';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-registration-component',
  templateUrl: './registration-component.component.html',
  styleUrls: ['./registration-component.component.css']
})
export class RegistrationComponentComponent implements OnInit {

  registerForm: FormGroup
  user: User
  constructor(public fb:FormBuilder,private toastr: ToastrService,public auth: AuthService,public router:Router) {
    this.validation()
   }

  ngOnInit() {
  }

  validation(){
    this.registerForm= this.fb.group({
      FullName:['',Validators.required],
      Email:['',Validators.required],
      UserName:['',Validators.required],
      passwords:this.fb.group({
        password:['',Validators.required],
        confirmPassword:['',Validators.required],
      },{validator: this.compararSenhas}),
      
    })
  }
  compararSenhas(fb: FormGroup){
    const confirmar= fb.get("confirmPassword")
    if(confirmar.errors == null || 'mismatch' in confirmar.errors){
        if(fb.get("password").value !== confirmar.value){
          confirmar.setErrors({mismatch: true})
        }else{
          confirmar.setErrors(null)
        }
    }
  }
  cadastrarUsuario(){
    if(this.registerForm.valid){
      this.user= Object.assign({Password: this.registerForm.get("passwords.password").value},this.registerForm.value)
      this.auth.register(this.user).subscribe((resp)=>{
          this.router.navigate(['/users/login'])
      },error=>{
        console.log(error)
      })
    }
  }
}
