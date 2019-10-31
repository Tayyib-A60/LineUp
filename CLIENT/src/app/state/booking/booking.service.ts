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
        const url = this.url + '/lineUp/user/createReservation';
        return this.httpClient.post(url, booking, { headers });
    }
}