// import the redux parts needed to start our store
import { createStore, applyMiddleware, compose } from "redux";
import thunk from "redux-thunk";
// import the already combined reducers for redux to use
import rootReducer from "./redux";
import { initialize, addTranslation } from "react-localize-redux";
import translations from "./translations/global";
import { languagesSelector } from "./redux/selectors/language";
// create our redux store using our reducers and our middleware, and export it for use in index.js
const composeEnhancers =
  typeof window === "object" && window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__
    ? window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__({
        // Specify extension’s options like name, actionsBlacklist, actionsCreators, serialize...
      })
    : compose;
const enhancer = composeEnhancers(
  applyMiddleware(thunk)
  // other store enhancers if any
);
const store = createStore(rootReducer, enhancer);
let init = true;
store.subscribe(s => {
  if (s != undefined) {
    const languages = languagesSelector(s).map(l => l.id);
    store.dispatch(initialize(languages));
    store.dispatch(addTranslation(translations));
    init = false;
  }
  window.store = store;
});
export default store;
