import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { RolService } from 'src/app/Services/rol.service';
import { Rol } from 'src/app/Interfaces/rol';
import { MenuService } from 'src/app/Services/menu.service';
import { menu } from 'src/app/Interfaces/menu';
import { ResponseApi } from 'src/app/Interfaces/response-api';

@Component({
  selector: 'app-modalmenu',
  templateUrl: './modalmenu.component.html',
  styleUrls: ['./modalmenu.component.css']
})
export class ModalmenuComponent implements OnInit {

  formMenu!: FormGroup;
  roles: Rol[] = [];

  constructor(
    public dialogRef: MatDialogRef<ModalmenuComponent>,
    private fb: FormBuilder,
    private _menuService: MenuService,
    private _rolservice:RolService,
    @Inject(MAT_DIALOG_DATA) public data: menu
  ) { }

  ngOnInit(): void {
    this._rolservice.List().subscribe(
      (response: ResponseApi) => {
        if (response.status) {
          this.roles = response.value;
        }
      }
    );

    this.formMenu = this.fb.group({
      selectedRoles: [[]]
    });

    const selectedRoles = this.data.selectedRoles;
    this.formMenu.get('selectedRoles')?.setValue(selectedRoles);
  }

  onClose(): void {
    this.dialogRef.close();
  }

  updateRoles(): void {
    const selectedRoles = this.formMenu.get('selectedRoles')?.value;
    const id = this.data.id;

    const menuToUpdate: menu = {
      id,
      name:'',
      url:'',
      selectedRoles
    };

    this._menuService.Update(menuToUpdate).subscribe(
      (response: ResponseApi) => {
        if (response.status) {
          this.dialogRef.close('updated');
        }
      }
    );
  }

}
