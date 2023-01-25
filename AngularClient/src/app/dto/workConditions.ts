import { Currency } from './currency';
import { Employee } from './employee'
export class WorkConditions {
  constructor(public id: number, public employee: Employee = new Employee(), public employeeId: number,
    public date: Date = new Date(), public payRate: number = 0, public wageCurrency: Currency,
    public wageCurrencyId: number, public expensesCurrency: Currency, public expensesCurrencyId: number,
    public payCurrency: Currency, public payCurrencyId: number, public deductions: number) {

  }
}
