import { ErrorHandler, NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { AddComponent } from './Employees/add/add.component';
import { ListComponent } from './Employees/list/list.component';
import { GlobalErrorHandler } from './common/global-error-handler';
import { DetailsComponent } from './Employees/details/details.component';
import { AppRoutingModule } from './Employees/app-routing-module';
import { EmployeeService } from './services/employee.service';
@NgModule({
  declarations: [
    AppComponent,
    AddComponent,
    ListComponent,
    DetailsComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    AppRoutingModule
  ],
  providers: [
    EmployeeService,
   { provide: ErrorHandler , useClass : GlobalErrorHandler
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
