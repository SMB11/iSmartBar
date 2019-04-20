import React, { Component } from "react";
import NavBar from "../Reusable/navBar";
import Product from "../Products/product";

class landingPage extends Component {
  state = {};
  render() {
    return (
      <div>
        <NavBar />
        <Product
          product={{
            id: 1,
            name: "vardanik",
            price: 1
          }}
        />
      </div>
    );
  }
}

export default landingPage;
