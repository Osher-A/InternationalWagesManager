import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { SalaryComponents } from 'src/app/dto/salaryComponents';
import { DataService } from '../data.service';
const URL = "https://localhost:7194/api/salarycomponents";

@Injectable({
  providedIn: 'root'
})
export class SalaryDataService extends DataService<SalaryComponents>{
  constructor( http:HttpClient) { 
   super(http, URL)
   }

   set endOfUrl(value: string) {
    this.url = URL + value;
  }
}
