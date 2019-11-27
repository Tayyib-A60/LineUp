import { ManageMerchantsService } from './../manage-merchants.service';
import { Injectable } from '@angular/core';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { SpaceService } from '../space.service';
import { SpaceActionTypes } from './space.action.types';
import * as spaceActions from './space.actions';
import { mergeMap, map, catchError } from 'rxjs/operators';
import { SpaceType } from '../models/spaceType.model';
import { of, Observable } from 'rxjs';
import { Action } from '@ngrx/store';
import { Amenity } from '../models/amenity.model';
import { Space } from '../models/space.model';
import { SpaceQueryResult } from '../models/spaceQueryResult';
import { NotificationService } from '../../services/notification.service';
import { QueryResult } from '../models/queryResult.model';

@Injectable()
export class SpaceEffects {
    merchantId = localStorage.getItem('currentUser')? JSON.parse(localStorage.getItem('currentUser'))['id'] : 0;
    query = {
        currentPage: 1,
        pageSize: 10
    };
    merchantQuery = {
        userId: this.merchantId,
        currentPage: 1,
        pageSize: 10
    };
    constructor(private actions$: Actions,
                private spaceService: SpaceService,
                private notify: NotificationService,
                private merchantService: ManageMerchantsService) { }

    // @Effect()
    // successNotification$ = this.actions$.pipe(
    //     ofType(SpaceActionTypes.GetSpaceTypesSuccess),
    //     map((action: spaceActions.SuccessNotification) => {
    //         this.notify.typeSuccess(action.payload, 'Success!');
    //     })
    // );
    
    // @Effect()
    // failureNotification$ = this.actions$.pipe(
    //     ofType(SpaceActionTypes.FailureNotification),
    //     map((action: spaceActions.FailureNotification) => {
    //         this.notify.typeError(action.payload, 'Error!')
    //     })
    // );
    
    @Effect()
    createSpaceType$: Observable<Action> = this.actions$.pipe(
        ofType(SpaceActionTypes.CreateSpaceType),
        map((action: spaceActions.CreateSpaceType) => action.payload),
        mergeMap((spaceType: SpaceType) => 
            this.spaceService.createSpaceType(spaceType).pipe(
                map(response => new spaceActions.CreateSpaceTypeSuccess(response) && new spaceActions.SuccessNotification('Space type created successfully')
                ),
                catchError(err => of(new spaceActions.CreateSpaceTypeFailure(err) && new spaceActions.FailureNotification('Failed to create space type')) )
                )
            )
    );
    
    @Effect()
    createSpace$: Observable<Action> = this.actions$.pipe(
        ofType(SpaceActionTypes.CreateSpace),
        map((action: spaceActions.CreateSpace) => action.payload),
        mergeMap((space: Space) => 
            this.spaceService.createSpace(space).pipe(
                map(response => new spaceActions.CreateSpaceSuccess(response) && new spaceActions.SuccessNotification('Space created successfully') ),
                catchError(err => of(new spaceActions.CreateSpaceFailure(err) && new spaceActions.FailureNotification(`${err.message}`)))
            )
        )
    );

    @Effect()
    updateSpace$: Observable<Action> = this.actions$.pipe(
        ofType(SpaceActionTypes.UpdateSpace),
        map((action: spaceActions.UpdateSpace) => action.payload),
        mergeMap((space: Space) => 
            this.spaceService.updateSpace(space.id, space).pipe(
                map(response => new spaceActions.UpdateSpaceSuccess(response) && new spaceActions.SuccessNotification('Space updated successfully')),
                catchError(err => of(new spaceActions.UpdateSpaceFailure(err) && new spaceActions.FailureNotification(`${err.message}`)))
            )
        )
    );

