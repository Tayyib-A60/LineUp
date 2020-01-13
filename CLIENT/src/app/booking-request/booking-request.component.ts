import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Store, select } from '@ngrx/store';
import { SpaceState } from '../spaces/state/space.reducers';
import * as spaceActions from './../spaces/state/space.actions';
import * as spaceSelectors from './../spaces/state/space.selector';
import * as bookingSelector from '../state/booking/booking.selector';
import { takeWhile } from 'rxjs/operators';
import { Space } from '../spaces/models/space.model';
import { BookingState } from '../state/booking/booking.reducer';
@Component({
  selector: 'app-booking-request',
  templateUrl: './booking-request.component.html',
  styleUrls: ['./booking-request.component.scss']
})
export class BookingRequestComponent implements OnInit {
  id: any;
  componentActive = true;
  space: Space;
  loaded: boolean;
  booking: any;
  freeAmenities = [];

  constructor(private route: ActivatedRoute,
              private spaceStore: Store<SpaceState>,
              private bookingStore: Store<BookingState>) { }

  ngOnInit() {
    this.route.params.subscribe(
      (params: Params) => {
      this.id = params['id'];
    });
    this.booking = JSON.parse(localStorage.getItem('bookingToCreate'));
    console.log(this.booking);
    
    this.spaceStore.dispatch(new spaceActions.GetSingleSpace(Number(this.id)));

    this.spaceStore.pipe(select(spaceSelectors.getSingleSpace),
        takeWhile(() => this.componentActive))
        .subscribe(space => {
          this.space = space;
          this.loaded = true;
          if(space) {
            space.amenities.forEach((amenity) => {
              if(amenity.price == 0) {
                this.freeAmenities.push(amenity);
              }
            });
          } 
    });

    // this.bookingStore.pipe(select(bookingSelector.getBookingToCreate),
    //     takeWhile(() => this.componentActive))
    //     .subscribe(booking => {
    //       this.booking = booking;
    //       this.loaded = true; 
    // });
  }

}
