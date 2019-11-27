export interface Space {
    id: number;
    name: string;
    location: Location;
    description: string;
    size: number;
    type: Type;
    pricePH: Pricing;
    pricePD: Pricing;
    pricePW: Pricing;
    userId: number;
    amenities: Amenity[];
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

export interface Type {
    type: string;
    id: number;
}