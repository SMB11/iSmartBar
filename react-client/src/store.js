// import the redux parts needed to start our store
import { createStore, applyMiddleware, compose } from "redux";
import thunk from "redux-thunk";
// import the already combined reducers for redux to use
import rootReducer from "./redux";
import { initialize, addTranslation } from "react-localize-redux";
import translations from "./translations/global";
// create our redux store using our reducers and our middleware, and export it for use in index.js
const store = createStore(rootReducer, applyMiddleware(thunk));
store.subscribe(_ => {
  console.log(store.getState());
});
const languages = ["en", "it"];
store.dispatch(initialize(languages));
store.dispatch(addTranslation(translations));
export default store;
