import { Injectable } from '@angular/core';

import{MatSnackBar} from '@angular/material/snack-bar';
import { duration } from 'moment';
import { Session } from '../Interfaces/session';

@Injectable({
  providedIn: 'root'
})
export class UtilityService {

  constructor(private _SnackBar:MatSnackBar) { }

  ShowAlert (message:string, tipo:string){

    this._SnackBar.open(message,tipo,{
      horizontalPosition:"end",
      verticalPosition:"top",
      duration:3000
    })
  }
  saveUserSession (sessionUser:Session)
  {
    localStorage.setItem("User",JSON.stringify(sessionUser));
  }
  getUsersession(){

    const datastring=localStorage.getItem("User");
    const User=JSON.parse(datastring!);
    return User;
  }
  DeleteUserSession(){
    localStorage.removeItem("User")
  }

}
