import { createFeatureSelector, createSelector } from '@ngrx/store';

import { SpaceState } from './space.reducers';

export const getSpaceFeatureState = createFeatureSelector<SpaceState>('spaces');

export const getSpaces = createSelector(
    getSpaceFeatureState,
    spaceState => spaceState.spaces
);

export const getSpaceTypes = createSelector(
    getSpaceFeatureState,
    spaceState => spaceState.spaceTypes
);

export const getSingleSpace = createSelector(
    getSpaceFeatureState,
    spaceState => spaceState.spaceToEdit
);

export const getError = createSelector(
    getSpaceFeatureState,
    spaceState => spaceState.error
);