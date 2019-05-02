import React, { Component } from "react";
import "./assets/scss/app.scss";
import { Provider } from "react-redux";
import store from "./store";
import { renderToStaticMarkup } from "react-dom/server";
import { withLocalize } from "react-localize-redux";
import globalTranslations from "./translations/global.json";
import StartProcessPage from "./components/StartProcess/startProcessPage";
import { BrowserRouter, Switch, Route, Redirect } from "react-router-dom";
import Modal from "react-modal";
import Routes from "./components/routes";
import Footer from "./components/Reusable/footer";
import ScrollToTop from "./components/Reusable/ScrollToTop";

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
  componentWillUpdate() {}
  render() {
    return (
      <BrowserRouter>
        <ScrollToTop>
          <div className="container" id="App">
            <Provider store={store}>
              <Switch>
                <Route path="/process" component={StartProcessPage} exact />
                <Route component={Routes} />
              </Switch>
            </Provider>
          </div>
        </ScrollToTop>
      </BrowserRouter>
    );
  }
}

export default withLocalize(App);
