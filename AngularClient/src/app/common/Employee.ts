export class Employee {
  constructor(public id: Number, public firstName: string, public lastName: string,
    public dob: Date, public phone: any, public email: string) { }

  get fullName(): string {
    return this.firstName + this.lastName;
  }
}

