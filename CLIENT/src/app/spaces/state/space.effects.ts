import { Injectable } from '@angular/core';
import { Actions, Effect, ofType, EffectsModule } from '@ngrx/effects';
import { SpaceService } from '../space.service';
import { SpaceActionTypes } from './space.action.types';
import * as spaceActions from './space.actions';
import { mergeMap, map, catchError } from 'rxjs/operators';
import { SpaceType } from '../models/spaceType.model';
import { of, Observable } from 'rxjs';
import { Action } from '@ngrx/store';
import { Amenity } from '../models/amenity.model';
import { Space } from '../models/space.model';
import { GetSingleSpaceSuccess, UpdateSpace } from './space.actions';

@Injectable()
export class SpaceEffects {

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
}