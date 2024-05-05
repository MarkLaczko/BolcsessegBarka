import { defineStore } from "pinia";
import hu from "@locales/hu.mjs";
import en from "@locales/en.mjs";

export const languageStore = defineStore("languageStore", {
  state() {
    return {
      language: "HU"
    };
  },
  actions: {
    switchLanguage() {
      this.language = this.language === "HU" ? "EN" : "HU";
    },
  },
  getters: {
    messages() {
      if (this.language == "HU") {
        return hu;
      }
      return en;
    }
  },
  persist: true,
});
