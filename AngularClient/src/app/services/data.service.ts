import { HttpClient } from '@angular/common/http';
import { Injectable, Inject } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

export class DataService<T> {

  constructor(private http: HttpClient, private url: string) { }

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

  delete(id: Number): Observable<T> {
    return this.http.delete<T>(this.url + '/' + id.toString())
      .pipe(
        catchError(this.handleError2<T>('post'))
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


