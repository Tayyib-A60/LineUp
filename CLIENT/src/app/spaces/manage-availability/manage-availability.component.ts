import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { SpaceService } from '../space.service';
import { Space, BookingStatus } from '../models/space.model';
import { Store } from '@ngrx/store';
import * as bookingActions from '../../state/booking/booking.actions';
import * as bookingSelectors from '../../state/booking/booking.selector';
import { SpaceState } from '../state/space.reducers';

@Component({
  selector: 'app-manage-availability',
  templateUrl: './manage-availability.component.html',
  styleUrls: ['./manage-availability.component.scss']
})
export class ManageAvailabilityComponent implements OnInit {
  id: any;
  editMode: boolean;
  dateStart: any;
  dateEnd: any;
  timeStart = {
    hour: 6,
    minute: 0,
    second: 0
  };
  timeEnd = {
    hour: 12,
    minute: 0,
    second: 0
  };
  minuteStep = 0;
  meridian = true;
  space: Space;
  currentUser: any;
  numberOfGuests: number;

  constructor(private route: ActivatedRoute,
              private spaceService: SpaceService,
              private spaceStore: Store<SpaceState>) { }

  ngOnInit() {
    this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
    this.route.params
      .subscribe(
        (params: Params) => {
          this.id = params['id'];
          this.editMode = params['id'] != null;
        });
        this.spaceService.getSpace(Number(this.id)).subscribe((space: Space) => {
          this.space = space;
          console.log(space);
        });
  }

  bookSpace() {
    if(this.currentUser === null) {
      // this.notification.typeInfo('Please sign in to book this space', 'Info');
      return;
    }
    const { year, month, day } = this.dateStart;
    let yearTo = this.dateEnd['year'];
    let monthTo = this.dateEnd['month'];
    let dayTo = this.dateEnd['day'];
    let {hour, minute, second } = this.timeStart;
    hour = this.space.selectedPricingOption === 1 ? 0 : hour;
    minute = this.space.selectedPricingOption === 1 ? 1 : minute;
    let hourTo = this.space.selectedPricingOption === 1 ? 23 :this.timeEnd['hour'];
    let minuteTo = this.space.selectedPricingOption === 1 ? 59: this.timeEnd['minute'];
    let secondTo = this.timeEnd['second'];
    const dateFrom = new Date(year, month-1, day, hour+1, minute, second);
    const dateTo = new Date(yearTo, monthTo-1, dayTo, hourTo+1, minuteTo, secondTo);

    let amenitiesPrice = 0;
    console.log(this.dateStart, this.dateEnd);
    console.log(dateFrom, dateTo);
    
    // this.amenitiesSelected.forEach(amenity => {
    //   amenitiesPrice += amenity.price;
    // });
    let usingTimes = [];

    if(dayTo > day && month === monthTo) {
      for(let i = day; i <= dayTo; i++) {
        const from = new Date(year, month-1, i, hour+1, minute, second);
        const to = new Date(yearTo, monthTo-1, i, hourTo+1, minuteTo, secondTo);
        usingTimes.push({usingFrom: from, usingTill: to});
      }
    }
    if(month < monthTo) {
      let lastDay = new Date(dateFrom.getFullYear(), dateFrom.getMonth() + 1, 0);
      
      for(let i = day; i <= day + (lastDay.getDate() - day) + dayTo; i++) {        
        const from = new Date(year, month-1, i, hour+1, minute, second);
        const to = new Date(yearTo, month-1, i, hourTo+1, minuteTo, secondTo);
        usingTimes.push({usingFrom: from, usingTill: to})
      }
    }
    if(usingTimes.length == 0) {
      usingTimes.push({usingFrom: dateFrom, usingTill: dateTo});
    }

    const booking = {
      userId: this.space['userId'],
      idOfSpaceBooked: this.space['id'],
      usingTimes: [
        ...usingTimes
      ],
      status: BookingStatus.Booked,
      bookedById: this.currentUser['id'],
      totalPrice: 0,
      noOfGuests: 0,
      createdByOwner: true
    };
    console.log(booking);    
    this.spaceStore.dispatch(new bookingActions.CreateReservation(booking));
    // this.router.navigate(['/profile'], {relativeTo: this.route});
  }

}
