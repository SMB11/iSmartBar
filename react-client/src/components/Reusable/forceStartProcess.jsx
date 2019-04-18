import { locationStepStorageKey } from "../StartProcess/chooseLocation";
import { languageStepStorageKey } from "../StartProcess/chooseLanguage";
import { miniBarStepStorageKey } from "../StartProcess/chooseMiniBar";

import React from "react";
import { Redirect } from "react-router-dom";

export default function ForceStartProcess() {
  if (
    !JSON.parse(window.sessionStorage.getItem(languageStepStorageKey)) ||
    !JSON.parse(window.sessionStorage.getItem(locationStepStorageKey)) ||
    !JSON.parse(window.sessionStorage.getItem(miniBarStepStorageKey))
  )
    return <Redirect to="/process" />;

  return "";
}
