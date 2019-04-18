import React, { Component } from "react";
import "./App.css";
import { Provider } from "react-redux";
import store from "./store";
import { renderToStaticMarkup } from "react-dom/server";
import { withLocalize } from "react-localize-redux";
import globalTranslations from "./translations/global.json";
import StartProcessPage from "./components/StartProcess/startProcessPage";
import { BrowserRouter, Switch, Route, Redirect } from "react-router-dom";
import Modal from "react-modal";
import Routes from "./components/routes";

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
  componentDidMount() {
    Modal.setAppElement("#App");
  }
  render() {
    return (
      <BrowserRouter>
        <div className="App" id="App">
          <Provider store={store}>
            <Switch>
              <Route path="/process" component={StartProcessPage} exact />
              <Route component={Routes} />
            </Switch>
          </Provider>
        </div>
      </BrowserRouter>
    );
  }
}

export default withLocalize(App);
