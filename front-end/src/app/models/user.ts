// For what you receive after login/auth
export interface User {
    displayName: string;
    token: string;
    image: string;
    username: string;
    ingredientsCount: number;
    recipesCount: number;
    dayPlansCount: number;
}

// For login form
export interface LoginFormValues {
    email: string;
    password: string;
}

// For registration form
export interface RegisterFormValues {
    displayName: string;
    email: string;
    password: string;
    username: string;
}

export interface UserFormValues {
    email: string;
    password: string;
    displayName?: string;
    userName?: string;
}