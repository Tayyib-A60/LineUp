import { Action } from '@ngrx/store';
import { BookingActionTypes } from './booking.action.types';

export class CreateBooking implements Action {
    readonly type = BookingActionTypes.CreateBooking;

    constructor(public payload: any) {
        this.type = BookingActionTypes.CreateBooking;
    }
}
export class CreateBookingSuccess implements Action {
    readonly type = BookingActionTypes.CreateBookingSuccess;

    constructor(public payload: any) {
        this.type = BookingActionTypes.CreateBookingSuccess;
    }
}
export class CreateBookingFailure implements Action {
    readonly type = BookingActionTypes.CreateBookingFailure;

    constructor(public payload: any) {
        this.type = BookingActionTypes.CreateBookingFailure;
    }
}
export class CreateReservation implements Action {
    readonly type = BookingActionTypes.CreateReservation;

    constructor(public payload: any) {
        this.type = BookingActionTypes.CreateReservation;
    }
}
export class CreateReservationSuccess implements Action {
    readonly type = BookingActionTypes.CreateReservationSuccess;

    constructor(public payload: any) {
        this.type = BookingActionTypes.CreateReservationSuccess;
    }
}
export class CreateReservationFailure implements Action {
    readonly type = BookingActionTypes.CreateReservationFailure;

    constructor(public payload: any) {
        this.type = BookingActionTypes.CreateReservationFailure;
    }
}

export type BookingActions = CreateBooking | CreateBookingSuccess | CreateBookingFailure
| CreateReservation | CreateReservationSuccess | CreateReservationFailure;