import { Currency } from './currency';
import { Employee } from './employee'
export class WorkConditions {
  constructor(public id?: number, public employee?: Employee,
    public employeeId?: number, public date?: Date, public payRate?: number,
    public wageCurrency: Currency = new Currency(), public wageCurrencyId?: number,
    public expensesCurrency: Currency = new Currency(), public expensesCurrencyId?: number,
    public payCurrency: Currency = new Currency(), public payCurrencyId?: number,
    public deductions?: number) {

  }
}
