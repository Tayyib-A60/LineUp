import { BookingActions } from './booking.actions';
import { BookingActionTypes } from './booking.action.types';

export interface BookingState {
    Bookings: any[],
    currentBooking: any,
    error: string,
    response: any
}

const INITIAL_STATE: BookingState = {
    Bookings: [],
    currentBooking: null,
    error: '',
    response: null
};

export function reducer(state = INITIAL_STATE, action: BookingActions) {
    switch (action.type) {
        case BookingActionTypes.CreateBookingSuccess:
            return {
                ...state,
                response: action.payload,
                error: null
            };
        case BookingActionTypes.CreateBookingFailure:
            return {
                ...state,
                response: null,
                error: action.payload
            }
        case BookingActionTypes.CreateReservationSuccess:
            return {
                ...state,
                response: action.payload,
                error: null
            };
        case BookingActionTypes.CreateReservationFailure:
            return {
                ...state,
                response: null,
                error: action.payload
            };
        default:
            return { ...state };
    }
}