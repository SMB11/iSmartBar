export const FETCH_LANGUAGES = "language/FETCH_LANGUAGES";
export const FETCH_LANGUAGES_END = "language/FETCH_LANGUAGES_END";
export const FOCUS_LANGUAGE = "language/FOCUS_LANGUAGE";
// export const CART_UPDATED = 'cart/CART_UPDATED';

const initialState = {
  languages: [],
  languagesFetching: false,
  focusedLanguage: null
};

export default (state = initialState, action) => {
  switch (action.type) {
    case FETCH_LANGUAGES:
      return { ...state, languagesFetching: true };
    case FETCH_LANGUAGES_END:
      return { ...state, languagesFetching: false, languages: action.payload };
    default:
      return { ...state };
  }
};

export const fetchLanguagesStart = () => ({
  type: FETCH_LANGUAGES
});

export const fetchLanguagesEnd = languages => ({
  type: FETCH_LANGUAGES_END,
  payload: languages
});

function fetchSecretSauce() {
  return fetch("http://localhost:3000/");
}

export function GetLanguages() {
  return function(dispatch) {
    dispatch(fetchLanguagesStart());
    return fetchSecretSauce().then(data =>
      dispatch(
        fetchLanguagesEnd([
          { id: "en", name: "English" },
          { id: "it", name: "Italian" }
        ])
      )
    );
  };
}
