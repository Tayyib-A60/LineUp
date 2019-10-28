import { Action } from '@ngrx/store';
import { SpaceActionTypes } from './space.action.types';
import { Space } from '../models/space.model';
import { SpaceType } from '../models/spaceType.model';
import { Amenity } from '../models/amenity.model';
import { SpaceQueryResult } from '../models/spaceQueryResult';

export class CreateSpace implements Action {
    readonly type = SpaceActionTypes.CreateSpace;

    constructor(public payload: Space) {
        this.type = SpaceActionTypes.CreateSpace;
    }
}
export class CreateSpaceSuccess implements Action {
    readonly type = SpaceActionTypes.CreateSpaceSuccess;

    constructor(public payload: any) {
        this.type = SpaceActionTypes.CreateSpaceSuccess;
    }
}
export class CreateSpaceFailure implements Action {
    readonly type = SpaceActionTypes.CreateSpaceFailure;

    constructor(public payload: string) {
        this.type = SpaceActionTypes.CreateSpaceFailure;
    }
}

export class UpdateSpace implements Action {
    readonly type = SpaceActionTypes.UpdateSpace;

    constructor(public payload: Space) {
        this.type = SpaceActionTypes.UpdateSpace;
    }
}

export class UpdateSpaceSuccess implements Action {
    readonly type = SpaceActionTypes.UpdateSpaceSuccess;

    constructor(public payload: any) {
        this.type = SpaceActionTypes.UpdateSpaceSuccess;
    }
}
export class UpdateSpaceFailure implements Action {
    readonly type = SpaceActionTypes.UpdateSpaceFailure;

    constructor(public payload: string) {
        this.type = SpaceActionTypes.UpdateSpaceFailure;
    }
}

export class DeleteSpace implements Action {
    readonly type = SpaceActionTypes.DeleteSpace;

    constructor(public payload: number) {
        this.type = SpaceActionTypes.DeleteSpace;
    }
}

export class DeleteSpaceSuccess implements Action {
    readonly type = SpaceActionTypes.DeleteSpaceSuccess;

    constructor(public payload: number) {
        this.type = SpaceActionTypes.DeleteSpaceSuccess;
    }
}

export class DeleteSpaceFailure implements Action {
    readonly type = SpaceActionTypes.DeleteSpaceFailure;

    constructor(public payload: string) {
        this.type = SpaceActionTypes.DeleteSpaceFailure;
    }
}
export class GetSpaces implements Action {
    readonly type = SpaceActionTypes.GetSpaces;

    constructor() {
        this.type = SpaceActionTypes.GetSpaces;
    }
}
export class GetSpacesSuccess implements Action {
    readonly type = SpaceActionTypes.GetSpacesSuccess;

    constructor(public payload: SpaceQueryResult) {
        this.type = SpaceActionTypes.GetSpacesSuccess;
    }
}
export class GetSpacesFailure implements Action {
    readonly type = SpaceActionTypes.GetSpacesFailure;

    constructor(public payload: string) {
        this.type = SpaceActionTypes.GetSpacesFailure;
    }
}

export class GetSingleSpace implements Action {
    readonly type = SpaceActionTypes.GetSingleSpace;

    constructor(public payload: number) {
        this.type = SpaceActionTypes.GetSingleSpace;
    }
}
export class GetSingleSpaceSuccess implements Action {
    readonly type = SpaceActionTypes.GetSingleSpaceSuccess;

    constructor(public payload: Space) {
        this.type = SpaceActionTypes.GetSingleSpaceSuccess;
    }
}
export class GetSingleSpaceFailure implements Action {
    readonly type = SpaceActionTypes.GetSingleSpaceFailure;

    constructor(public payload: any) {
        this.type = SpaceActionTypes.GetSingleSpaceFailure;
    }
}

export class CreateSpaceType implements Action {
    readonly type = SpaceActionTypes.CreateSpaceType;

    constructor(public payload: SpaceType) {
        this.type = SpaceActionTypes.CreateSpaceType;
    }
}
export class CreateSpaceTypeSuccess implements Action {
    readonly type = SpaceActionTypes.CreateSpaceTypeSuccess;

