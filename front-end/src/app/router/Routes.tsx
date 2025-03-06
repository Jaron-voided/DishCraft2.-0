import {createBrowserRouter, RouteObject} from "react-router-dom";
import HomePage from "../../features/home/HomePage.tsx";
import UserProfile from "../../features/users/UserProfile.tsx";
import LoginForm from "../../features/users/LoginForm.tsx";
import AppLayout from "../layout/AppLayout.tsx";

export const routes: RouteObject[] = [
    {
        path: '/',
        element: <AppLayout />,
        children: [

            /*HomePage*/
            {path: '', element: <HomePage />},

            /*User*/
            {path: 'login', element: <LoginForm />},
            {path: '/profile/:username', element: <UserProfile />},

            /*Ingredients*/
/*            {path: 'ingredients', element: <IngredientDashboard />},
            {path: 'createIngredient', element: <IngredientForm key='create'/>},
            {path: 'ingredients/manage/:id', element: <IngredientForm key='manage'/>},*/
/*            /!*Recipes*!/
            {path: 'recipes', element: <RecipeDashboard />},
            {path: 'recipes/:id', element: <RecipeDetails />},
            {path: 'createRecipe', element: <RecipeForm key='create'/>},
            {path: 'recipes/manage/:id', element: <RecipeForm key='manage'/>},

            /!*DayPlans*!/
            {path: 'dayPlan', element: <DayPlanDashboard />},
            {path: 'dayPlan/:id', element: <DayPlanDetails />},
            {path: 'createDayPlan', element: <DayPlanForm key='create'/>},
            {path: 'dayPlan/manage/:id', element: <DayPlanForm key='manage'/>},

            /!*Errors*!/
            {path: 'errors', element: <TestErrors />},
            {path: 'server-error', element: <ServerError />},
            {path: '*', element: <Navigate replace to='/not-found' />},*/
        ]
    }
]

export const router = createBrowserRouter(routes)