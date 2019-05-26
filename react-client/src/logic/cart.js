const limits = [20, 5];
const outsideID = limits.length;
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
    const index = oldCart[outsideID].findIndex(p => p.id === productOutside.id);
    if (index >= 0)
      oldCart[outsideID][index].quantity += productOutside.quantity;
    else oldCart[outsideID].push(productOutside);
  }
  carts[carts.length - 1] = oldCart;
  window.localStorage.setItem("carts", JSON.stringify(carts));
  return {
    carts,
    popupMessage: `${quantity} items were added to your MiniBar.`
  };
};

// const verifyCarts = carts => {
//   const outside = carts[outsideID];

//   for (let i = 0; i < limits.length - 1 && outside.length !== 0; i++) {
//     const reducer = (qty = 0, pr) => {
//       return qty + pr.quantity;
//     };
//     const quantity = carts[i].reduce(reducer, 0);
//     if (quantity < limits[i]) {
//       const moveCount = limits[i] - quantity;
//       const toMove = [];
//       const canMove;
//       let quantityLeft = moveCount;
//       while((canMove = outside.find(p => p.size === i+1)) != undefined) {
//         if(canMove.quantity >= quantityLeft){
//           canMove.quantity = quantityLeft;
//           toMove.push(canMove);
//           break;
//         }
//         else{
//           toMove.push(canMove);
//           quantityLeft -= canMove.quantity;
//         }
//       }
//       canMove.quantity < quantity
//     }
//   }
// };

const changeProductQuantity = (id, size, quantity) => {
  const carts = getCarts();
  let oldCart = carts[carts.length - 1];
  const index = oldCart[size - 1].findIndex(p => p.id === id);
  let product = oldCart[size - 1][index];
  if (product) {
    const delta = quantity - product.quantity;
    product.quantity = quantity;
    oldCart[size - 1][index] = product;
    if (size !== outsideID + 1) {
      if (delta === -1) {
        const toMoveIndex = oldCart[outsideID].findIndex(p => p.size === size);
        if (toMoveIndex >= 0) {
          const toMove = oldCart[outsideID][toMoveIndex];

          if (toMove.quantity > 1) {
            oldCart[outsideID][toMoveIndex] = {
              ...toMove,
              quantity: toMove.quantity - 1
            };
          } else {
            oldCart[outsideID].splice(0, 1);
          }

          carts[carts.length - 1] = oldCart;
          window.localStorage.setItem("carts", JSON.stringify(carts));
          const res = addToCart(toMove, 1);
          res.popupMessage = "One item was moved to inside of MiniBar.";
          return res;
        }
      } else {
        product.quantity = Math.min(quantity, limits[size - 1]);
      }
    }
    carts[carts.length - 1] = oldCart;
    window.localStorage.setItem("carts", JSON.stringify(carts));
  }
  // carts = verifyCarts(carts);
  return { carts };
};

const removeProduct = (id, size, forceAll = false) => {
  const carts = getCarts();
  let oldCart = carts[carts.length - 1];
  const index = oldCart[size - 1].findIndex(p => p.id === id);
  if (index >= 0) {
    let product = oldCart[size - 1][index];
    if (size !== 3) {
      if (product.quantity > 1 && !forceAll) {
        product.quantity -= 1;
        oldCart[size - 1][index] = product;
      } else {
        oldCart[size - 1].splice(index, 1);
      }
      // get one from other

      const toMoveIndex = oldCart[outsideID].findIndex(p => p.size === size);
      if (toMoveIndex >= 0) {
        const toMove = oldCart[outsideID][toMoveIndex];

        if (toMove.quantity > 1) {
          oldCart[outsideID][toMoveIndex] = {
            ...toMove,
            quantity: toMove.quantity - 1
          };
        } else {
          oldCart[outsideID].splice(0, 1);
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
    window.localStorage.setItem("carts", JSON.stringify([[[], [], []]]));
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
