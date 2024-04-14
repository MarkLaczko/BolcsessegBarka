<template>
  <div class="card mb-3 w-75 m-auto">
    <div class="row g-0">
      <div class="col-md-3">
        <img
          :src="'http://localhost:5173/src/assets/images/' + image"
          :alt="image"
          class="img-fluid rounded-start object-fit-cover w-100 h-100"
          alt="kep"
        />
      </div>
      <div class="col-md-6">
        <div class="card-body">
          <h6 class="card-title">
            {{ messages.components.BaseLearningMaterialCard.courseTitle }}:
            {{ course }}
          </h6>
          <h3 class="card-text">
            <b>{{ learningMaterial }}</b>
          </h3>
          <span class="badge">{{ releaseData }}</span>
        </div>
      </div>
      <div class="col-md-3 d-flex justify-content-center align-items-center">
        <a href="" class="btn text-white my-2 buttons">{{
          messages.components.BaseLearningMaterialCard.viewButton
        }}</a>
      </div>
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
    course: String,
    learningMaterial: String,
    image: String,
    releaseData: String,
  },
};
</script>
