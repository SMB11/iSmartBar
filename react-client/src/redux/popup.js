export const SET_POPUPMESSSAGE = "SET_POPUPMESSSAGE ";
export const SETPOPUPSHOWN = "SETPOPUPSHOWN ";

const initialState = {
  popupMessage: null,
  popupShown: true
};

export default (state = initialState, action) => {
  switch (action.type) {
    case SET_POPUPMESSSAGE:
      return { ...state, popupMessage: action.payload, popupShown: false };
    case SETPOPUPSHOWN:
      return { ...state, popupShown: true };
    default:
      return { ...state };
  }
};

export const SetPopupMessage = message => {
  return {
    type: SET_POPUPMESSSAGE,
    payload: message
  };
};

export const SetPopupShown = () => {
  return {
    type: SETPOPUPSHOWN
  };
};
