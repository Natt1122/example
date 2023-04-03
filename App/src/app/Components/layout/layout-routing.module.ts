import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LayoutComponent } from './layout.component';
import { MenusComponent } from './Pages/menus/menus.component';
import { RolComponent } from './Pages/rol/rol.component';
import { UserManageComponent } from './Pages/user-manage/user-manage-component';


const routes: Routes = [{
path:'',
component:LayoutComponent,
children:[
  {path:'usermanage',component:UserManageComponent},
  {path:'Menus',component:MenusComponent},
  {path:'Rol',component:RolComponent}
]

}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LayoutRoutingModule { }
