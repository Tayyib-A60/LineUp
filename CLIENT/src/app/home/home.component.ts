import { Component, OnInit } from '@angular/core';
import { NgbCarouselConfig } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  hideSidebar: boolean;
  cities = [
    {id: 1, name: 'Vilnius'},
    {id: 2, name: 'Kaunas'},
    {id: 3, name: 'Pavilnys', disabled: true},
    {id: 4, name: 'Pabradė'},
    {id: 5, name: 'Klaipėda'}
];
  selectedCity: any;
  constructor(private carouselConfig: NgbCarouselConfig) {
    carouselConfig.showNavigationArrows = false;
    carouselConfig.interval = 4000;
  }

  ngOnInit() {
  }

  toggleHideSidebar($event: boolean): void {
    setTimeout(() => {
      this.hideSidebar = $event;
    }, 0);
  }
  
}
