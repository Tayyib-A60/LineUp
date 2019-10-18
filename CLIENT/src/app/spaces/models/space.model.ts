export interface Space {
    id: number;
    name: string;
    location: string;
    description: string;
    size: number;
    type: Type;
    price: string;
    userId: number;
    amenities: Amenity[];
}

export interface Amenity {
    name: string;
    price: number;
}

export interface Type {
    type: string;
    id: number;
}