import { Component, OnInit } from '@angular/core';
import { Currency } from '../../dto/currency';
import { WorkConditions } from '../../dto/workConditions';
import { CurrencyDataService } from '../../services/currency-data.service';
import { WorkCDataService } from '../../services/work-cdata-service.';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'update',
  templateUrl: './update.component.html',
  styleUrls: ['../../app.component.css']
})
export class WorkConditionsUpdateComponent implements OnInit {
  private _updatedDate: string | null = null;
  currencies: Currency[] = [];
  public workConditions: WorkConditions;

  constructor(private _currencyDataService: CurrencyDataService, private _router: Router,
    activatedRoute: ActivatedRoute, private _workCDataService: WorkCDataService) {
    this.workConditions = new WorkConditions();
    this.workConditions.id = activatedRoute.snapshot.params['id'];

  }

  ngOnInit(): void {
    this._currencyDataService.getAll().subscribe(response => {
      this.currencies = response;
    })

    this._workCDataService.get(this.workConditions.id as number).subscribe(response => {
      this.workConditions = response;
    })
  }

  onSubmit(form: NgForm) {
    let dateParts = this._updatedDate?.split('/') as string[];
    this.workConditions.date = this._updatedDate == null ? this.workConditions.date :
      new Date(Number.parseInt(dateParts[0]), Number.parseInt(dateParts[1]), Number.parseInt(dateParts[2]));
    this.workConditions.expensesCurrencyId = this.workConditions.expensesCurrency.id;
    this.workConditions.wageCurrencyId = this.workConditions.wageCurrency.id;
    this.workConditions.payCurrencyId = this.workConditions.payCurrency.id;

    this._workCDataService.update(this.workConditions.employeeId as number, this.workConditions).subscribe(response => {
      this._router.navigate([`workconditions/details/${this.workConditions.employeeId}`])
      console.log(this.workConditions);
    })
  }

  onChange(input: HTMLInputElement) {
    this._updatedDate = input.value;
  }
}

