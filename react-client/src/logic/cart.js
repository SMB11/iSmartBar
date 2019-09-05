const limits = [20, 5];
const getDisitibution = (cart, product, quantity) => {
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
    if (quantitySum <= limit) inside = limit - quantitySum;
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
  const distribution = getDisitibution(oldCart, product, quantity);

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
    const index = oldCart[oldCart.length - 1].findIndex(p => p.id === productOutside.id);
    if (index >= 0) oldCart[oldCart.length - 1][index].quantity += productOutside.quantity;
    else oldCart[oldCart.length - 1].push(productOutside);
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
  // get carts object
  const carts = getCarts();
  // get the cart section to update
  let oldCart = carts[carts.length - 1];
  // get the cart item index to remove
  const index = oldCart[size - 1].findIndex(p => p.id === id);
  // if it exists
  if (index >= 0) {
    // get the cart item
    let product = oldCart[size - 1][index];
    // if we working with inside items
    if (size !== oldCart.length) {
      // and the product quantity is bigger than one just subtract one and update 
      if (product.quantity > 1) {
        product.quantity -= 1;
        oldCart[size - 1][index] = product;
      } else {
        // else remove the product
        oldCart[size - 1].splice(index, 1);
      }
      // determine if we need to bring an item from outside to inside
      const toMoveIndex = oldCart[oldCart.length].findIndex(p => p.size === size);
      // if we have empty space 
      if (toMoveIndex >= 0) {
        // get the movable item
        const toMove = oldCart[oldCart.length][toMoveIndex];
        
        // if quantity is bigger than one subtract one in the outside section
        if (toMove.quantity > 1) {
          oldCart[oldCart.length][toMoveIndex] = {
            ...toMove,
            quantity: toMove.quantity - 1
          };
        } else {
          // else remove from the outside
          oldCart[oldCart.length].splice(0, 1);
        }
        //update cart and send notification
        carts[carts.length - 1] = oldCart;
        window.localStorage.setItem("carts", JSON.stringify(carts));
        // call addToCart to reuse code
        const res = addToCart(toMove, 1);
        res.popupMessage = "One item was moved to inside of MiniBar.";
        return res;
      }
    } else {
      // else if working with outside items remove the product
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
      JSON.stringify([[[], [], []]])
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
