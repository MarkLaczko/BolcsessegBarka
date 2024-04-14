import { defineStore } from "pinia";
import hu from "@locales/hu.mjs";
import en from "@locales/en.mjs";

export const languageStore = defineStore("languageStore", {
  state() {
    return {
      language: "HU",
      messages: hu,
    };
  },
  actions: {
    switchLanguage() {
      this.language = this.language === "HU" ? "EN" : "HU";
      this.messages = this.language === "HU" ? hu : en;
    },
  },
  persist: true,
});
