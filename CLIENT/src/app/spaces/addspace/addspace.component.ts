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
  amenityItems = [ ]
  constructor() { }

  ngOnInit() {
  }

  toggleCheckbox(item) {
    console.log(item);
    
    if(item.currentTarget.checked) {
      this.amenityItems.push(item.currentTarget.id);
    } else {
      this.amenityItems.splice((this.amenityItems.indexOf(item.currentTarget.id),1))
    }
      console.log(this.amenityItems);
  }

}
