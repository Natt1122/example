import { Component, OnInit, AfterViewInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { ModalmenuComponent } from '../../Modal/modalmenu/modalmenu.component';
import { menu } from 'src/app/Interfaces/menu';
import { MenuService } from 'src/app/Services/menu.service';
import { UtilityService } from 'src/app/reusable/utility.service';



@Component({
  selector: 'app-menus',
  templateUrl: './menus.component.html',
  styleUrls: ['./menus.component.css']
})

export class MenusComponent implements OnInit, AfterViewInit {

  columnTable: string[] = ['name', 'actions'];

  dataList = new MatTableDataSource<menu>([]);
  @ViewChild(MatPaginator) paginationTable!: MatPaginator;

  constructor(
    private dialog: MatDialog,
    private _menuservice: MenuService,
    private _utilityService: UtilityService
  ) { }

  getMenu() {
    this._menuservice.get().subscribe({
      next:(data)=>{
       if (data.status)
       this.dataList.data = data.value;
       else
       this._utilityService.ShowAlert("No data found","Oops!")
      },
      error:(e)=>{}
    })
  }

  Updaterol(menu: menu) {
    const dialogRef = this.dialog.open(ModalmenuComponent, {
      disableClose: true,
      data: { mode: 'edit', menu }
    });
  
    dialogRef.afterClosed().subscribe(result => {
      if (result === 'updated') {
        this.getMenu();
      }
    });
  }


  
  ngOnInit(): void {
    this.getMenu();
  }
  ngAfterViewInit(): void {
    this.dataList.paginator = this.paginationTable;
  }
  applyTableFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataList.filter = filterValue.trim().toLocaleLowerCase();
  }

}
