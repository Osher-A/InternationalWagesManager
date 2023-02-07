import { ErrorHandler } from '@angular/core'

export class GlobalErrorHandler implements ErrorHandler {
  handleError(error: Error): void {
    alert('An unexpected error occurred!' + error.message + error.stack);
  }

}
