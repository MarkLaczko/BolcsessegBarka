<template>
  <BaseLayout>
    <BaseSpinner :loading="loading" />

    <div class="rounded-3 my-5 pt-2" v-if="!loading">
      <h1 class="text-center mb-4">{{ messages.pages.coursesPage.title }}</h1>

      <h2 v-if="courses.length === 0" class="text-center">
        {{ messages.pages.coursesPage.youDontHaveCoursesText }}
      </h2>

      <div class="row px-5 pb-5 g-4">
        <div class="col-12 col-lg-4 col-md-6" v-for="course in courses" :key="course.id">
          <BaseCourseCard :title="`${course.name}`" :image="`${course.image}`" :courseId="course.id"
            :groups="course.groups" />
        </div>
      </div>
    </div>
  </BaseLayout>
</template>

<script>
import BaseLayout from "@layouts/BaseLayout.vue";
import BaseSpinner from "@components/BaseSpinner.vue";
import BaseCourseCard from "@components/BaseCourseCard.vue";
import { mapState, mapActions } from "pinia";
import { languageStore } from "@stores/LanguageStore.mjs";
import { http } from "@utils/http.mjs";
import { userStore } from "@stores/UserStore.mjs";
import { groupStore } from "@stores/GroupStore.mjs";

export default {
  data() {
    return {
      courses: [],
      loading: true,
    };
  },
  methods: {
    ...mapActions(groupStore, ["getGroups"]),
    async getCourses() {
      const response = await http.get("/courses", {
        headers: {
          Authorization: `Bearer ${userStore().token}`,
        },
      });

      for (const course of response.data.data) {
        for (const group of course.groups) {
          if (group.users.find(x => x.id == userStore().currentUserData.id)) {
            this.courses.push(course);
            break;
          }
        }
      }
    },
  },
  computed: {
    ...mapState(languageStore, ["messages"]),
  },
  async mounted() {
    await this.getCourses();
    this.loading = false;
  },
  components: {
    BaseLayout,
    BaseCourseCard,
    BaseSpinner
  },
};
</script>
