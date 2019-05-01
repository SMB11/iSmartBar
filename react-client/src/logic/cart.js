const canAddToCart = (cart, product, quantity) => {
  //   let cartWithSize = cart.filter(p => product.size === p.size).length;
  const reducer = (qty, pr) => {
    if (pr.size === product.size) {
      return qty + pr.quantity;
    }
    return qty;
  };
  let size = cart.reduce(reducer);
  if (size + quantity > 5) {
    return false;
  }
  return true;
};
const addToCart = (product, quantity) => {
  if (!window.sessionStorage.getItem("carts")) {
    window.sessionStorage.setItem("carts", JSON.stringify([[[], [], [], [], [], []]]));
  }
  let carts = JSON.parse(window.sessionStorage.getItem("carts"));
  let oldCart = carts[carts.length - 1];

  if (canAddToCart(oldCart, product, quantity)) {
    product.quantity = quantity;
    oldCart.push(product);
    carts[carts.length - 1] = oldCart;
    window.sessionStorage.setItem("carts", JSON.stringify(carts));
    return carts;
  } else {
    alert("cant add to cart");
  }
};
const getCarts = () => {
  const carts = JSON.parse(window.sessionStorage.getItem("carts"));
  if (!carts) return [[]];

  return carts;
};
export default {
  addToCart,
  getCarts
};
