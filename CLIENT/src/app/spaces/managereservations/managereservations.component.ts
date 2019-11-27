import { Component, OnInit } from '@angular/core';
import { takeWhile } from 'rxjs/operators';
import * as bookingActions from '../../state/booking/booking.actions';
import * as bookingSelectors from '../../state/booking/booking.selector';
import { BookingState } from '../../state/booking/booking.reducer';
import { Store, select } from '@ngrx/store';
@Component({
  selector: 'app-managereservations',
  templateUrl: './managereservations.component.html',
  styleUrls: ['./managereservations.component.scss']
})
export class ManagereservationsComponent implements OnInit {

  data = [
    {
      id: 1,
      image: 'assets/img/elements/01.png',
      name: "Ethel Price",
      location: "V.I",
      price: "200,000",
      size: 200
    },
    {
      id: 1,
      image: 'assets/img/elements/02.png',
      name: "Ethel Price",
      location: "V.I",
      price: "200,000",
      size: 200
    },
    {
      id: 1,
      image: 'assets/img/elements/03.png',
      name: "Ethel Price",
      location: "V.I",
      price: "200,000",
      size: 200
    },
    {
      id: 1,
      image: 'assets/img/elements/04.png',
      name: "Ethel Price",
      location: "V.I",
      price: "200,000",
      size: 200
    },
  ];
  currentUser: any;
  reservations = [];
  componentActive = true;
  
  constructor(private bookingStore: Store<BookingState>) { }

  ngOnInit() {
    this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
    console.log(this.currentUser);
    
    this.bookingStore.dispatch(new bookingActions.GetMerchantBookings(this.currentUser['id']));
    this.bookingStore.pipe(select(bookingSelectors.getMerchantBookings),
                      takeWhile(() => this.componentActive))
                      .subscribe(reservations => {
                        console.log(reservations);
                        this.reservations = reservations['items'];             
    });
  }

}
