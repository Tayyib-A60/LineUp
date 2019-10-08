import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-managebookings',
  templateUrl: './managebookings.component.html',
  styleUrls: ['./managebookings.component.scss']
})
export class ManagebookingsComponent implements OnInit {

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
  
  constructor() { }

  ngOnInit() {
  }

}
