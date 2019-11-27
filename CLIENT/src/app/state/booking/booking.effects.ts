import { Actions, Effect, ofType } from '@ngrx/effects';
import { Injectable } from '@angular/core';
import { BookingService } from './booking.service';
import * as bookingActions from './booking.actions';
import { BookingActionTypes } from './booking.action.types';
import { mergeMap, map, catchError } from 'rxjs/operators';
import { of, Observable } from 'rxjs';
import { Action } from '@ngrx/store';
import { NotificationService } from '../../services/notification.service';

@Injectable()
export class BookingEffects {

    constructor(private actions$: Actions,
                private bookingService: BookingService,
                private notify: NotificationService) { }

    // @Effect()
    // successNotification$ = this.actions$.pipe(
    //     ofType(BookingActionTypes.BookingSuccessNotification),
    //     map((action: bookingActions.BookingSuccessNotification) => {
    //         this.notify.typeSuccess(action.payload, 'Success!')
    //     })
    // );

    // @Effect()
    // failureNotification$ = this.actions$.pipe(
    //     ofType(BookingActionTypes.BookingFailureNotification),
    //     map((action: bookingActions.BookingFailureNotification) => {
    //         this.notify.typeSuccess(action.payload, 'Success!')
    //     })
    // );

    @Effect()
    createReservation$: Observable<Action> = this.actions$.pipe(
        ofType(BookingActionTypes.CreateReservation),
        map((action: bookingActions.CreateReservation) => action.payload),
        mergeMap((booking: any) =>
            this.bookingService.createReservation(booking).pipe(
                map(res => new bookingActions.CreateReservationSuccess(res)),
                catchError(err => of(new bookingActions.CreateReservationFailure(err)))
            )
        )
    );

    @Effect()
    getCustomerBookings$: Observable<Action> = this.actions$.pipe(
        ofType(BookingActionTypes.GetCustomerBookings),
        map((action: bookingActions.GetCustomerBookings) => action.payload),
        mergeMap((customerId: number) =>
            this.bookingService.getCustomerReservations(customerId).pipe(
                map(res => new bookingActions.GetCustomerBookingsSuccess(res)),
                catchError(err => of(new bookingActions.GetCustomerBookingsFailure(err)))
            )
        )
    );
    @Effect()
    getBookingTimes$: Observable<Action> = this.actions$.pipe(
        ofType(BookingActionTypes.GetBookingTimes),
        map((action: bookingActions.GetBookingTimes) => action.payload),
        mergeMap((requestBody: any) =>
            this.bookingService.getBookingTimes(requestBody).pipe(
                map(res => new bookingActions.GetBookingTimesSuccess(res) ),
                catchError(err => of(new bookingActions.GetBookingTimesFailure(err)))
            )
        )
    );

    @Effect()
    getMerchantBookings$: Observable<Action> = this.actions$.pipe(
        ofType(BookingActionTypes.GetMerchantBookings),
        map((action: bookingActions.GetMerchantBookings) => action.payload),
        mergeMap((merchantId: number) =>
            this.bookingService.getMerchantReservations(merchantId).pipe(
                map(res => new bookingActions.GetMerchantBookingsSuccess(res)
                 ),
                catchError(err => of(new bookingActions.GetMerchantBookingsFailure(err) ))
            )
        )
    );

    // @Effect()
    // getMerchantBookings$: Observable<Action> = this.actions$.pipe(
    //     ofType(BookingActionTypes.GetCustomerBookings),
    //     map((action: bookingActions.GetCustomerBookings) => action.payload),
    //     mergeMap((customerId: number) =>
    //         this.bookingService.getCustomerReservations(customerId).pipe(
    //             map(res => new bookingActions.GetCustomerBookingsSuccess(res)),
    //             catchError(err => of(new bookingActions.GetCustomerBookingsFailure(err)))
    //         )
    //     )
    // );
}