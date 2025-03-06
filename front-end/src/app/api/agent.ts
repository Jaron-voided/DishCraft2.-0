import axios, {AxiosError, AxiosResponse} from "axios";
import {Ingredient} from "../models/ingredient.ts";
import {toast} from "react-toastify";
import {router} from "../router/Routes.tsx";
import {store} from "../stores/store.ts";
import {User, UserFormValues} from "../models/user.ts";
import {PaginatedResult} from "../models/pagination.ts";


const sleep = (delay: number) => {
    return new Promise(resolve => {
        setTimeout(resolve, delay);
    })
}

axios.defaults.baseURL = 'http://localhost:5000/api';

const responseBody = <T> (response: AxiosResponse<T>) => response.data;

axios.interceptors.request.use(config => {
    const token = store.commonStore.token;
    if (token && config.headers) config.headers.Authorization = `Bearer ${token}`;
    return config;
})

axios.interceptors.response.use(async response => {
    /*    try {
            await sleep(1000);
            return response;
        } catch (error) {
            console.log(error);
            return Promise.reject(error);
        }*/
    await sleep(1000);
    const pagination = response.headers['pagination'];
    if (pagination) {
        response.data = new PaginatedResult(response.data, JSON.parse(pagination));
        return response as AxiosResponse<PaginatedResult<unknown>>
    }
    return response;
}, (error: AxiosError) => {
    const {data, status, config} = error.response as AxiosResponse;
    switch (status) {
        case 400:
            if (config.method === 'get' && Object.prototype.hasOwnProperty.call(data.errors, 'id')) {
                router.navigate('/not-found')
            }
            if (data.errors) {
                const modalStateErrors = [];
                for (const key in data.errors) {
                    if (data.errors[key]) {
                        modalStateErrors.push(data.errors[key]);
                    }
                }
                throw modalStateErrors.flat();
            } else {
                toast.error(data);
            }
            break;
        case 401:
            toast.error('Unauthorized');
            break;
        case 403:
            toast.error('Forbidden');
            break;
        case 404:
            router.navigate('/not-found')
            break;
        case 500:
            //store.commonStore.setServerError(data);
            router.navigate('/server-error');
            break;
    }
    return Promise.reject(error);
})


const requests = {
    get: <T> (url: string) => axios.get<T>(url).then(responseBody),
    post: <T>(url: string, body: {}) => axios.post<T>(url, body).then(responseBody),
    put: <T>(url: string, body: {}) => axios.put<T>(url, body).then(responseBody),
    del: <T>(url: string) => axios.delete<T>(url).then(responseBody),
}

const Ingredients = {
    list: (params: URLSearchParams) => {
        return axios.get<PaginatedResult<Ingredient[]>>('/ingredients', {params})
            .then(response => {
                console.log('Raw API Response:', response);
                return responseBody(response);
            });
    },
    details: (id: string) => {
        return requests.get<Ingredient>(`/ingredients/${id}`)
            .then(response => {
                console.log('Raw Details Response:', response);
                return response;
            });
    },
    create: (ingredient: Ingredient) => axios.post<void>('/ingredients', ingredient),
    update: (ingredient: Ingredient) => axios.put<void>(`/ingredients/${ingredient.id}`, ingredient),
    delete: (id: string) => axios.delete<void>(`/ingredients/${id}`)
}

const Account = {
    current: () => requests.get<User>('/account'),
    login: (user: UserFormValues) => requests.post<User>('/account/login', user),
    register: (user: UserFormValues) => requests.post<User>('/account/register', user),
}

const agent = {
    Ingredients,
    Account
}

export default agent;