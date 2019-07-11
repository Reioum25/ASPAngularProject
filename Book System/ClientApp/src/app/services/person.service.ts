import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { MyResponse } from '../models/MyResponse.model';
import { Person } from '../models/person.model';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PersonService {
  personApi:string;

  constructor(
    private http: HttpClient
  ) {
    this.personApi = 'api/MySample/'
   }

   getMyName(): Observable<MyResponse>{
    return this.http.get<MyResponse>(this.personApi + 'MyName');
  }

  getYourName(name:string): Observable<MyResponse>{
    return this.http.get<MyResponse>(this.personApi + 'YourName/' + name)
  }

  checkLegalAge(person:Person): Observable<MyResponse>{
    return this.http.post<MyResponse>(this.personApi + 'LegalAge', person);
  }
}
