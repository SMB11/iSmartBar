const limits = [20, 5];
const outsideID = sizes.length + 1;
const canAddToCart = (cart, product, quantity) => {
  //   let cartWithSize = cart.filter(p => product.size === p.size).length;
  const sectionItems = cart[product.size - 1];
  const reducer = (qty = 0, pr) => {
    return qty + pr.quantity;
  };
  const limit = limits[product.size - 1];
  let quantitySum = sectionItems.reduce(reducer, 0);
  if (quantitySum + quantity > limit) {
    const outside = quantitySum + quantity - limit;
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
  return {
    carts,
    popupMessage: `${quantity} items were added to your MiniBar.`
  };
};

const changeProductQuantity = (id, size, quantity) => {
  const carts = getCarts();
  let oldCart = carts[carts.length - 1];
  const index = oldCart[size - 1].findIndex(p => p.id === id);
  let product = oldCart[size - 1][index];
  if (product) {
    product.quantity = quantity;
    oldCart[size - 1][index] = product;
    carts[carts.length - 1] = oldCart;
    window.localStorage.setItem("carts", JSON.stringify(carts));
  }
  return { carts };
};

const removeProduct = (id, size) => {
  const carts = getCarts();
  let oldCart = carts[carts.length - 1];
  const index = oldCart[size - 1].findIndex(p => p.id === id);

  if (index >= 0) {
    let product = oldCart[size - 1][index];
    if (size !== 6) {
      if (product.quantity > 1) {
        product.quantity -= 1;
        oldCart[size - 1][index] = product;
      } else {
        oldCart[size - 1].splice(index, 1);
      }
      // get one from other

      const toMoveIndex = oldCart[5].findIndex(p => p.size === size);
      if (toMoveIndex >= 0) {
        const toMove = oldCart[5][toMoveIndex];

        if (toMove.quantity > 1) {
          oldCart[5][toMoveIndex] = {
            ...toMove,
            quantity: toMove.quantity - 1
          };
        } else {
          oldCart[5].splice(0, 1);
        }

        carts[carts.length - 1] = oldCart;
        window.localStorage.setItem("carts", JSON.stringify(carts));
        const res = addToCart(toMove, 1);
        res.popupMessage = "One item was moved to inside of MiniBar.";
        return res;
      }
    } else {
      oldCart[size - 1].splice(index, 1);
    }
  }
  carts[carts.length - 1] = oldCart;
  window.localStorage.setItem("carts", JSON.stringify(carts));

  return { carts };
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
  getCarts,
  changeProductQuantity,
  removeProduct
};
