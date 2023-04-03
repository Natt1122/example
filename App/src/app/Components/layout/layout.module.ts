import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LayoutRoutingModule } from './layout-routing.module';
import { UserManageComponent } from './Pages/user-manage/user-manage-component';
import { MenusComponent } from './Pages/menus/menus.component';
import { RolComponent } from './Pages/rol/rol.component';
import { SharedModule } from 'src/app/reusable/shared/shared.module';
import { ModaluserComponent } from './Modal/modaluser/modaluser.component';
import { ModalrolComponent } from './Modal/modalrol/modalrol.component';
import { ModalmenuComponent } from './Modal/modalmenu/modalmenu.component';




@NgModule({
  declarations: [
    UserManageComponent,
    MenusComponent,
    ModaluserComponent,
    ModalrolComponent,
    RolComponent,
    ModalmenuComponent,
    
    
    
  ],
  imports: [
    CommonModule,
    LayoutRoutingModule,
    SharedModule
  ]
})
export class LayoutModule { }
