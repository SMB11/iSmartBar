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
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import {
  popupMessageSelector,
  popupShownSelector
} from "./redux/selectors/popup";
import { SetPopupShown } from "./redux/popup";
import { UpdateCart } from "./redux/cart";
import Cookies from 'universal-cookie';
import ReactNotification from "react-notifications-component";
import "react-notifications-component/dist/theme.css";
import api from "./api";

class PageWrapper extends Component {
  constructor(props) {
    super(props);
    this.addNotification = this.addNotification.bind(this);
    this.notificationDOMRef = React.createRef();
    const cookies = new Cookies();
    cookies.set('currency', 'USD');
    this.validateCart();
  }
  validateCart(){
    
    const carts = JSON.parse(localStorage.getItem("carts"));
    if(carts){
      api.cart.validate(carts).then(e => {
        if(e.data){
          this.props.UpdateCart(e.data);
        }
      });
    }

  }
  addNotification(message) {
    this.notificationDOMRef.current.addNotification({
      message: message,
      type: "info",
      insert: "top",
      container: "top-right",
      animationIn: ["animated", "fadeIn"],
      animationOut: ["animated", "fadeOut"],
      dismiss: { duration: 3000 },
      dismissable: { click: true }
    });
  }
  componentWillReceiveProps(nextProps) {
    if (!nextProps.popupShown) {
      this.addNotification(nextProps.popupMessage);
      this.props.SetPopupShown();
    }
  }
  render() {
    return (
      <React.Fragment>
        <ReactNotification ref={this.notificationDOMRef} />
        <Switch>
          <Route path="/process" component={StartProcessPage} exact />
          <Route component={Routes} />
        </Switch>
      </React.Fragment>
    );
  }
}

const mapStateToProps = state => {
  return {
    popupMessage: popupMessageSelector(state),
    popupShown: popupShownSelector(state)
  };
};
const mapDispatchToProps = dispatch =>
  bindActionCreators(
    {
      SetPopupShown,
      UpdateCart
    },
    dispatch
  );

const PageWrapperComponent = connect(
  mapStateToProps,
  mapDispatchToProps
)(withLocalize(PageWrapper));

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
              <PageWrapperComponent />
            </Provider>
          </div>
        </ScrollToTop>
      </BrowserRouter>
    );
  }
}

export default withLocalize(App);
