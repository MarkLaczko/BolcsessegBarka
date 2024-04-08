import { defineStore } from "pinia";

export const themeStore = defineStore("themeStore", {
  state() {
    return {
      isDarkMode: false,
    };
  },
  actions: {
    toggleTheme() {
      this.isDarkMode = !this.isDarkMode;

      if (this.isDarkMode) {
        document.documentElement.setAttribute("data-theme", "dark");
      } else {
        document.documentElement.removeAttribute("data-theme");
      }
    },
  },
  persist: true,
});
