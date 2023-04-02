import { EmployeeDataService } from './../../services/data/employee/employee-data-service';
import { SalaryComponents } from './../../dto/salaryComponents';
import { Component, OnInit } from '@angular/core';
import { SalaryDataService } from 'src/app/services/data/salary/salary.service';
import { ActivatedRoute , Router} from '@angular/router';
import { MessageService } from 'src/app/services/messages/message.service';
import { BehaviorSubject } from 'rxjs';
import { Salary } from 'src/app/dto/salary';
@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['../../app.component.css']
})
export class SalaryDetailsComponent implements OnInit {
  private _employeeId: number;
  allSalaryComp = new BehaviorSubject<SalaryComponents[]>([]);
  employeeName:string = ""
  
  constructor(private salaryData: SalaryDataService,private _employeeData: EmployeeDataService, 
    private _route: ActivatedRoute, private _router: Router, private _messageService: MessageService) {
      this._employeeId = this._route.snapshot.params['id'];
     }
  
  ngOnInit(): void {
    this.salaryData.endOfUrl = `/all/${this._employeeId}`;
    
    this.salaryData.getAll().subscribe(response => {
        this.allSalaryComp.next(response);
    })
    
    this.salaryData.endOfUrl = "";
    this._employeeData.get(this._employeeId).subscribe(response => {
        this.employeeName = response.firstName;
    })
  }

  onEdit(salaryComp: SalaryComponents) : void{
       this._router.navigate([`salary/update/${salaryComp.id}`])
  }

  async onDelete(salaryComp: SalaryComponents)  {
   if(await this._messageService.usersConfirmation()){
    this.salaryData.delete(salaryComp.id as number).subscribe(response => {
      this.allSalaryComp.next(this.allSalaryComp.value.filter(s => s != salaryComp));
      this._router.navigate([`salary/details/${salaryComp.id}`])
      console.log(response);
    })
   }
  }
  

}
