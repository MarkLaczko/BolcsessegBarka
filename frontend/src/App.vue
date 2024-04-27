<script>
import { RouterView } from "vue-router";
import ScrollBackToTopButton from "@components/ScrollBackToTopButton.vue";
import { themeStore } from "@stores/ThemeStore.mjs";
import { mapState } from "pinia";
export default {
  components: {
    RouterView,
    ScrollBackToTopButton
  },
  computed: {
    ...mapState(themeStore, ["isDarkMode"]),
    isSignedOut() {
      return this.$route.path === "/register" || this.$route.path === "/login";
    },
  },
  mounted() {
    if (this.isDarkMode) {
    document.documentElement.setAttribute("data-theme", "dark");
  } else {
    document.documentElement.removeAttribute("data-theme");
  }
  }
};
</script>

<template>
  <div :class="{ backgroundColor: isSignedOut }">
    <RouterView />
    <ScrollBackToTopButton/>
  </div>
</template>
