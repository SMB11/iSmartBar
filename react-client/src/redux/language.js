const initialState = {
  languages: [{ id: "en", name: "English" }, { id: "it", name: "Italian" }]
};

export default (state = initialState, action) => {
  switch (action.type) {
    default:
      return { ...state };
  }
};
