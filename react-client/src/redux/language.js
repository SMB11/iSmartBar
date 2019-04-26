const initialState = {
  languages: [
    { id: "en", name: "English" },
    { id: "it", name: "Italiano" },
    { id: "de", name: "Deutsch" },
    { id: "fr", name: "Français" },
    { id: "ru", name: "Русский" },
    { id: "es", name: "Español" },
    { id: "pt", name: "Português" },
    { id: "zh", name: "中文" },
    { id: "ja", name: "日本人" }
  ]
};

export default (state = initialState, action) => {
  switch (action.type) {
    default:
      return { ...state };
  }
};
