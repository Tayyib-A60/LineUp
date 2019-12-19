import { Component, OnInit, ChangeDetectorRef, ChangeDetectionStrategy, Input } from '@angular/core';
import { slider } from './hello-slide.animation'
import { NguCarouselConfig } from '@ngu/carousel';
import { Observable, interval } from 'rxjs';
import { startWith, take, map } from 'rxjs/operators';

@Component({
selector: 'app-sample-carousel',
templateUrl: './sample-carousel.component.html',
styleUrls: ['./sample-carousel.component.scss'],
animations: [slider],
changeDetection: ChangeDetectionStrategy.OnPush
})
export class SampleCarouselComponent implements OnInit {

@Input() name: string;
  imgags = [
    'assets/bg.jpg',
    'assets/car.png',
    'assets/canberra.jpg',
    'assets/holi.jpg'
  ];
  carouselItems = [
    {type: 'Event Space', imgUrl: 'https://cdn.pixabay.com/photo/2013/03/02/02/41/city-89197__180.jpg', content: 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Magnam quo molestias reiciendis necessitatibus sint nobis quasi', url: 'event'},
    {type: 'Office Space', imgUrl: 'https://cdn.pixabay.com/photo/2019/11/16/18/43/image-4630827_960_720.jpg', content: 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Magnam quo molestias reiciendis necessitatibus sint nobis quasi', url: 'office'},
    {type: 'Shared Apartment', imgUrl: 'https://cdn.pixabay.com/photo/2019/11/16/18/43/image-4630827_960_720.jpg', content: 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Magnam quo molestias reiciendis necessitatibus sint nobis quasi', url: 'apartment'},
    {type: 'Gym Space', imgUrl: 'https://cdn.pixabay.com/photo/2013/03/02/02/41/city-89197__180.jpg', content: 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Magnam quo molestias reiciendis necessitatibus sint nobis quasi', url: 'gym'},
    {type: 'Meeting Space', imgUrl: 'https://cdn.pixabay.com/photo/2019/11/16/18/43/image-4630827_960_720.jpg', content: 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Magnam quo molestias reiciendis necessitatibus sint nobis quasi', url: 'shared'},
    {type: 'Chilling Space', imgUrl: 'https://cdn.pixabay.com/photo/2013/03/02/02/41/city-89197__180.jpg', content: 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Magnam quo molestias reiciendis necessitatibus sint nobis quasi', url: 'chilling'}
  ]
  public carouselTileItems$: Observable<number[]>;
  public carouselTileConfig: NguCarouselConfig = {
    grid: { xs: 1, sm: 1, md: 1, lg: 5, all: 0 },
    speed: 550,
    slide: 3,
    point: {
      visible: true
    },
    touch: true,
    loop: true,
    interval: { timing: 500000 },
    animation: 'lazy'
  };
  tempData: any[];
  responsiveOptions: { breakpoint: string; numVisible: number; numScroll: number; }[];
  
  constructor(private cdr: ChangeDetectorRef) {
    this.responsiveOptions = [
      {
          breakpoint: '1024px',
          numVisible: 4,
          numScroll: 4
      },
      {
          breakpoint: '768px',
          numVisible: 4,
          numScroll: 4
      },
      {
          breakpoint: '560px',
          numVisible: 1,
          numScroll: 1
      }
  ];
  }

  ngOnInit() {
    this.tempData = [];
    this.carouselTileItems$ = interval(500).pipe(
      startWith(0),
      take(10),
      map(val => {
        // console.log(val);
        
        const data = (this.tempData = [
          ...this.tempData,
          this.imgags[Math.floor(Math.random() * this.imgags.length)]
        ]);
        
        console.log(data);
        
        return data;
      })
    );
  }


}
