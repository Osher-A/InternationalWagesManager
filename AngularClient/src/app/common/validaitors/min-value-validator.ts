import { AbstractControl, ValidationErrors } from "@angular/forms";
export class MinValueValidator {
    static cannotBeZero(control: AbstractControl) : ValidationErrors | null {
        if(control.value as number < 1)
           return {cannotBeZero: true}
        else 
           return null;   

           //return {cannotBeZero: {
           //minValue:1,
           //actualValue: control.value as number
           //}}
    }
}