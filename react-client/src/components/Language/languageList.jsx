import React, { Component } from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import Language from "./language";
import {
  languagesSelector,
  languagesFetchingSelector
} from "../../redux/selectors/language";
import { GetLanguages } from "../../redux/language";
import {
  withLocalize,
  Translate,
  setActiveLanguage
} from "react-localize-redux";

class LanguageList extends Component {
  state = {
    anyChecked: false
  };
  componentDidMount() {
    this.props.GetLanguages();
  }
  handleChecked(language) {
    this.props.languages.forEach(lang => (lang.checked = false));
    language.checked = true;
    this.setState({ ...this.state, anyChecked: true });
    setActiveLanguage(language.id);
  }
  render() {
    const { languages, loading } = this.props;
    const isButtonDisabled = loading || !this.state.anyChecked;
    if (loading) return <h2>Loading...</h2>;
    else
      return (
        <div>
          {languages.map((value, index) => (
            <Language
              onChecked={() => this.handleChecked.bind(this)(value)}
              key={index}
              language={value}
            />
          ))}
          <button disabled={isButtonDisabled}>
            <Translate id="nextStep" />
          </button>
        </div>
      );
  }
}

const mapStateToProps = state => ({
  languages: languagesSelector(state),
  loading: languagesFetchingSelector(state)
});

const mapDispatchToProps = dispatch =>
  bindActionCreators(
    {
      GetLanguages
    },
    dispatch
  );

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withLocalize(LanguageList));
