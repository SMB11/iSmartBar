import React, { Component } from "react";
import "./App.css";
import { Provider } from "react-redux";
import store from "./store";
import { LocalizeProvider } from "react-localize-redux";
import { renderToStaticMarkup } from "react-dom/server";
import { withLocalize } from "react-localize-redux";
import globalTranslations from "./translations/global.json";
import StartProcessPage from "./components/StartProcess/startProcessPage";
import { BrowserRouter } from "react-router-dom";
import Routes from "./Routes/routes";

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
      <BrowserRouter>
        <div className="App">
          <Provider store={store}>
            {/* <StartProcessPage /> */}
            <Routes />
          </Provider>
        </div>
      </BrowserRouter>
    );
  }
}

export default withLocalize(App);
