import React, { Component } from "react";
import "../../assets/scss/mybar.scss";
import MyCartItem from "./myCartItem";
import { sectionCartSelector } from "../../redux/selectors/cart";
import { connect } from "react-redux";
import { read } from "fs";
class Section extends Component {
  render() {
    const prod = this.props.products;
    return (
      <div className="section">
        <h3 className="title">{this.props.sectionTitle}</h3>
        <div className="section-body">
          {!prod || prod.length === 0
            ? "Empty"
            : prod.map((p, index) => {
                return <MyCartItem key={index} product={p} />;
                const res = [];
                for (let i = 0; i < p.quantity; i++) {
                  res.push(<MyCartItem key={i} product={p} />);
                }
                return res;
              })}
        </div>
      </div>
    );
  }
}

const mapStateToProps = (state, props) => {
  return {
    products: sectionCartSelector(state, props.size)
  };
};

export default connect(mapStateToProps)(Section);
