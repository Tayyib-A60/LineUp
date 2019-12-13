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
  currentOrientation = 'horizontal'
  bookingQueryResult = {totalItems: 0, items: [] }
  currentUser: any;
  searchString: string;
  bookings = [];
  componentActive = true;
  currentPage = 1;
  capacities = [
    {label: 'Up to 50', capacity: 50},
    {label: 'Up to 100', capacity: 50},
    {label: 'Up to 200', capacity: 199},
    {label: 'Up to 500', capacity: 499},
    {label: 'Up to 1000', capacity: 1000},
    {label: 'More than 1000', capacity: 1000}
  ];
  selectedCapacity: any;
  merchantId = localStorage.getItem('currentUser')? 
  JSON.parse(localStorage.getItem('currentUser'))['id'] : 0;
  dateStart: any;
  dateEnd: any;
  merchantBookingQuery = {
    userId: this.merchantId,
    currentPage: this.currentPage,
    pageSize: 1,
    searchString: this.searchString,
    dateStart: '01/01/0001',
    dateEnd: '01/01/0001'
  };

  constructor(private bookingStore: Store<BookingState>,
              private router: Router) { }

  ngOnInit() {
    this.currentUser = JSON.parse(localStorage.getItem('currentUser'));

    this.bookingStore.dispatch(new bookingActions.GetMerchantBookings(this.merchantBookingQuery));
    this.bookingStore.pipe(select(bookingSelectors.getMerchantBookings),
                      takeWhile(() => this.componentActive))
                      .subscribe(bookings => {
                        this.bookingQueryResult = <{totalItems: 0, items: []}>bookings;
                        this.bookings = bookings['items'];
                        // if(this.bookings) {
                        //   const currentBookingTime = this.bookings[0]['usingFrom'];
                        // }             
    });
  }

  

  search() {
    const dates = this.formatDates();
    this.merchantBookingQuery = { ...this.merchantBookingQuery, dateStart: dates.dateStart.toDateString(), dateEnd: dates.dateEnd.toDateString(), searchString: this.searchString };
    
    this.bookingStore.dispatch(new bookingActions.GetMerchantBookings(this.merchantBookingQuery));
    this.bookingStore.pipe(select(bookingSelectors.getMerchantBookings),
                      takeWhile(() => this.componentActive))
                      .subscribe(bookings => {
                        this.bookingQueryResult = <{totalItems: 0, items: []}>bookings;
                        console.log(this.bookingQueryResult);
                        this.bookings = bookings['items'];
                        if(this.bookings.length > 1) {
                          const currentBookingTime = this.bookings[0]['usingFrom'];
                          // console.log(new Date(currentBookingTime));
                      }             
    });
  }

  onPageChange(page: number) {
    this.merchantBookingQuery = { ...this.merchantBookingQuery, currentPage: page };
    this.currentPage = page;

    this.bookingStore.dispatch(new bookingActions.GetMerchantBookings(this.merchantBookingQuery));
    
    this.bookingStore.pipe(select(bookingSelectors.getMerchantBookings),
                      takeWhile(() => this.componentActive))
                      .subscribe(bookings => {
                        this.bookingQueryResult = <{totalItems: 0, items: []}>bookings;
                        this.bookings = bookings['items'];
                        if(this.bookings) {
                          const currentBookingTime = this.bookings[0]['usingFrom'];
                          console.log(new Date(currentBookingTime));
                        }             
    });
  }

  private formatDates() {
    const { year, month, day } = this.dateStart;
    let yearTo = this.dateEnd['year'];
    let monthTo = this.dateEnd['month'];
    let dayTo = this.dateEnd['day'];
    
    const dateFrom = new Date(year, month-1, day);
    const dateTo = new Date(yearTo, monthTo-1, dayTo);
    console.log(dateFrom, dateTo);
    
    return { dateStart: dateFrom, dateEnd: dateTo };  
  }

}
