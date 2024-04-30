<template>
  <div class="card h-100">
    <div class="card-header">
      <h3 class="text-center">{{ assignment.task_name }}</h3>
    </div>
    <div class="card-body m-auto text-center">
      <img
        :src="getImageSrc(assignment.course_image)"
        class="img-fluid rounded"
      />
      <RouterLink :to="{name: 'assignment', params: { id: assignment.id } }" class="btn text-white mt-3 buttons">{{
        messages.components.BaseAssignmentCard.buttonTitle
      }}</RouterLink>
    </div>
    <div class="card-footer">
      <h6 class="text-center m-0">
        <b
          >{{ messages.components.BaseAssignmentCard.deadline }}:
          {{ formatDate(assignment.deadline) }}</b
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
    assignment: Object,
  },
  methods: {
    formatDate(dateStr) {
      const parts = dateStr.split(/[\s.:]/);
      const date = new Date(parts[0], parts[1] - 1, parts[2], parts[3], parts[4], parts[5]);
      
      return new Intl.DateTimeFormat('hu-HU', {
        year: 'numeric',
        month: '2-digit',
        day: '2-digit'
      }).format(date);
    },
    getImageSrc(base64Data) {
      return `data:image/jpeg;base64,${base64Data}`;
    },
  }
};
</script>
