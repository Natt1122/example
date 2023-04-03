import { Component, OnInit,AfterViewInit, ViewChild } from '@angular/core';
import {MatTableDataSource} from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';

import { ModaluserComponent } from '../../Modal/modaluser/modaluser.component';
import { User } from 'src/app/Interfaces/user';
import { UserService } from 'src/app/Services/user.service';
import Swal from 'sweetalert2';
import { UtilityService } from 'src/app/reusable/utility.service';



@Component({
  selector: 'app-user-manage',
  templateUrl: './user-manage.component.html',
  styleUrls: ['./user-manage.component.css']
})
export class UserManageComponent implements OnInit,AfterViewInit {
  
  columntable: string[] = ['FullName','email','rolDescription', 'status', 'actions']
  dataIni:User[]=[];
  dataListUser= new MatTableDataSource(this.dataIni);
  @ViewChild(MatPaginator) paginationtable!:MatPaginator;
  constructor(
    private dialog:MatDialog,
    private _userService:UserService,
    private _utilityService:UtilityService
  ) { }

  getUsers(){
    this._userService.ListUser().subscribe({
      next:(data)=>{
       if (data.status)
       this.dataListUser=data.value;
       else
       this._utilityService.ShowAlert("No data found","Oops!")
      },
      error:(e)=>{}
    })
  }

  ngOnInit(): void {
    this.getUsers();
  }
  ngAfterViewInit(): void {
      this.dataListUser.paginator=this.paginationtable
  }

  applyTableFilter(event: Event){
    const filterValue=(event.target as HTMLInputElement).value;
    this.dataListUser.filter=filterValue.trim().toLocaleLowerCase();
  }
  newUser(){
    this.dialog.open(ModaluserComponent,{
      disableClose:true
    }).afterClosed().subscribe(answer=> {
      if(answer === "true")this.getUsers()
    });
  }
  EditUser(user:User){
    this.dialog.open(ModaluserComponent,{
      disableClose:true,
      data:user
    }).afterClosed().subscribe(answer=> {
      if(answer === "true")this.getUsers()
    });
  }

  DeleteUser(user:User){
    Swal.fire({
      title: 'Do you want to delet the user?',
      text: user.FullName,
      icon:"warning",
      confirmButtonColor:'#3085d6',
      confirmButtonText:"Yes, delete",
      showCancelButton:true,
      cancelButtonColor: '#d33',
      cancelButtonText: 'Cancel'
    }).then((answer)=>{
      if(answer.isConfirmed){
        this._userService.Delete(user.id).subscribe({
          next:(data)=>{
            if(data.status)
            {
              this._utilityService.ShowAlert("User was delet","Ready")
              this.getUsers();
            }else
            this._utilityService.ShowAlert("It couldn't delete", "Error")
          },
          error:(e)=>{}
        })
      }
    })
  }
  

}
