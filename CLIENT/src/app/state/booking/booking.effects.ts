import { Actions, Effect, ofType } from '@ngrx/effects';
import { Injectable } from '@angular/core';
import { BookingService } from './booking.service';
import * as bookingActions from './booking.actions';
import { BookingActionTypes } from './booking.action.types';
import { mergeMap, map, catchError } from 'rxjs/operators';
import { of, Observable } from 'rxjs';
import { Action } from '@ngrx/store';

@Injectable()
export class BookingEffects {

    constructor(private actions$: Actions,
                private bookingService: BookingService) { }

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
}