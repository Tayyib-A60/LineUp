import { takeWhile } from 'rxjs/operators';
import { Component, OnInit } from '@angular/core';
import * as bookingActions from '../../state/booking/booking.actions';
import * as bookingSelectors from '../../state/booking/booking.selector';
import { BookingState } from '../../state/booking/booking.reducer';
import { State, Store, select } from '@ngrx/store';
import { Router } from '@angular/router';

@Component({
  selector: 'app-managebookings',
  templateUrl: './managebookings.component.html',
  styleUrls: ['./managebookings.component.scss']
})
export class ManagebookingsComponent implements OnInit {

  currentUser: any;
  bookings = [];
  componentActive = true;
  
  constructor(private bookingStore: Store<BookingState>,
              private router: Router) { }

  ngOnInit() {
    this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
    this.bookingStore.dispatch(new bookingActions.GetMerchantBookings(this.currentUser['id']));
    this.bookingStore.pipe(select(bookingSelectors.getMerchantBookings),
                      takeWhile(() => this.componentActive))
                      .subscribe(bookings => {
                        console.log(bookings);
                        this.bookings = bookings['items'];
                        if(this.bookings) {
                          const currentBookingTime = this.bookings[0]['usingFrom'];
                          console.log(new Date(currentBookingTime));
                        }             
    });
  }

}
