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
    }
  ];
  currentOrientation = 'horizontal';
  currentUser: any;
  reservations = [];
  componentActive = true;
  merchantId = localStorage.getItem('currentUser')? 
  JSON.parse(localStorage.getItem('currentUser'))['id'] : 0;
  dateStart: any;
  dateEnd: any;
  currentPage = 1;
  searchString: string;
  merchantBookingQuery = {
    userId: this.merchantId,
    currentPage: this.currentPage,
    pageSize: 10,
    status: 'Reserved',
    searchString: this.searchString,
    dateStart: '01/01/0001',
    dateEnd: '01/01/0001'
  };
  upcomingReservations: any[];
  previousReservations: any[];
  reservationQueryResult: any;
  

  constructor(private bookingStore: Store<BookingState>) { }

  ngOnInit() {
    this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
    this.fetchReservations();
    
  }

  search() {
    const dates = this.formatDates();
    this.merchantBookingQuery = { ...this.merchantBookingQuery, dateStart: dates.dateStart.toDateString(), dateEnd: dates.dateEnd.toDateString(), searchString: this.searchString };
    this.fetchReservations();
  }

  onPageChange(page: number) {
    this.merchantBookingQuery = { ...this.merchantBookingQuery, currentPage: page };
    this.currentPage = page;

    this.fetchReservations();
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

  private fetchReservations() {
    this.bookingStore.dispatch(new bookingActions.GetMerchantBookings(this.merchantBookingQuery));
    this.bookingStore.pipe(select(bookingSelectors.getMerchantBookings),
                      takeWhile(() => this.componentActive))
                      .subscribe(reservations => {
                        console.log(reservations);
                        this.reservations = reservations['items'];
                        if(this.reservations) {
                          this.upcomingReservations = [];
                          this.previousReservations = [];
                          reservations['items'].forEach(item => {
                            if(item['usingFrom'] > Date()) {
                              console.log('is prev');
                              
                              this.upcomingReservations.push(item);
                            } else if(item['usingFrom'] < Date()){
                              console.log('is upcoming');
                              this.previousReservations.push(item);
                            }
                          });                                      
                        }             
                  });
  }

}
