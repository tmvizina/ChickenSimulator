import { Injectable, Inject } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import {Farm} from "./farm"

@Injectable({
    providedIn: 'root'
})

export class FarmService {
  constructor(private http: HttpClient) {

  }

  getFarms(@Inject('BASE_URL') baseUrl: string): any {
    return this.http.get<Farm[]>(baseUrl + 'api/Chickens/Farm');
  }
}
