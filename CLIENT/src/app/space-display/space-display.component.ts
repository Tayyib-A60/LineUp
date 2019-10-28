import { Component, OnInit } from '@angular/core';
import { NgbCarouselConfig } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-space-display',
  templateUrl: './space-display.component.html',
  styleUrls: ['./space-display.component.scss']
})
export class SpaceDisplayComponent implements OnInit {

  constructor(private carouselConfig: NgbCarouselConfig) {
    carouselConfig.showNavigationArrows = true;
    carouselConfig.interval = 0;
  }

  ngOnInit() {
  }

}