    constructor(public payload: any) {
        this.type = SpaceActionTypes.CreateSpaceTypeSuccess;
    }
}
export class CreateSpaceTypeFailure implements Action {
    readonly type = SpaceActionTypes.CreateSpaceTypeFailure;

    constructor(public payload: string) {
        this.type = SpaceActionTypes.CreateSpaceTypeFailure;
    }
}

export class UpdateSpaceType implements Action {
    readonly type = SpaceActionTypes.UpdateSpaceType;

    constructor(public payload: SpaceType) {
        this.type = SpaceActionTypes.UpdateSpaceType;
    }
}
export class UpdateSpaceTypeSuccess implements Action {
    readonly type = SpaceActionTypes.UpdateSpaceTypeSuccess;

    constructor(public payload: SpaceType) {
        this.type = SpaceActionTypes.UpdateSpaceTypeSuccess;
    }
}
export class UpdateSpaceTypeFailure implements Action {
    readonly type = SpaceActionTypes.UpdateSpaceTypeFailure;

    constructor(public payload: string) {
        this.type = SpaceActionTypes.UpdateSpaceTypeFailure;
    }
}

export class DeleteSpaceType implements Action {
    readonly type = SpaceActionTypes.DeleteSpaceType;

    constructor(public payload: number) {
        this.type = SpaceActionTypes.DeleteSpaceType;
    }
}
export class DeleteSpaceTypeSuccess implements Action {
    readonly type = SpaceActionTypes.DeleteSpaceTypeSuccess;

    constructor(public payload: number) {
        this.type = SpaceActionTypes.DeleteSpaceTypeSuccess;
    }
}
export class DeleteSpaceTypeFailure implements Action {
    readonly type = SpaceActionTypes.DeleteSpaceTypeFailure;

    constructor(public payload: string) {
        this.type = SpaceActionTypes.DeleteSpaceTypeFailure;
    }
}
export class GetSpaceTypes implements Action {
    readonly type = SpaceActionTypes.GetSpaceTypes;

    constructor() {
        this.type = SpaceActionTypes.GetSpaceTypes;
    }
}
export class GetSpaceTypesSuccess implements Action {
    readonly type = SpaceActionTypes.GetSpaceTypesSuccess;

    constructor(public payload: SpaceType[]) {
        this.type = SpaceActionTypes.GetSpaceTypesSuccess;
    }
}
export class GetSpaceTypesFailure implements Action {
    readonly type = SpaceActionTypes.GetSpaceTypesFailure;

    constructor(public payload: string) {
        this.type = SpaceActionTypes.GetSpaceTypesFailure;
    }
}
export class CreateAmenity implements Action {
    readonly type = SpaceActionTypes.CreateAmenity;

    constructor(public payload: Amenity) {
        this.type = SpaceActionTypes.CreateAmenity;
    }
}
export class CreateAmenitySuccess implements Action {
    readonly type = SpaceActionTypes.CreateAmenitySuccess;

    constructor(public payload: any) {
        this.type = SpaceActionTypes.CreateAmenitySuccess;
    }
}
export class CreateAmenityFailure implements Action {
    readonly type = SpaceActionTypes.CreateAmenityFailure;

    constructor(public payload: string) {
        this.type = SpaceActionTypes.CreateAmenityFailure;
    }
}

export type SpaceActions = CreateSpace | UpdateSpace |
DeleteSpace | CreateSpaceType | UpdateSpaceType | DeleteSpaceType
| CreateSpaceSuccess | CreateSpaceFailure | UpdateSpaceSuccess | UpdateSpaceFailure
| DeleteSpaceSuccess | DeleteSpaceFailure | CreateSpaceTypeSuccess | CreateSpaceTypeFailure
| UpdateSpaceTypeSuccess | UpdateSpaceTypeFailure | DeleteSpaceTypeSuccess | DeleteSpaceTypeFailure
| CreateAmenity | CreateAmenitySuccess | CreateAmenityFailure | GetSpaces | GetSpacesSuccess
| GetSpacesFailure | GetSpaceTypes | GetSpaceTypesSuccess | GetSpaceTypesFailure | GetSingleSpace
| GetSingleSpaceSuccess | GetSingleSpaceFailure; 