import { HttpClient } from '@angular/common/http';
import { SalaryDataService } from 'src/app/services/data/salary/salary.service';
import { SalaryComponents } from 'src/app/dto/salaryComponents';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.css']
})
export class SalaryUpdateComponent implements OnInit {
  private _salaryDataService: SalaryDataService
  salaryComp: SalaryComponents = new SalaryComponents();
  private _id: number;
  constructor(http: HttpClient, private _route: ActivatedRoute, private _router: Router) {
      this._salaryDataService = new SalaryDataService(http);
      this._id = this._route.snapshot.params['id'];
   }

  ngOnInit(): void {
    this._salaryDataService.get(this._id).subscribe(response => {
        this.salaryComp = response;
      })
  }

  onChange(date: Date){
    this.salaryComp.date = date;
  }

  onSubmit() : void{
    this._salaryDataService.update(this._id, this.salaryComp).subscribe(response => {
      this._router.navigate([`details/${this.salaryComp.employeeId}`])
    })
  }

}
