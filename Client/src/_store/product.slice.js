import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import { fetchWrapper } from '_helpers';

// Create slice

const name = 'product';
const initialState = createInitialState();
const extraActions = createExtraActions();
const extraReducers = createExtraReducers();
const slice = createSlice({ name, initialState, extraReducers });

// Exports

export const productActions = { ...slice.actions, ...extraActions };
export const productsReducer = slice.reducer;

// Implementation

function createInitialState() {
    return {
        items: [],
        loading: false,
        error: null,
    };
}

function createExtraActions() {
    const baseUrl = `${process.env.REACT_APP_API_URL}/api/product`;

    return {
        getAll: createAsyncThunk(`${name}/getAll`, async () => {
            const response = await fetchWrapper.get(baseUrl);
            return response.data;
        }),
    };
}

function createExtraReducers() {
    const { getAll } = extraActions;

    return {
        [getAll.pending]: (state) => {
            state.loading = true;
            state.error = null;
        },
        [getAll.fulfilled]: (state, action) => {
            state.items = action.payload;
            state.loading = false;
        },
        [getAll.rejected]: (state, action) => {
            state.loading = false;
            state.error = action.error.message;
        },
    };
}
