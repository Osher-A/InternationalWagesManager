import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Currency } from '../../../dto/currency';
import { DataService } from '../data.service';

@Injectable({
  providedIn: 'root'
})
export class CurrencyDataService extends DataService<Currency> {

  constructor(http: HttpClient) {
    super(http, '/currencies');
  }
}
