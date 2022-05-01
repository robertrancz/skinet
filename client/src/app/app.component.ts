import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { IPagination } from './shared/models/pagination';
import { IProduct } from './shared/models/product';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  title = 'Skinet';
  products: IProduct[];

  /**
   *
   */
  constructor(private http: HttpClient) {

  }

  ngOnInit(): void {
    this.http.get('https://localhost:5001/api/products?pageSize=25').subscribe(
      (resp: IPagination) => {
        this.products = resp.data;
        console.log(resp);
      },
      (error) => {
        console.log(error);
      }
    );

  }
}
