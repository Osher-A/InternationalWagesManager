import { HttpClient } from '@angular/common/http';
import { SalaryDataService } from 'src/app/services/data/salary/salary.service';
import { SalaryComponents } from './../../dto/salaryComponents';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['../../app.component.css']
})
export class SalaryAddComponent implements OnInit {
  salaryComp: SalaryComponents;
  private _salaryDataService: SalaryDataService | undefined;

  constructor(private _route: ActivatedRoute,
    private _router: Router, HttpClient: HttpClient) {
      this._salaryDataService = new SalaryDataService(HttpClient)
      this.salaryComp = new SalaryComponents() 
      this.salaryComp.employeeId = this._route.snapshot.params['id'];
     }

  ngOnInit(): void {
  }

  onSubmit(){
   console.log(this.salaryComp)
     this._salaryDataService?.post(this.salaryComp).subscribe(response => {
        if(response.totalHours as number > 1)
        this._router.navigate([`salary/details/${this.salaryComp.employeeId}`])
     })
  }

}
