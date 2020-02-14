import { Component, OnInit } from '@angular/core';
import { NguCarouselConfig } from '@ngu/carousel';
import { SpaceService } from '../spaces/space.service';

@Component({
  selector: 'app-space-home',
  templateUrl: './space-home.component.html',
  styleUrls: ['./space-home.component.scss']
})
export class SpaceHomeComponent implements OnInit {

  spaceTypes: any[];
  constructor(private spaceService: SpaceService) { }

  ngOnInit() {
    // this.spaceService.getSpaceTypes().subscribe((spaceTypes: any[]) => {
    //   this.spaceTypes = spaceTypes;
    //   console.log(spaceTypes);
    // })
  }

}