    @Effect()
    createAmenity$: Observable<Action> = this.actions$.pipe(
        ofType(SpaceActionTypes.CreateAmenity),
        map((action: spaceActions.CreateAmenity) => action.payload),
        mergeMap((amenity: Amenity) => 
            this.spaceService.createAmenity(amenity).pipe(
                map(response => new spaceActions.CreateAmenitySuccess(response) && new spaceActions.SuccessNotification('Amenity created successfully')),
                catchError(err => of(new spaceActions.CreateAmenityFailure(err) && new spaceActions.FailureNotification(`${err.message}`)))
            )
        )
    );

    @Effect()
    getSingleSpace$: Observable<Action> = this.actions$.pipe(
        ofType(SpaceActionTypes.GetSingleSpace),
        mergeMap((action: spaceActions.GetSingleSpace) => this.spaceService.getSpace(action.payload)
            .pipe(
                map((space: Space) => {
                    // console.log(space);
                    
                    return new spaceActions.GetSingleSpaceSuccess(space)}),
                catchError(err => of(new spaceActions.GetSingleSpaceFailure(err) && new spaceActions.FailureNotification(`${err.message}`)))
            ) 
        )
    );

    @Effect()
    getSpaceTypes$: Observable<Action> = this.actions$.pipe(
        ofType(SpaceActionTypes.GetSpaceTypes),
        mergeMap((action: spaceActions.GetSpaceTypes) => this.spaceService.getSpaceTypes()
            .pipe(
                map((spaceTypes: SpaceType[]) => {
                this.notify.typeSuccess('action.payload', 'Success!')
                    return new spaceActions.GetSpaceTypesSuccess(spaceTypes);
                }),
                catchError(err => of(new spaceActions.GetSpaceTypesFailure(err) && new spaceActions.FailureNotification(`${err.message}`)))
            ) 
        )
    );

    @Effect()
    getSpaces$: Observable<Action> = this.actions$.pipe(
        ofType(SpaceActionTypes.GetSpaces),
        mergeMap((action: spaceActions.GetSpaces) => this.spaceService.getSpaces(this.query)
            .pipe(
                map((spaceQueryResult: SpaceQueryResult) => new spaceActions.GetSpacesSuccess(spaceQueryResult)),
                catchError(err => of(new spaceActions.GetSpacesFailure(err) && new spaceActions.FailureNotification(`${err.message}`)))
            )
        )
    );
    
    @Effect()
    getMerchantSpaces$: Observable<Action> = this.actions$.pipe(
        ofType(SpaceActionTypes.GetMerchantSpaces),
        mergeMap((action: spaceActions.GetMerchantSpaces) => this.spaceService.getMerchantSpaces(this.merchantQuery)
            .pipe(
                map((merchantSpaceQR: SpaceQueryResult) => new spaceActions.GetMerchantSpacesSuccess(merchantSpaceQR)),
                catchError(err => of(new spaceActions.GetMerchantSpacesFailure(err) && new spaceActions.FailureNotification(`${err.message}`)))
            )
        )
    );

    @Effect()
    getMerchants$: Observable<Action> = this.actions$.pipe(
        ofType(SpaceActionTypes.GetMerchants),
        mergeMap((action: spaceActions.GetMerchants) => this.spaceService.getMerchants()
            .pipe(
                map((merchants: QueryResult) => new spaceActions.GetMerchantsSuccess(merchants)),
                catchError(err => of(new spaceActions.GetMerchantsFailure(err)))
            )
        )
    );

    @Effect()
    deleteSpace$: Observable<Action> = this.actions$.pipe(
        ofType(SpaceActionTypes.DeleteSpace),
        map((action: spaceActions.DeleteSpace) => action.payload),
        mergeMap((id: number) => 
            this.spaceService.deleteSpace(id).pipe(
                map((response: number) => new spaceActions.DeleteSpaceSuccess(response) && new spaceActions.SuccessNotification('Deleted successfully')),
                catchError(err => of(new spaceActions.DeleteSpaceFailure(err) && new spaceActions.FailureNotification(`${err.message}`)))
            )
        )
    );
    
}