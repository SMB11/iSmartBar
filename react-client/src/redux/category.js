import api from "../api";

export const CATEGORYGET_START = "CATEGORYGET_START";
export const CATEGORYGET_END = "CATEGORYGET_END";

const initialState = {
  categories: [],
  categoryLoading: false
};

export default (state = initialState, action) => {
  switch (action.type) {
    case CATEGORYGET_START:
      return { ...state, categoryLoading: true };
    case CATEGORYGET_END:
      return { ...state, categoryLoading: false, categories: action.payload };
    default:
      return { ...state };
  }
};

export const CategoryGetStart = () => ({
  type: CATEGORYGET_START
});
export const CategoryGetEnd = categories => ({
  type: CATEGORYGET_END,
  payload: categories
});

export const CategoryGet = lang => {
  return function(dispatch) {
    dispatch(CategoryGetStart());
    return api.categories
      .getAll(lang)
      .then(response => {
        dispatch(CategoryGetEnd(response.data));
        console.log(response.data);
      })
      .catch(err => {
        console.log(err);
      });
  };
};
