<template>
  <div class="card mb-3 w-75 m-auto">
    <div class="row g-0">
      <div class="col-md-3" style="max-height: 200px;">
        <img
          :src="getImageSrc(learningMaterial.course_image)"
          class="img-fluid w-100 object-fit-cover h-100 responsive-rounded" style="max-height: 200px;"
          alt="kep"
        />
      </div>
      <div class="col-md-6">
        <div class="card-body">
          <h6 class="card-title">
            {{ messages.components.BaseLearningMaterialCard.courseTitle }}:
            {{ learningMaterial.course_name }}
          </h6>
          <h3 class="card-text">
            <b>{{ learningMaterial.title }}</b>
          </h3>
          <span class="badge" v-if="learningMaterial.updated_at != null">{{
            formatDate(learningMaterial.updated_at)
          }}</span>
        </div>
      </div>
      <div class="col-md-3 d-flex justify-content-center align-items-center">
        <button @click.prevent="openDialog(learningMaterial)" class="btn text-white my-2 buttons">{{
          messages.components.BaseLearningMaterialCard.viewButton
        }}</button>
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
  methods: {
    formatDate(dateStr) {
      const date = new Date(dateStr);
      const year = date.getFullYear();
      const month = (date.getMonth() + 1).toString().padStart(2, "0");
      const day = date.getDate().toString().padStart(2, "0");
      return `${year}.${month}.${day}`;
    },
    getImageSrc(base64Data) {
      return `data:image/jpeg;base64,${base64Data}`;
    },
    openDialog(material) {
      this.$emit('open-material-dialog', material);
    }
  },
  props: {
    learningMaterial: Object,
  },
};
</script>

<style scoped>
@media (max-width: 767px) {
  .responsive-rounded {
    border-radius: 0.25rem 0.25rem 0 0;
  }
}

@media (min-width: 768px) {
  .responsive-rounded {
    border-radius: 0.25rem 0 0 0.25rem;
  }
}
</style>
