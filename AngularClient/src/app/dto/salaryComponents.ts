import { Employee } from './employee';
export class SalaryComponents{
    constructor(public id?:number, public employeeId?:number,
        public date?:Date, public totalHours?:number, public bonusHours?:number, public bonusWage?:number,
        public expenses?:number ){}
}