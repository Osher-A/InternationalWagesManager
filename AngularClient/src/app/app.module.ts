import { ErrorHandler, NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';


import { AppComponent } from './app.component';
import { AddComponent } from './Employees/add/add.component';
import { ListComponent } from './Employees/list/list.component';
import { GlobalErrorHandler } from './common/global-error-handler';
import { DetailsComponent } from './Employees/details/details.component';
import { AppRoutingModule } from './Employees/app-routing-module';
import { EmployeeService } from './services/employee.service';
import { IndexComponent } from './workConditions/index/index.component';
import { NavbarComponent } from './common/navbar/navbar.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
@NgModule({
  declarations: [
    AppComponent,
    AddComponent,
    ListComponent,
    DetailsComponent,
    IndexComponent,
    NavbarComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    AppRoutingModule,
    MatButtonModule,
    MatToolbarModule,
    BrowserAnimationsModule,
  ],
  providers: [
    EmployeeService,
    {
      provide: ErrorHandler, useClass: GlobalErrorHandler
    }],
  bootstrap: [AppComponent]
})
export class AppModule { }
