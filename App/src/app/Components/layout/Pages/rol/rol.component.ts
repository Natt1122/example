import { Component, OnInit, AfterViewInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import { ModalrolComponent } from '../../Modal/modalrol/modalrol.component';
import { Rol } from 'src/app/Interfaces/rol';
import { RolService } from 'src/app/Services/rol.service';
import Swal from 'sweetalert2';
import { UtilityService } from 'src/app/reusable/utility.service';


@Component({
  selector: 'app-rol',
  templateUrl: './rol.component.html',
  styleUrls: ['./rol.component.css']
})
export class RolComponent implements OnInit,AfterViewInit {

  columnTable: string[] = ['name', 'actions'];
  dataIni: Rol[] = [];
  dataListRol = new MatTableDataSource(this.dataIni);

  @ViewChild(MatPaginator) paginationTable!: MatPaginator;

  constructor(
    private dialog: MatDialog,
    private _rolService: RolService,
    private _utilityService: UtilityService
  ) { }

  getRol() {
    this._rolService.List().subscribe({
      next:(data)=>{
       if (data.status)
       this.dataListRol=data.value;
       else
       this._utilityService.ShowAlert("No data found","Oops!")
      },
      error:(e)=>{}
    })
  }

  ngOnInit(): void {
    this.getRol();
  }

  ngAfterViewInit(): void {
    this.dataListRol.paginator = this.paginationTable;
  }

  applyTableFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataListRol.filter = filterValue.trim().toLocaleLowerCase();
  }

  newRol() {
    this.dialog.open(ModalrolComponent, {
      disableClose: true
    }).afterClosed().subscribe(answer => {
      if (answer === "true") this.getRol()
    });
  }

  editRol(rol: Rol) {
    this.dialog.open(ModalrolComponent, {
      disableClose: true,
      data: rol
    }).afterClosed().subscribe(answer => {
      if (answer === "true") this.getRol()
    });
  }

  deleteRol(id:number) {
    const rol: Rol = {
      id: id,
      name: ""
    };
  
    Swal.fire({
      title: 'Do you want to delete the role?',
      text: rol.name,
      icon: "warning",
      confirmButtonColor: '#3085d6',
      confirmButtonText: "Yes, delete",
      showCancelButton: true,
      cancelButtonColor: '#d33',
      cancelButtonText: 'Go back'
    }).then((answer) => {
      if(answer.isConfirmed){
        this._rolService.Delete(rol).subscribe({
          next:(data)=>{
            if(data.status)
            {
              this._utilityService.ShowAlert("Role was deleted","Ready")
              this.getRol();
            }else
            this._utilityService.ShowAlert("It couldn't delete", "Error")
          },
          error:(e)=>{}
        })
      }
    })
  }

}
