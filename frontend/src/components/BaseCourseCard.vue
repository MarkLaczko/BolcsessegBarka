<template>
  <div class="card">
    <div class="card-img-wrapper">
      <img
        :src="'data:image/png;base64,' + image"
        class="card-img-top img-fluid object-fit-cover"
        :alt="image"
      />
    </div>
    <div class="card-body text-center">
      <h5 class="card-title">{{ title }} ({{ groupName }})</h5>
      <RouterLink
        class="btn text-white mt-3 buttons"
        :to="{
          name: 'course',
          params: { id: courseId },
          query: { groupName: groupName },
        }"
        >{{ messages.components.BaseCourseCard.viewButton }}</RouterLink
      >
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
    courseId: Number,
    groupName: String,
  },
};
</script>

<style scoped>
.card {
  display: inline-block;
  width: 100%;
  overflow: hidden;
}

.card-img-wrapper {
  position: relative;
  width: 100%;
  padding-top: 56.25%;
}

.card-img-wrapper img {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  object-fit: cover;
}
</style>
