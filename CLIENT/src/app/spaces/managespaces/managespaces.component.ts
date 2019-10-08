import { Component, OnInit, ViewChild } from '@angular/core';
import { DatatableComponent } from '@swimlane/ngx-datatable/release'

@Component({
  selector: 'app-managespaces',
  templateUrl: './managespaces.component.html',
  styleUrls: ['./managespaces.component.scss']
})
export class ManagespacesComponent implements OnInit {

  data = [
    {
      id: 1,
      image: 'assets/img/elements/01.png',
      name: "Ethel Price",
      location: "V.I",
      price: "200,000",
      size: 200
    },
    {
      id: 1,
      image: 'assets/img/elements/02.png',
      name: "Ethel Price",
      location: "V.I",
      price: "200,000",
      size: 200
    },
    {
      id: 1,
      image: 'assets/img/elements/03.png',
      name: "Ethel Price",
      location: "V.I",
      price: "200,000",
      size: 200
    },
    {
      id: 1,
      image: 'assets/img/elements/04.png',
      name: "Ethel Price",
      location: "V.I",
      price: "200,000",
      size: 200
    },
  ];
    @ViewChild(DatatableComponent, {static: false}) table: DatatableComponent;

    constructor() {
    }
    ngOnInit() {

    }

}
