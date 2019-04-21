const initialState = {
  languages: [
    { id: "en", name: "English" },
    { id: "it", name: "Italian" },
    { id: "ge", name: "German" },
    { id: "fr", name: "French" },
    { id: "ch", name: "Chinese" },
    { id: "mn", name: "Mandarin" },
    { id: "ru", name: "Russsian" },
    { id: "am", name: "Armenian" }
  ]
};

export default (state = initialState, action) => {
  switch (action.type) {
    default:
      return { ...state };
  }
};
