import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

export class DataService<T> {

  constructor(private http: HttpClient, protected url: string) { }


  getAll(): Observable<T[]> {
    return this.http.get<T[]>(this.url)
      .pipe(
        catchError(this.handleError2<T[]>('getAll', [])
        ))
  }

  get(id: number): Observable<T> {
    return this.http.get<T>(this.url + '/' + id)
      .pipe(
        catchError(this.handleError2<T>('get id=${id}'))
      )
  }

  post(body: T): Observable<T> {
    return this.http.post<T>(this.url, body)
      .pipe(
        catchError(this.handleError2<T>('post'))
      )
  }

  delete(id: number): Observable<T> {
    return this.http.delete<T>(this.url + '/' + id.toString())
      .pipe(
        catchError(this.handleError2<T>('post'))
      )
  }

  update(id: number, body: T): Observable<T> {
    return this.http.put<T>(this.url + '/' + id.toString(), body)
      .pipe(
        catchError(this.handleError2<T>('update'))
      )
  }

  private handleError(error: Response) {
    if (error.status == 400)
      //this.form.setErrors(error.json());
      alert('This is an invalid request');
    // unexpected errors are handled at GlobalErrorHandler.ts
  }

  private handleError2<T>(operation = 'operation', result?: T) {
    return (error: Response): Observable<T> => {
      if (error.status == 400)
        alert('This is an invalid request');
      //this.form.setErrors(error.json());
      // unexpected errors are handled at GlobalErrorHandler.ts

      // TODO: send error to remote logging
      //better job of transforming error for user consumption
      //this.log(`${operation} failed: ${error.message}`);

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
}


