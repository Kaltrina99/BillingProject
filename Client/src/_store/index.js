import { configureStore } from '@reduxjs/toolkit';

import { authReducer } from './auth.slice';
import { usersReducer } from './users.slice';
import {productsReducer} from './product.slice'

export * from './auth.slice';
export * from './users.slice';
export * from './product.slice'

export const store = configureStore({
    reducer: {
        auth: authReducer,
        users: usersReducer,
        products: productsReducer,
    },
});