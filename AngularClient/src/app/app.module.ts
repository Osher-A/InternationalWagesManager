import { ErrorHandler, NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClient, HttpClientModule } from '@angular/common/http';

import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatDatepicker, MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInput, MatInputModule } from '@angular/material/input';
import { MatNativeDateModule } from '@angular/material/core';



import { AppComponent } from './app.component';
import { EmployeesAddComponent } from './Employees/add/add.component';
import { EmployeesIndexComponent } from './Employees/index/index.component';
import { GlobalErrorHandler } from './common/global-error-handler';
import { EmployeesDetailsComponent } from './Employees/details/details.component';
import { AppRoutingModule } from './Employees/app-routing-module';
import { WorkConditionsIndexComponent } from './workConditions/index/index.component';
import { WorkConditionsDetailsComponent } from './workConditions/details/details.component'
import { NavbarComponent } from './common/navbar/navbar.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { WorkConditionsAddComponent } from './workConditions/add/add.component';
import { WorkCDataService } from './services/work-cdata-service.';
import { AlertMsgComponent } from './alert-msg/alert-msg.component';

@NgModule({
  declarations: [
    AppComponent,
    EmployeesAddComponent,
    EmployeesIndexComponent,
    EmployeesDetailsComponent,
    WorkConditionsIndexComponent,
    WorkConditionsDetailsComponent,
    WorkConditionsAddComponent,
    NavbarComponent,
    AlertMsgComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    AppRoutingModule,
    MatButtonModule,
    MatToolbarModule,
    BrowserAnimationsModule,
    MatDatepickerModule,
    MatFormFieldModule,
    MatNativeDateModule,
    MatInputModule,
  ],
  providers: [
    {
      provide: ErrorHandler, useClass: GlobalErrorHandler
    },
    {
      provide: WorkCDataService, deps: [HttpClient], useFactory: (dep1: HttpClient) => {
        return new WorkCDataService(dep1, "")
      },

    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
