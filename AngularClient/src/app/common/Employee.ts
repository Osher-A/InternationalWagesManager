export class Employee {
  constructor(public id: Number = 0, public firstName: string = "", public lastName: string = "",
    public dob: Date = new Date(), public phone: any = "", public email: string = "") { }

  get fullName(): string {
    return this.firstName + this.lastName;
  }
}

