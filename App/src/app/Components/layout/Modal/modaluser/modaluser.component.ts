import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder,FormGroup, Validators } from '@angular/forms';
import {MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { Rol } from 'src/app/Interfaces/rol';
import { User } from 'src/app/Interfaces/user';
import { RolService } from 'src/app/Services/rol.service';
import { UserService } from 'src/app/Services/user.service';
import { UtilityService } from 'src/app/reusable/utility.service';


@Component({
  selector: 'app-modaluser',
  templateUrl: './modaluser.component.html',
  styleUrls: ['./modaluser.component.css']
})
export class ModaluserComponent implements OnInit {

  formUser:FormGroup;
  hidePassword:boolean= true;
  titleAction:string="Add";
  btnAction:string="Save";
  listRol:Rol[]=[];


  constructor(
    private modalActual:MatDialogRef<ModaluserComponent>,
    @Inject(MAT_DIALOG_DATA) public dataUser:User,
    private fb:FormBuilder,
    private _userservice:UserService,
    private _utilityService:UtilityService,
    private _rolService:RolService

  ) { 
    this.formUser=this.fb.group({
      FullName:['',Validators.required],
      email:['',Validators.required],
      idRol:['',Validators.required],
      password:['',Validators.required],
      isActive: ['1',Validators.required],
      
    });
    if(this.dataUser !=null){
      this.titleAction="Edit";
      this.btnAction="Update"
    }
    this._rolService.List().subscribe({
      next:(data)=> {
        if (data.status) this.listRol=data.value
        console.log(data.value);
      },
      error:(e)=>{}
    }
    
    )
  }

  ngOnInit(): void {
    if(this.dataUser!=null){
      this.formUser.patchValue({
        FullName: this.dataUser.FullName,
        email: this.dataUser.email,
        idRol:this.dataUser.IdRol,
        passsword:this.dataUser.password,
        isActive: this.dataUser.isActive.toString,
      })
  } }

  EditSave_User(){
    const _user:User={
      id: this.dataUser==null? 0 : this.dataUser.id,
      FullName:this.formUser.value.FullName,
      email:this.formUser.value.email,
      IdRol:this.formUser.value.idRol,
      rolDescription: '',
      password: this.formUser.value.passsword,
      isActive: parseInt(this.formUser.value.isActive),
    }

    if(this.dataUser==null){
      this._userservice.Save(_user).subscribe({
        next: (data)=>{
          if(data.status){
            this._utilityService.ShowAlert("user was registered","success")
            this.modalActual.close("true")
          }

        },
        error: (e)=>{}
      })

    }else{
      this._userservice.Update(_user).subscribe({
        next: (data)=>{
          if(data.status){
            this._utilityService.ShowAlert("User was Updated","exito")
            this.modalActual.close("true")
          }else
          this._utilityService.ShowAlert("Failed to Update User", "error")

        },
        error: (e)=>{}
      })
    }
  

  }


}
