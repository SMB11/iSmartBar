const canAddToCart = (cart, product, quantity) => {
  //   let cartWithSize = cart.filter(p => product.size === p.size).length;
  const sectionItems = cart[product.size - 1];
  const reducer = (qty = 0, pr) => {
    return qty + pr.quantity;
  };
  let quantitySum = sectionItems.reduce(reducer, 0);
  if (quantitySum + quantity > 5) {
    const outside = quantitySum + quantity - 5;
    let inside = 0;
    if (quantitySum <= 5) inside = 5 - quantitySum;
    return { outside, inside };
  } else {
    return { outside: 0, inside: quantity };
  }
};
const addToCart = (product, quantity) => {
  let carts = getCarts();
  if (quantity === 0) return carts;
  product.quantity = quantity;
  let oldCart = carts[carts.length - 1];
  const distribution = canAddToCart(oldCart, product, quantity);

  const productInside = { ...product, quantity: distribution.inside };
  const productOutside = { ...product, quantity: distribution.outside };
  if (productInside.quantity > 0) {
    const index = oldCart[product.size - 1].findIndex(
      p => p.id === productInside.id
    );
    if (index >= 0)
      oldCart[product.size - 1][index].quantity += productInside.quantity;
    else oldCart[product.size - 1].push(productInside);
  }
  if (productOutside.quantity > 0) {
    const index = oldCart[5].findIndex(p => p.id === productOutside.id);
    if (index >= 0) oldCart[5][index].quantity += productOutside.quantity;
    else oldCart[5].push(productOutside);
  }
  carts[carts.length - 1] = oldCart;
  window.localStorage.setItem("carts", JSON.stringify(carts));
  return carts;
};
const getCarts = () => {
  if (!window.localStorage.getItem("carts")) {
    window.localStorage.setItem(
      "carts",
      JSON.stringify([[[], [], [], [], [], []]])
    );
  }
  let carts = JSON.parse(window.localStorage.getItem("carts"));
  return carts;
};
export default {
  addToCart,
  getCarts
};
