import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { WorkConditionsAddComponent } from "./workConditions/add/add.component";
import { WorkConditionsDetailsComponent } from "./workConditions/details/details.component";
import { WorkConditionsIndexComponent } from "./workConditions/index/index.component";
import { EmployeesAddComponent } from "./Employees/add/add.component";
import { EmployeesDetailsComponent } from "./Employees/details/details.component";
import { EmployeesIndexComponent } from "./Employees/index/index.component";
import { WorkConditionsUpdateComponent } from "./workConditions/update/update.component";
import { SalaryIndexComponent } from "./salary/index/index.component";
import { SalaryDetailsComponent } from "./salary/details/details.component";
import { SalaryAddComponent } from "./salary/add/add.component";
import { SalaryUpdateComponent } from "./salary/update/update.component";


const routes: Routes = [
  { path: '', component: EmployeesIndexComponent },
  { path: 'employees', component: EmployeesIndexComponent },
  { path: 'employee/details/:id', component: EmployeesDetailsComponent },
  { path: 'employee/details', component: EmployeesDetailsComponent },
  { path: 'employee/add', component: EmployeesAddComponent },
  { path: 'workconditions', component: WorkConditionsIndexComponent },
  { path: 'workconditions/details/:id', component: WorkConditionsDetailsComponent },
  { path: 'workconditions/add/:id', component: WorkConditionsAddComponent },
  { path: 'workconditions/update/:id', component: WorkConditionsUpdateComponent },
  { path: 'salary', component: SalaryIndexComponent},
  { path: 'salary/details/:id', component: SalaryDetailsComponent},
  { path: 'salary/add/:id', component: SalaryAddComponent},
  { path: 'salary/update/:id', component: SalaryUpdateComponent}
]
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
