<script>
import { RouterView } from "vue-router";
import ScrollBackToTopButton from "@components/ScrollBackToTopButton.vue";
export default {
  components: {
    RouterView,
    ScrollBackToTopButton
  },
  computed: {
    isSignedOut() {
      return this.$route.path === "/register" || this.$route.path === "/login";
    },
  },
};
</script>

<template>
  <div :class="{ backgroundColor: isSignedOut }">
    <RouterView />
    <ScrollBackToTopButton/>
  </div>
</template>
