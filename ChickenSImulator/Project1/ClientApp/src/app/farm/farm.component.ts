import { Component, Inject, Input } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { FarmService } from '../farm.service';
import { Farm } from '../farm';

@Component({
    selector: 'app-farm',
    templateUrl: './farm.component.html',
  styleUrls: ['./farm.component.css'],
  providers: [FarmService]
})
/** Farm component*/
export class FarmComponent {
  fJSON: string = "Farms";
  farms: Farm[] = [];
  base: string = "";

  /** Farm ctor */
  constructor(private http: HttpClient, private farm: FarmService, @Inject('BASE_URL') baseUrl) {
    this.base = baseUrl + 'api/Chickens/Farm'
    this.getFarms();
  }

  getFarms() {
    this.http.get<Farm[]>(this.base).subscribe(fList => {
      this.farms = fList;
      console.log(fList);
      console.log(this.farms);
    })
  }
}


