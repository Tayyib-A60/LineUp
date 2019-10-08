import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-addspace',
  templateUrl: './addspace.component.html',
  styleUrls: ['./addspace.component.scss']
})
export class AddspaceComponent implements OnInit {
  amenities = [
    {name: 'Sound sys'}, {name: 'AC'},
    {name: 'Coffee'}, {name: 'Rest room'},
    {name: 'Free wifi'}, {name: 'Kitchenette'}
  ];
  amenityItems = ['Items', 'Coffee']
  constructor() { }

  ngOnInit() {
  }

}
