export interface Ingredient {
    id: string; // Using string for UUID
    name: string;
    category: string;

    measuredIn: string;
    weightUnit?: string;
    volumeUnit?: string;

    pricePerPackage: number;
    quantity: number;
    pricePerMeasurement: number;

    calories: number;
    carbs: number;
    fat: number;
    protein: number;

    appUserId: string;
}