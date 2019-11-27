export enum SpaceActionTypes {
    SuccessNotification= 'Success Notification',
    FailureNotification= 'Failure Notification',
    NotificationDisplayed = 'Displayed Notification',
    CreateSpace = '[Space] Create Space',
    UpdateSpace = '[Space] Update Space',
    DeleteSpace = '[Space] Delete Space',
    CreateSpaceSuccess = '[Space] Create Space Success',
    UpdateSpaceSuccess = '[Space] Update Space Success',
    DeleteSpaceSuccess = '[Space] Delete Space Success',
    CreateSpaceFailure = '[Space] Create Space Failure',
    UpdateSpaceFailure = '[Space] Update Space Failure',
    DeleteSpaceFailure = '[Space] Delete Space Failure',
    CreateSpaceType = '[Spacetype] Create Spacetype',
    UpdateSpaceType = '[SpaceType] Update SpaceType',
    DeleteSpaceType = '[SpaceType] Delete SpaceType',
    CreateSpaceTypeSuccess = '[Spacetype] Create SpaceType Success',
    UpdateSpaceTypeSuccess = '[SpaceType] Update SpaceType Success',
    DeleteSpaceTypeSuccess = '[SpaceType] Delete SpaceType Success',
    CreateSpaceTypeFailure = '[Spacetype] Create SpaceType Failure',
    UpdateSpaceTypeFailure = '[SpaceType] Update SpaceType Failure',
    DeleteSpaceTypeFailure = '[SpaceType] Delete SpaceType Failure',
    CreateAmenity = '[Amenity] CreateAmenity',
    CreateAmenitySuccess = '[Amenity] CreateAmenity Success',
    CreateAmenityFailure = '[Amenity] CreateAmenity Failure',
    GetSpaces = '[GetSpaces] Get Spaces',
    GetSpacesSuccess = '[GetSpaces] Get Spaces Success',
    GetSpacesFailure = '[GetSpaces] Get Spaces Failure',
    GetMerchantSpaces = '[GetMerchantSpaces] Get MerchantSpaces',
    GetMerchantSpacesSuccess = '[GetMerchantSpaces] Get MerchantSpaces Success',
    GetMerchantSpacesFailure = '[GetMerchantSpaces] Get MerchantSpaces Failure',
    GetSpaceTypes = '[GetSpacetypes] Get Spacetypes',
    GetSpaceTypesSuccess = '[GetSpacetypes] Get Spacetype Success',
    GetSpaceTypesFailure = '[GetSpacetypes] Get Spacetype Failure',
    GetAmenities = '[GetAmenities] Get Amenities',
    GetAmenitiesSuccess = '[GetAmenities] Get Amenities Success',
    GetAmenitiesFailure = '[GetAmenities] Get Amenities Failure',
    GetSingleSpace = '[GetSingleSpace] Get SingleSpace',
    GetSingleSpaceSuccess = '[GetSingleSpace] Get SingleSpace Success',
    GetSingleSpaceFailure = '[GetSingleSpace] Get SingleSpace Failure'
};
export function containsSuccess(type: string) {
    const isSuccess = type.toLowerCase().search('success');
    if(isSuccess > 0){
        return 'Success Notification';
    }
    return 'Failure Notification';
}