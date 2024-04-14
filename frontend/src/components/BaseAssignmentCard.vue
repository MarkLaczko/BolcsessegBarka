<template>
  <div class="card">
    <div class="card-header">
      <h3 class="text-center">{{ title }}</h3>
    </div>
    <div class="card-body m-auto text-center">
      <img
        :src="'http://localhost:5173/src/assets/images/' + image"
        :alt="image"
        class="img-fluid rounded"
      />
      <a href="#" class="btn text-white mt-3 buttons">{{
        messages.components.BaseAssignmentCard.buttonTitle
      }}</a>
    </div>
    <div class="card-footer">
      <h6 class="text-center m-0">
        <b
          >{{ messages.components.BaseAssignmentCard.deadline }}:
          {{ deadline }}</b
        >
      </h6>
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
    deadline: String,
  },
};
</script>
