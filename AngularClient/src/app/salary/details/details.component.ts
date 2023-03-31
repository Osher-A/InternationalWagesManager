import { EmployeeDataService } from './../../services/data/employee/employee-data-service';
import { SalaryComponents } from './../../dto/salaryComponents';
import { Component, OnInit } from '@angular/core';
import { SalaryDataService } from 'src/app/services/data/salary/salary.service';
import { ActivatedRoute , Router} from '@angular/router';
import { MessageService } from 'src/app/services/messages/message.service';
@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['../../app.component.css']
})
export class SalaryDetailsComponent implements OnInit {
  private _id: number;
  allSalaryComp: SalaryComponents[] = []
  employeeName:string = ""
  
  constructor(private salaryData: SalaryDataService,private _employeeData: EmployeeDataService, 
    private _route: ActivatedRoute, private _router: Router, private _messageService: MessageService) {
      this._id = this._route.snapshot.params['id'];
     }
  
  ngOnInit(): void {
    this.salaryData.endOfUrl = `/all/${this._id}`;
    
    this.salaryData.getAll().subscribe(results => {
        this.allSalaryComp = results;
    })
    
    this.salaryData.endOfUrl = "";
    this._employeeData.get(this._id).subscribe(response => {
        this.employeeName = response.firstName;
    })
  }

  onEdit(salaryComp: SalaryComponents) : void{
       this._router.navigate([`salary/update/${salaryComp.id}`])
  }

  async onDelete(salaryComp: SalaryComponents)  {
   if(await this._messageService.usersConfirmation()){
    this._employeeData.delete(this._id).subscribe(response => {
      this._router.navigate([`salary/details/${salaryComp.id}`])
    })
   }
  }
  

}
