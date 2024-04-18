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

    <div class="rounded-3 my-5 pt-2" v-if="!loading">
      <h1 class="text-center mb-4">{{ messages.pages.coursesPage.title }}</h1>

      <h2 v-if="courses.length === 0" class="text-center">
        Nincsenek kurzusaid!
      </h2>

      <div class="row px-5 pb-5 g-4">
        <div
          class="col-12 col-lg-4 col-md-6"
          v-for="course in courses"
          :key="course.id"
        >
          <BaseCourseCard
            :title="`${course.name}`"
            :image="`${course.image}`"
          />
        </div>
      </div>
    </div>
  </BaseLayout>
</template>

<script>
import BaseLayout from "@layouts/BaseLayout.vue";
import BaseCourseCard from "@components/BaseCourseCard.vue";
import { mapState } from "pinia";
import { languageStore } from "@stores/LanguageStore.mjs";
import { http } from "@utils/http.mjs";
import { userStore } from "@stores/UserStore.mjs";

export default {
  data() {
    return {
      courses: [],
      loading: true,
    };
  },
  methods: {
    async getCourses() {
      const response = await http.get("/groups", {
        headers: {
          Authorization: `Bearer ${userStore().token}`,
        },
      });

      response.data.data.forEach((group) => {
        group.users.forEach((user) => {
          if (user.id === userStore().currentUserData.id) {
            this.courses.push(...group.courses);
          }
        });
      });
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
  },
};
</script>
