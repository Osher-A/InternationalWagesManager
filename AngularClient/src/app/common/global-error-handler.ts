import { ErrorHandler } from '@angular/core'

export class GlobalErrorHandler implements ErrorHandler {
    handleError(error: Response): void {
      alert('An unexpected error occurred!' + error);
    }

}
