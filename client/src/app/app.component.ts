import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  title = 'Skinet';
  products: any[];

  /**
   *
   */
  constructor(private http: HttpClient) {

  }

  ngOnInit(): void {
    this.http.get('https://localhost:5001/api/products?pageSize=25').subscribe(
      (resp: any) => {
        this.products = resp.data;
        console.log(resp);
      },
      (error) => {
        console.log(error);
      }
    );

  }
}
