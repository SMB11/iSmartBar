import React, { Component } from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import Language from "../Language/language";
import { languagesSelector } from "../../redux/selectors/language";
import { withLocalize, Translate } from "react-localize-redux";
export const languageStepStorageKey = "ChooseLanguageState";

class ChooseLanguage extends Component {
  state = {
    selected: {}
  };
  componentDidMount() {
    const state = JSON.parse(
      window.sessionStorage.getItem(languageStepStorageKey)
    );
    console.log(state);
    if (state) this.setState(state);
  }
  componentDidUpdate(prevProps, prevState) {
    const language = this.state.selected;
    const prevLanguage = prevState.selected;
    if (
      (prevLanguage && language && language.id !== prevLanguage.id) ||
      (!prevLanguage && language)
    ) {
      this.props.setActiveLanguage(language.id);
    }
  }
  handleChecked(language) {
    this.setState({ ...this.state, selected: language });
  }
  handleFinish() {
    window.sessionStorage.setItem(
      languageStepStorageKey,
      JSON.stringify(this.state)
    );
    this.props.onFinished();
  }
  render() {
    const { languages } = this.props;
    const isButtonDisabled = !this.state.selected;
    return (
      <div>
        {languages.map((value, index) => (
          <Language
            onChecked={() => this.handleChecked.bind(this)(value)}
            key={index}
            checked={this.state.selected && this.state.selected.id === value.id}
            language={value}
          />
        ))}
        <button
          disabled={isButtonDisabled}
          onClick={this.handleFinish.bind(this)}
        >
          <Translate id="select" />
        </button>
      </div>
    );
  }
}

const mapStateToProps = state => ({
  languages: languagesSelector(state)
});

export default connect(mapStateToProps)(withLocalize(ChooseLanguage));
