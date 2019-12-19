import { SharedService } from './../services/shared.service';
import { NotificationService } from './../services/notification.service';
import { Store, select } from '@ngrx/store';
import { Component, OnInit } from '@angular/core';
import { NgbCarouselConfig } from '@ng-bootstrap/ng-bootstrap';
import { ActivatedRoute, Params, Router } from '@angular/router';
import * as spaceActions from '../spaces/state/space.actions';
import * as spaceSelectors from '../spaces/state/space.selector';
import * as userSelectors from '../state/user.selector';
import * as bookingActions from '../state/booking/booking.actions';
import * as bookingSelectors from '../state/booking/booking.selector';
import { SpaceState } from '../spaces/state/space.reducers';
import { takeWhile } from 'rxjs/operators';
import { UserState } from '../state/user.reducers';
import { BookingState } from '../state/booking/booking.reducer';
import { Space, Amenity } from '../spaces/models/space.model';

@Component({
  selector: 'app-new-space',
  templateUrl: './new-space.component.html',
  styleUrls: ['./new-space.component.scss']
})
export class NewSpaceComponent implements OnInit {

  id: number;
  date: any;
  timeFrom = {
    hour: 6,
    minute: 0,
    second: 0
  };
  timeTo = {
    hour: 12,
    minute: 0,
    second: 0
  };
  contactUsForm = {
    name: '',
    email: '',
    phone: '',
    message: '',
    user234SpacesEmail: '',
    spaceOwnerId: 0
  };
  amenitiesSelected = <Amenity[]>[];
  minuteStep = 0;
  meridian = true;
  firstRating: any;
  currentRate: any;
  dateFrom: any;
  dateTo: any;
  isDisabled: any;
  componentActive = true;
  thisSpace = <Space>{};
  currentUser = {};
  totalCost = 0;
  bookingTimes = [];
  loaded: boolean;
  latitude: number;
  longitude: number;
  
  constructor(private carouselConfig: NgbCarouselConfig,
              private route: ActivatedRoute,
              private spaceStore: Store<SpaceState>,
              private userStore: Store<UserState>,
              private bookingStore: Store<BookingState>,
              private router: Router,
              private notification: NotificationService,
              private sharedService: SharedService) {
    carouselConfig.showNavigationArrows = true;
    carouselConfig.interval = 0;
  }
  
  ngOnInit() {
    this.route.params.subscribe(
      (params: Params) => {
      this.id = params['id'];
    });
    this.spaceStore.dispatch(new spaceActions.GetSingleSpace(Number(this.id)));
    this.spaceStore.pipe(select(spaceSelectors.getSingleSpace),
        takeWhile(() => this.componentActive))
        .subscribe(space => {
          console.log(space);
          this.thisSpace = space;
          this.loaded = true; 
    });
    navigator.geolocation.getCurrentPosition((myLocation) => {
      this.latitude = myLocation.coords.latitude;
      this.longitude = myLocation.coords.longitude;
    });
    this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
  }

  amenityChecked(event) {    
    if(event.target.checked) {
      const amenityIndex = this.thisSpace.amenities.findIndex(a => a['id'] === Number(event.target.value));
      this.totalCost += this.thisSpace.amenities[amenityIndex].price;
      this.amenitiesSelected.push(this.thisSpace.amenities[amenityIndex]);
    }
    if(!event.target.checked) {
      const index = this.amenitiesSelected.findIndex(a => a['id'] === Number(event.target.value));
      this.totalCost -= this.amenitiesSelected[index].price;
      this.amenitiesSelected.splice(index, 1);
    }
    // this.amenitiesSelected.forEach(amenity => {
    //   this.totalCost += amenity.price;
    // });
  }

  sendEnquiry() {
    this.contactUsForm.user234SpacesEmail = this.currentUser['email'];
    this.contactUsForm.spaceOwnerId = this.thisSpace.userId;
    this.sharedService.postEnquiry(this.contactUsForm)
    .subscribe((res) => {
      this.notification.typeSuccess('Success', 'Your message was sent successfully')
    }, (err) => {
      this.notification.typeError('Failed', `${err.message}`);
    })
    console.log(this.contactUsForm);
    
  }

  bookSpace() {
    if(this.currentUser === null) {
      this.notification.typeInfo('Please sign in to book this space', 'Info');
      return;
    }
    const { year, month, day } = this.dateFrom;
    let yearTo = this.dateTo['year'];
    let monthTo = this.dateTo['month'];
    let dayTo = this.dateTo['day'];
    console.log(this.timeFrom);
    
    const {hour, minute, second } = this.timeFrom;
    let hourTo = this.timeTo['hour'];
    let minuteTo = this.timeTo['minute'];
    let secondTo = this.timeTo['second'];
    const dateFrom = new Date(year, month-1, day, hour+1, minute, second);
    const dateTo = new Date(yearTo, monthTo-1, dayTo, hourTo+1, minuteTo, secondTo);

    let amenitiesPrice = 0;
    this.amenitiesSelected.forEach(amenity => {
      amenitiesPrice += amenity.price;
    });
    const booking = {
      userId: this.thisSpace['userId'],
      spaceBooked: {
        id: this.thisSpace['id']
      },
      usingFrom: dateFrom,
      usingTill: dateTo,
      status: 'Reserved',
      bookedById: this.currentUser['id'],
      totalPrice: this.thisSpace['price'] + amenitiesPrice
    };
    console.log(booking);    
    this.spaceStore.dispatch(new bookingActions.CreateReservation(booking));
    // this.router.navigate(['/profile'], {relativeTo: this.route});
  }

  checkAvailability() {
    const { year, month, day } = this.dateFrom;
    let yearTo = this.dateTo['year'];
    let monthTo = this.dateTo['month'];
    let dayTo = this.dateTo['day'];
    const {hour, minute, second } = this.timeFrom;
    console.log(this.timeFrom);
    
    let hourTo = this.timeTo['hour'];
    let minuteTo = this.timeTo['minute'];
    let secondTo = this.timeTo['second'];
    const dateFrom = new Date(year, month-1, day, hour+1, minute, second);
    const dateTo = new Date(yearTo, monthTo-1, dayTo, hourTo+1, minuteTo, secondTo);
    const requestBody = {
      id: Number(this.id),
      From: dateFrom,
      To: dateTo
    };
    // console.log(requestBody);
    
    this.spaceStore.dispatch(new bookingActions.GetBookingTimes(requestBody));
    this.spaceStore.pipe(select(bookingSelectors.getBookingTimes),
      takeWhile(() => this.componentActive))
      .subscribe(bookingTimes => {
        this.bookingTimes = bookingTimes? bookingTimes: [];
        console.log(bookingTimes);
    });
  }

  getCustomerBooking() {
    console.log(this.timeFrom, this.timeTo);
    // this.bookingStore.dispatch(new bookingActions.GetCustomerBookings(this.currentUser['id']));
    // this.bookingStore.pipe(select(bookingSelectors.getCustomerBookings),
    // takeWhile(() => this.componentActive))
    // .subscribe(bookingQR => {
    //   console.log(bookingQR);
    // });
  }

}
