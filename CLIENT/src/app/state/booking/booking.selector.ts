import { createFeatureSelector, createSelector } from '@ngrx/store';
import { BookingState } from './booking.reducer';

export const getBookingFeatureState = createFeatureSelector<BookingState>('booking');

export const getCurrentBooking = createSelector(
    getBookingFeatureState,
    bookingState => bookingState.currentBooking
);

export const getBookings = createSelector(
    getBookingFeatureState,
    bookingState => bookingState.Bookings
);