import { AppState } from '../../state/app.state';
import { Space } from '../models/space.model';
import { SpaceType } from '../models/spaceType.model';
import { SpaceActions } from './space.actions';
import { SpaceActionTypes } from './space.action.types';
import { Amenity } from '../models/amenity.model';
import { CreateAmenityFailure, GetSingleSpaceFailure } from './space.actions';
import { act } from '@ngrx/effects';


export interface AppState extends AppState {
    spaces: SpaceState;
}

export interface SpaceState {
    spaces: Space[];
    spaceTypes: SpaceType[];
    currentSpaceId: number;
    amenities: Amenity[];
    error: string;
    response: string;
    spaceToEdit: Space
}

const INITIAL_STATE: SpaceState = {
    spaces: [],
    spaceTypes: [],
    currentSpaceId: null,
    error: '',
    response: '',
    amenities: [],
    spaceToEdit: <Space>{}
}

export function reducer(state = INITIAL_STATE, action: SpaceActions): SpaceState {
    switch (action.type) {
        case SpaceActionTypes.CreateSpaceSuccess:
            return {
                ...state,
                response: action.payload,
                error: ''
            };
        case SpaceActionTypes.CreateSpaceFailure:
            return {
                ...state,
                error: action.payload
            };
        case SpaceActionTypes.UpdateSpaceSuccess:
            // const updatedSpaces = state.spaces.map(
            //     space => action.payload.id === space.id ? action.payload : space
            // );
            return {
                ...state,
                response: action.payload,
                error: ''
            };
        case SpaceActionTypes.UpdateSpaceFailure:
            return {
                ...state,
                error: action.payload
            };
        case SpaceActionTypes.DeleteSpaceSuccess:
            return {
                ...state,
                spaces: state.spaces.filter(space => space.id !== action.payload),
                error: ''
            };
        case SpaceActionTypes.DeleteSpaceFailure:
            return {
                ...state,
                error: action.payload
            };
        case SpaceActionTypes.CreateSpaceTypeSuccess:
            console.log(action.payload);
            return {
                ...state,
                response: action.payload,
                error: ''
            };
            case SpaceActionTypes.CreateSpaceTypeFailure:
            console.log(action.payload);
            return {
                ...state,
                error: action.payload
            };
        case SpaceActionTypes.UpdateSpaceTypeSuccess:
            const updatedSpaceTypes = state.spaceTypes.map(
                spaceType => action.payload.id === spaceType.id ? action.payload : spaceType
            );
            return {
                ...state,
                spaceTypes: updatedSpaceTypes,
                error: ''
            };
        case SpaceActionTypes.UpdateSpaceTypeFailure:
            return {
                ...state,
                error: action.payload
            }
        case SpaceActionTypes.DeleteSpaceTypeSuccess:
            return {
                ...state,
                spaceTypes: state.spaceTypes.filter(spaceType => spaceType.id !== spaceType.id)
            };
        case SpaceActionTypes.DeleteSpaceTypeFailure:
            return {
                ...state,
                error: action.payload
            };
        case SpaceActionTypes.CreateAmenitySuccess:
            return {
                ...state,
                response: action.payload,
                error: ''
            };
        case SpaceActionTypes.CreateAmenityFailure:
            return {
                ...state,
                error: action.payload
            };
        case SpaceActionTypes.GetSpaceTypesSuccess:
            return {
                ...state,
                spaceTypes: action.payload,
                error:  ''
            };
        case SpaceActionTypes.GetSpaceTypesFailure:
            return {
                ...state,
                error: action.payload
            };
        case SpaceActionTypes.GetSingleSpaceSuccess:
            return {
                ...state,
                spaceToEdit: action.payload,
                error: ''
            };
        case SpaceActionTypes.GetSingleSpaceFailure:
            return {
                ...state,
                error: action.payload
            };
        default:
            return state;
    }
}




