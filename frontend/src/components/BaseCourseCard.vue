<template>
  <div class="card">
    <img
      :src="'http://localhost:5173/src/assets/images/' + image"
      class="card-img-top"
      :alt="image"
    />
    <div class="card-body text-center">
      <h5 class="card-title">{{ title }}</h5>
      <a href="#" class="btn text-white mt-3 buttons">{{
        messages.components.BaseCourseCard.viewButton
      }}</a>
    </div>
  </div>
</template>

<script>
import { mapState } from "pinia";
import { languageStore } from "@stores/LanguageStore.mjs";
export default {
  computed: {
    ...mapState(languageStore, ["messages"]),
  },
  props: {
    title: String,
    image: String,
  },
};
</script>
