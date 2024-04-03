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
      var icon = document.getElementById("icon");

      if (this.isDarkMode) {
        icon.classList.remove("fa-moon");
        icon.classList.add("fa-sun");

        document.documentElement.setAttribute("data-theme", "dark");
      } else {
        icon.classList.add("fa-moon");
        icon.classList.remove("fa-sun");

        document.documentElement.removeAttribute("data-theme");
      }
    },
  },
});
