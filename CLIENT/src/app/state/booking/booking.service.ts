import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Injectable({
    providedIn: 'root'
})
export class BookingService {
    url = environment.url;

    constructor(private httpClient: HttpClient) { }

    createReservation(booking: any) {
        const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
        const url = this.url + '/lineUp/createBooking';
        return this.httpClient.post(url, booking, { headers });
    }
    
    getCustomerReservations(id: number) {
        const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
        const url = this.url + `/lineUp/getCustomerBookings/${id}`;
        return this.httpClient.get(url, { headers });
    }
    getMerchantReservations(id: number) {
        const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
        const url = this.url + `/lineUp/getMerchantBookings/${id}`;
        return this.httpClient.get(url, { headers });
    }

    getBookingTimes(requestBody: any) {
        const {From, To} = requestBody;
        const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
        const url = this.url + `/lineUp/getBookedTimes/${requestBody['id']}`;
        return this.httpClient.post(url,{From, To}, { headers });
    }
}