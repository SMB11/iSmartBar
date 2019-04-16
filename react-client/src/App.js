import React, { Component } from "react";
import "./App.css";
import SelectLanguage from "./components/PageSelectLanguage/selectLanguage";
import { Provider } from "react-redux";
import store from "./store";
import { LocalizeProvider } from "react-localize-redux";
import { renderToStaticMarkup } from "react-dom/server";
import { withLocalize } from "react-localize-redux";
import globalTranslations from "./translations/global.json";

class App extends Component {
  constructor(props) {
    super(props);

    this.props.initialize({
      languages: [
        { name: "English", code: "en" },
        { name: "Italian", code: "it" }
      ],
      translation: globalTranslations,
      options: { renderToStaticMarkup, renderInnerHtml: true }
    });
  }
  render() {
    return (
      <div className="App">
        <LocalizeProvider>
          <Provider store={store}>
            <SelectLanguage />
          </Provider>
        </LocalizeProvider>
      </div>
    );
  }
}

export default withLocalize(App);
