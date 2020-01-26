export interface Space {
    id: number;
    name: string;
    selectedPricingOption: PricingOption;
    minimumTerm: string;
    location: Location;
    description: string;
    size: number;
    type: Type;
    pricePH: Pricing;
    pricePD: Pricing;
    pricePW: Pricing;
    userId: number;
    amenities: Amenity[];
    photos: any[]
}
export interface Location {
    id: number;
    name: string;
    long: string;
    lat: string;
}
export interface Pricing {
    id: number;
    price: string;
    discount: number;
}

export interface Amenity {
    name: string;
    price: number;
}

export interface PricingOption {
    id: number;
    option: string;
    description: string;
}

export interface Type {
    type: string;
    id: number;
}

export interface LocationDetails {
    locationName: string;
    lat: string;
    long: string;
}