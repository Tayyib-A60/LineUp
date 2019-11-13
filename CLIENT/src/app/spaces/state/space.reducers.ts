import { Space } from '../models/space.model';
import { SpaceType } from '../models/spaceType.model';
import { SpaceActions } from './space.actions';
import { SpaceActionTypes } from './space.action.types';
import { Amenity } from '../models/amenity.model';
import { SpaceQueryResult } from '../models/spaceQueryResult';

export interface SpaceState {
    spaces: Space[];
    spaceQueryResult: SpaceQueryResult;
    merchantSpacesQR: SpaceQueryResult;
    spaceTypes: SpaceType[];
    currentSpaceId: number;
    amenities: Amenity[];
    error: string;
    response: string;
    spaceToEdit: Space
}

const INITIAL_STATE: SpaceState = {
    spaces: [],
    spaceQueryResult: <SpaceQueryResult>{},
    merchantSpacesQR: <SpaceQueryResult>{},
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
            const items = state.spaceQueryResult.items.filter(sp => sp.id !== action.payload);
            return {
                ...state,
                spaceQueryResult: { 
                    totalItems: state.spaceQueryResult.totalItems-1,
                    items 
                },
                error: ''
            };
        case SpaceActionTypes.DeleteSpaceFailure:
            return {
                ...state,
                error: action.payload
            };
        case SpaceActionTypes.GetSpacesSuccess:
            return {
                ...state,
                spaceQueryResult: action.payload,
                error: ''
            };
        case SpaceActionTypes.GetSpacesFailure:
            return {
                ...state,
                error: action.payload
            };
        case SpaceActionTypes.GetMerchantSpacesSuccess:
            return {
                ...state,
                merchantSpacesQR: action.payload,
                error: ''
            };
        case SpaceActionTypes.GetMerchantSpacesFailure:
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




