import { Component, OnInit } from '@angular/core';

import { FormBuilder,FormGroup,Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Login } from 'src/app/Interfaces/login';
import { UserService } from 'src/app/Services/user.service';
import { UtilityService } from 'src/app/reusable/utility.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  formLogin:FormGroup;
  hidePassword:boolean=true;
  showLoading:boolean=false;
  
  constructor(

    private fb:FormBuilder,
    private router:Router,
    private _userervice:UserService,
    private _utilityService:UtilityService
   
  ) {
      this.formLogin=this.fb.group({
        email:['',Validators.required],
        password:['',Validators.required]
      });
  }

  ngOnInit(): void {
  }

  IniSesion(){

    this.showLoading=true;

    const request :Login ={
      email:this.formLogin.value.email,
      password:this.formLogin.value.password
    }
    this._userervice.Login(request).subscribe({
      next: (data)=>{
        if(data.status){
          this._utilityService.saveUserSession(data.value);
          this.router.navigate(["Pages"])
        }else this._utilityService.ShowAlert ("No se encontraron coincidencias", "Opps!")
      },
      complete:()=>{
        this.showLoading =false;
      },
      error: ()=>{
        this._utilityService.ShowAlert("Hubo un error","Opps!")
      }
    })
  }

}
