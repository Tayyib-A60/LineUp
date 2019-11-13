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
    constructor(private action$: Actions,
                private spaceService: SpaceService) { }
    
    
    @Effect()
    createSpaceType$: Observable<Action> = this.action$.pipe(
        ofType(SpaceActionTypes.CreateSpaceType),
        map((action: spaceActions.CreateSpaceType) => action.payload),
        mergeMap((spaceType: SpaceType) => 
            this.spaceService.createSpaceType(spaceType).pipe(
                map(response => new spaceActions.CreateSpaceTypeSuccess(response)),
                catchError(err => of(new spaceActions.CreateSpaceTypeFailure(err))
                )
            )
        )
    );
    
    @Effect()
    createSpace$: Observable<Action> = this.action$.pipe(
        ofType(SpaceActionTypes.CreateSpace),
        map((action: spaceActions.CreateSpace) => action.payload),
        mergeMap((space: Space) => 
            this.spaceService.createSpace(space).pipe(
                map(response => new spaceActions.CreateSpaceSuccess(response)),
                catchError(err => of(new spaceActions.CreateSpaceFailure(err)))
            )
        )
    );

    @Effect()
    updateSpace$: Observable<Action> = this.action$.pipe(
        ofType(SpaceActionTypes.UpdateSpace),
        map((action: spaceActions.UpdateSpace) => action.payload),
        mergeMap((space: Space) => 
            this.spaceService.updateSpace(space.id, space).pipe(
                map(response => new spaceActions.UpdateSpaceSuccess(response)),
                catchError(err => of(new spaceActions.UpdateSpaceFailure(err)))
            )
        )
    );

    @Effect()
    createAmenity$: Observable<Action> = this.action$.pipe(
        ofType(SpaceActionTypes.CreateAmenity),
        map((action: spaceActions.CreateAmenity) => action.payload),
        mergeMap((amenity: Amenity) => 
            this.spaceService.createAmenity(amenity).pipe(
                map(response => new spaceActions.CreateAmenitySuccess(response)),
                catchError(err => of(new spaceActions.CreateAmenityFailure(err)))
            )
        )
    );

    @Effect()
    getSingleSpace$: Observable<Action> = this.action$.pipe(
        ofType(SpaceActionTypes.GetSingleSpace),
        mergeMap((action: spaceActions.GetSingleSpace) => this.spaceService.getSpace(action.payload)
            .pipe(
                map((space: Space) => new spaceActions.GetSingleSpaceSuccess(space)),
                catchError(err => of(new spaceActions.GetSingleSpaceFailure(err)))
            ) 
        )
    );

    @Effect()
    getSpaceTypes$: Observable<Action> = this.action$.pipe(
        ofType(SpaceActionTypes.GetSpaceTypes),
        mergeMap((action: spaceActions.GetSpaceTypes) => this.spaceService.getSpaceTypes()
            .pipe(
                map((spaceTypes: SpaceType[]) => new spaceActions.GetSpaceTypesSuccess(spaceTypes)),
                catchError(err => of(new spaceActions.GetSpaceTypesFailure(err)))
            ) 
        )
    );

    @Effect()
    getSpaces$: Observable<Action> = this.action$.pipe(
        ofType(SpaceActionTypes.GetSpaces),
        mergeMap((action: spaceActions.GetSpaces) => this.spaceService.getSpaces(this.query)
            .pipe(
                map((spaceQueryResult: SpaceQueryResult) => new spaceActions.GetSpacesSuccess(spaceQueryResult)),
                catchError(err => of(new spaceActions.GetSpacesFailure(err)))
            )
        )
    );
    
    @Effect()
    getMerchantSpaces$: Observable<Action> = this.action$.pipe(
        ofType(SpaceActionTypes.GetMerchantSpaces),
        mergeMap((action: spaceActions.GetMerchantSpaces) => this.spaceService.getMerchantSpaces(this.merchantQuery)
            .pipe(
                map((merchantSpaceQR: SpaceQueryResult) => new spaceActions.GetMerchantSpacesSuccess(merchantSpaceQR)),
                catchError(err => of(new spaceActions.GetMerchantSpacesFailure(err)))
            )
        )
    );

    @Effect()
    deleteSpace$: Observable<Action> = this.action$.pipe(
        ofType(SpaceActionTypes.DeleteSpace),
        map((action: spaceActions.DeleteSpace) => action.payload),
        mergeMap((id: number) => 
            this.spaceService.deleteSpace(id).pipe(
                map((response: number) => new spaceActions.DeleteSpaceSuccess(response)),
                catchError(err => of(new spaceActions.DeleteSpaceFailure(err)))
            )
        )
    );
    
}