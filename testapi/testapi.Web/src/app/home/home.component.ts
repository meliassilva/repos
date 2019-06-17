import { Component, OnInit } from '@angular/core';

import { ExampleService, LoadingComponent } from '../shared';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  values: Array<string>;
  loading: boolean;

  constructor (
    private exampleService: ExampleService
  ) {}


  ngOnInit() {
    this.loading = true;
    this.exampleService.getValues().subscribe(data => {
      this.values = data;
      this.loading = false;
    });
  }

}
