import {makeAutoObservable, reaction, runInAction} from "mobx";
import {Ingredient} from "../models/ingredient.ts";
import agent from "../api/agent.ts";
import {Pagination, PagingParams} from "../models/pagination.ts";


export default class IngredientStore {

    ingredientRegistry = new Map<string, Ingredient>();
    selectedIngredient: Ingredient | null = null;
    editMode = false;
    loading = false;
    loadingInitial = false;
    pagination: Pagination | null = null;
    pagingParams = new PagingParams();
    predicate = new Map().set('all', true);


    constructor() {
        makeAutoObservable(this)

        reaction(
            () => this.predicate.keys(),
            () => {
                this.pagingParams = new PagingParams();
                this.ingredientRegistry.clear();
                this.loadIngredients();
            }
        )
    }

    private setIngredient = (ingredient: Ingredient) => {
        this.ingredientRegistry.set(ingredient.id, ingredient);
    }

    setLoadingInitial = (state: boolean) => {
        this.loadingInitial = state;
    }

    loadIngredients = async () => {
        this.setLoadingInitial(true);
        try {
            const result = await agent.Ingredients.list(this.axiosParams);
            //const result = await agent.Ingredients.list(this.axiosParams);
            console.log('Raw Ingredients API Result:', result);

            if (result.data && result.data.length > 0) {
                result.data.forEach(ingredient => {
                    console.group(`Ingredient: ${ingredient.name}`);
                    console.log('Ingredient Object Type:', typeof ingredient);
                    console.log('Ingredient Object Keys:', Object.keys(ingredient));
                    console.log('Full Ingredient Object:', JSON.stringify(ingredient, null, 2));
                    console.log('Calories:', ingredient.calories);

                    // Try to access nutrition properties explicitly
                    console.log('Nutrition Access Test:', {
                        calories: ingredient.calories,
                        protein: ingredient.protein,
                        carbs: ingredient.carbs,
                        fat: ingredient.fat
                    });
                    console.groupEnd();

                    this.setIngredient(ingredient);
                });
            } else {
                console.log('No ingredients returned');
            }

            //this.setPagination(result.pagination)
            this.setLoadingInitial(false);

        } catch (error) {
            console.error('Error loading ingredients:', error);
            runInAction(() => this.setLoadingInitial(false));
        }
    };

    get axiosParams() {
        const params = new URLSearchParams();
        params.append('pageNumber', this.pagingParams.pageNumber.toString());
        params.append('pageSize', this.pagingParams.pageSize.toString());
        this.predicate.forEach((value, key) => {
            params.append(key, value);
        })
        return params;
    }
}