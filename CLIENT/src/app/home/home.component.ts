import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  hideSidebar: boolean;

  constructor() { }

  ngOnInit() {
  }

  toggleHideSidebar($event: boolean): void {
    setTimeout(() => {
      this.hideSidebar = $event;
    }, 0);
  }
  
}
