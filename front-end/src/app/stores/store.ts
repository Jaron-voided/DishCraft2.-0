import IngredientStore from "./ingredientStore.ts";
import {createContext, useContext} from "react";
import CommonStore from "./commonStore.ts";
import ModalStore from "./modalStore.ts";
import UserStore from "./userStore.ts";

interface Store {
    commonStore: CommonStore;
    userStore: UserStore;
    ingredientStore: IngredientStore;
    modalStore: ModalStore;
}

export const store: Store = {
    commonStore: new CommonStore(),
    userStore: new UserStore(),
    ingredientStore: new IngredientStore(),
    modalStore: new ModalStore(),
}

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext);
}