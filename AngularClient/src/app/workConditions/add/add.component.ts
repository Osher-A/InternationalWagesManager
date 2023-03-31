import { Component, inject, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Currency } from '../../dto/currency';
import { WorkConditions } from '../../dto/workConditions';
import { CurrencyDataService } from '../../services/data/currency/currency-data.service';
import { WorkCDataService } from '../../services/data/workConditions/work-cdata-service.';
import { WorkConditionsDetailsComponent } from '../details/details.component';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['../../app.component.css']
})
export class WorkConditionsAddComponent implements OnInit {
  currencies: Currency[] = [];

  public workConditions: WorkConditions;
  constructor(private _currencyDataService: CurrencyDataService, private _router: Router,
    activatedRoute: ActivatedRoute, private _workCDataService: WorkCDataService) {
    this.workConditions = new WorkConditions();
    this.workConditions.employeeId = activatedRoute.snapshot.params['id'];

  }

  ngOnInit(): void {
    this._currencyDataService.getAll().subscribe(response => {
      this.currencies = response;

    })
  }

  onSubmit(form: NgForm) {
    this._workCDataService.post(this.workConditions).subscribe(response => {
      this._router.navigate([`workconditions/details/${this.workConditions.employeeId}`])

    })

  }

}
