import { Injectable } from '@angular/core';
import Swal, { SweetAlertResult, SweetAlertShowClass } from 'sweetalert2'

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  private _confirmed: boolean = false;
  constructor() { }
  simpleAlert() {
    Swal.fire('Hello Angular');
  }

  alertWithSuccess() {
    Swal.fire('Thank you...', 'You submitted succesfully!', 'success')
  }
  erroalert() {
    Swal.fire({
      icon: 'error',
      title: 'Oops...',
      text: 'Something went wrong!',
      footer: '<a href>Why do I have this issue?</a>'
    })
  }
  topend() {
    Swal.fire({
      position: 'top-end',
      icon: 'success',
      title: 'Your work has been saved',
      showConfirmButton: false,
      timer: 1500
    })
  }

    async usersConfirmation() {
    await this.confirmBox();
    if (this._confirmed) {
      return true;
    }
    else {
      return false;
    }
  }

  private async confirmBox(): Promise<void>{
    const result = await Swal.fire({
      title: 'Are you sure want to remove?',
      text: 'You will not be able to recover this file!',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'No, keep it'
    });
    if (result.isConfirmed) {
      this._confirmed = true;
    }
    else if (result.isDismissed) {
      this._confirmed = false;
    }
  }

  public async deletionConfirmation() {
    await Swal.fire(
      'Deleted!',
      'Your imaginary file has been deleted.',
      'success'
    )
  }

  public async cancelationConfirmation() {
    await Swal.fire(
      'Cancelled',
      'Your imaginary file is safe :)',
      'error'
    )
  }




}
