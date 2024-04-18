<template>
  <BaseLayout>
    <div class="d-flex justify-content-center" v-if="loading">
      <img
        src="../assets/images/logo.png"
        alt="logo"
        width="300px"
        class="rotating-pulsating"
      />
    </div>

    <div v-if="!loading">
      <h1 class="text-center my-3">{{ course.name }}</h1>
    </div>
  </BaseLayout>
</template>

<script>
import BaseLayout from "@layouts/BaseLayout.vue";
import { mapState, mapActions } from "pinia";
import { courseStore } from "@stores/CourseStore.mjs";
export default {
  data() {
    return {
      course: {},
      loading: true,
    };
  },
  components: {
    BaseLayout,
  },
  methods: {
    ...mapActions(courseStore, ["getCourse"]),
  },
  async mounted() {
    this.course = await this.getCourse(this.$route.params.id);
    console.log(this.course);
    this.loading = false;
  },
};
</script>
