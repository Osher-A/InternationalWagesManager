import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AddComponent } from "./add/add.component";
import { DetailsComponent } from "./details/details.component";
import { ListComponent } from "./list/list.component";


const routes: Routes = [
  { path: '', component: ListComponent },
  { path: 'employees', component: ListComponent },
  { path: 'employee/details/:id', component: DetailsComponent },
  { path: 'employee/details', component: DetailsComponent },
  { path: 'employee/add', component: AddComponent }
]
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
