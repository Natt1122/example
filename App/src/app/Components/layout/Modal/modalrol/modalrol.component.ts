import { Component, OnInit,Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef,MAT_DIALOG_DATA } from '@angular/material/dialog';
import { RolService } from 'src/app/Services/rol.service';
import { UtilityService } from 'src/app/reusable/utility.service';
import { Rol } from 'src/app/Interfaces/rol';


@Component({
  selector: 'app-modalrol',
  templateUrl: './modalrol.component.html',
  styleUrls: ['./modalrol.component.css']
})
export class ModalrolComponent implements OnInit {

  formRol: FormGroup;
  titleAction: string = "Add";
  btnAction: string = "Save";
  roles: Rol[]=[];

  constructor(
    private modalActual: MatDialogRef<ModalrolComponent>,
    @Inject(MAT_DIALOG_DATA) public dataRol: Rol,
    private fb: FormBuilder,
    private _rolService: RolService,
    private _utilityService: UtilityService
  ) {
    this.formRol = this.fb.group({
      name: ["",Validators.required]
    });

    if (this.dataRol != null) {
      this.titleAction = "Edit";
      this.btnAction = "Update"
    }
  }

  ngOnInit(): void {
    if(this.dataRol!=null){
      this.formRol.patchValue({
        Name: this.dataRol.name,
      })
  } }

  saveRol() {
    const newRol: Rol = {
      id: this.dataRol == null ? 0 : this.dataRol.id,
      name: this.formRol.value.name
    };

    this._rolService.save(newRol).subscribe({
      next: (data) => {
        if (data.status) {
          this._utilityService.ShowAlert("Rol was registered", "success");
          this.modalActual.close("true");
        } else {
          this._utilityService.ShowAlert("Filed to register Rol", "error");
        }
      },
      error: (e) => {}
    })
  }
}
