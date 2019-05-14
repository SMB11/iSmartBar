import React, { Component } from "react";
import "../../assets/scss/addToCart.scss";
class AddToCart extends Component {
  state = {};
  btnRef;
  onClick() {
    const btnRef = this.btnRef;
    btnRef.classList.add("added");
    setTimeout(() => {
      btnRef.classList.remove("added");
    }, 500);
    this.props.onClick();
  }
  render() {
    return (
      <div className="button-content addToCart">
        <button
          ref={ref => (this.btnRef = ref)}
          onClick={this.onClick.bind(this)}
          className="btn"
        >
          <img src="http://beta.ismartbar.it/images/add-to-cart.svg" alt="" />
          <span>Add to Cart</span>
          <svg
            className="cart-ok"
            xmlns="http://www.w3.org/2000/svg"
            width="24"
            height="24"
            viewBox="0 0 24 24"
          >
            <path
              style={{ fill: "#288cff " }}
              d="M20.285 2l-11.285 11.567-5.286-5.011-3.714 3.716 9 8.728 15-15.285z"
            />
          </svg>
        </button>
      </div>
    );
  }
}

export default AddToCart;
