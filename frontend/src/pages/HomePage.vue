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
      <h1 class="text-center my-3">
        {{ messages.pages.homePage.title }} {{ currentUserData.name }}!
      </h1>
      <h1 class="text-center my-3">Üdvözöllek {{ currentUserData.name }}!</h1>

      <div class="rounded-3 pt-3">
        <h2 class="text-center mb-3">
          <b>{{ messages.pages.homePage.taskTitle }}</b>
        </h2>
        <div class="row px-5 pb-5 g-4">
          <TransitionGroup :name="`slide-${animationDirection}`">
            <div
              class="col-12 col-lg-4 col-md-6"
              v-for="assignment in paginatedAssignments"
              :key="assignment.id"
            >
              <BaseAssignmentCard
                :title="assignment.title"
                :image="assignment.image"
                :deadline="assignment.deadline"
              />
            </div>
          </TransitionGroup>
        </div>
        <Paginator
          :total-pages="totalPages"
          :current-page="currentPage"
          @page-changed="onPageChanged"
        />
      </div>
      <div class="rounded-3 pt-3 pb-2 mb-2">
        <h2 class="text-center mb-3">
          <b>{{ messages.pages.homePage.materialsTitle }}</b>
        </h2>
        <BaseLearningMaterialCard
          :course="'Szoftverfejlesztő'"
          :learningMaterial="'Tesztelés'"
          :image="'asd.jpeg'"
          :releaseData="'2024.06.05'"
        />
        <BaseLearningMaterialCard
          :course="'Frontend'"
          :learningMaterial="'Vue'"
          :image="'asd.jpeg'"
          :releaseData="'2024.06.10'"
        />
        <BaseLearningMaterialCard
          :course="'Backend'"
          :learningMaterial="'Rest API'"
          :image="'asd.jpeg'"
          :releaseData="'2024.06.12'"
        />
      </div>
    </div>
  </BaseLayout>
</template>

<script>
import BaseLayout from "@layouts/BaseLayout.vue";
import BaseAssignmentCard from "@components/BaseAssignmentCard.vue";
import BaseLearningMaterialCard from "@components/BaseLearningMaterialCard.vue";
import { userStore } from "@stores/UserStore.mjs";
import { mapActions, mapState } from "pinia";
import Paginator from "@components/BasePaginator.vue";
import { languageStore } from "@stores/LanguageStore.mjs";

export default {
  data() {
    return {
      assignments: [
        {
          id: 1,
          title: "Feladat 1",
          image: "asd.jpeg",
          deadline: "2024.09.09",
        },
        {
          id: 2,
          title: "Feladat 2",
          image: "asd.jpeg",
          deadline: "2024.09.13",
        },
        {
          id: 3,
          title: "Feladat 3",
          image: "asd.jpeg",
          deadline: "2024.09.15",
        },
        {
          id: 4,
          title: "Feladat 4",
          image: "asd.jpeg",
          deadline: "2024.09.09",
        },
        {
          id: 5,
          title: "Feladat 5",
          image: "asd.jpeg",
          deadline: "2024.09.13",
        },
        {
          id: 6,
          title: "Feladat 6",
          image: "asd.jpeg",
          deadline: "2024.09.15",
        },
        {
          id: 7,
          title: "Feladat 7",
          image: "asd.jpeg",
          deadline: "2024.09.09",
        },
        {
          id: 8,
          title: "Feladat 8",
          image: "asd.jpeg",
          deadline: "2024.09.13",
        },
        {
          id: 9,
          title: "Feladat 9",
          image: "asd.jpeg",
          deadline: "2024.09.15",
        },
        {
          id: 10,
          title: "Feladat 10",
          image: "asd.jpeg",
          deadline: "2024.09.09",
        },
        {
          id: 11,
          title: "Feladat 11",
          image: "asd.jpeg",
          deadline: "2024.09.13",
        },
        {
          id: 12,
          title: "Feladat 12",
          image: "asd.jpeg",
          deadline: "2024.09.15",
        },
        {
          id: 13,
          title: "Feladat 13",
          image: "asd.jpeg",
          deadline: "2024.09.09",
        },
        {
          id: 14,
          title: "Feladat 14",
          image: "asd.jpeg",
          deadline: "2024.09.13",
        },
        {
          id: 15,
          title: "Feladat 15",
          image: "asd.jpeg",
          deadline: "2024.09.15",
        },
        {
          id: 16,
          title: "Feladat 16",
          image: "asd.jpeg",
          deadline: "2024.09.09",
        },
        {
          id: 17,
          title: "Feladat 17",
          image: "asd.jpeg",
          deadline: "2024.09.13",
        },
        {
          id: 18,
          title: "Feladat 18",
          image: "asd.jpeg",
          deadline: "2024.09.13",
        },
      ],
      currentPage: 1,
      pageSize: 3,
      animationDirection: "right",
      loading: true,
    };
  },
  components: {
    BaseLayout,
    BaseAssignmentCard,
    BaseLearningMaterialCard,
    Paginator,
  },
  methods: {
    ...mapActions(userStore, ["getUser"]),

    onPageChanged(page) {
      this.currentPage = page;
    },
  },
  computed: {
    ...mapState(userStore, ["currentUserData"]),
    ...mapState(languageStore, ["messages"]),

    paginatedAssignments() {
      const start = (this.currentPage - 1) * this.pageSize;
      const end = start + this.pageSize;
      return this.assignments.slice(start, end);
    },

    totalPages() {
      return Math.ceil(this.assignments.length / this.pageSize);
    },
  },
  watch: {
    currentPage(newPage, oldPage) {
      this.animationDirection = newPage > oldPage ? "right" : "left";
    },
  },
  async mounted() {
    await this.getUser();
    this.loading = false;
  },
};
</script>

<style scoped>
.row {
  position: relative;
  overflow: hidden;
}

.slide-right-enter-active,
.slide-right-leave-active {
  transition: transform 0.5s ease-in-out, opacity 0.5s ease;
}
.slide-right-enter-from,
.slide-right-leave-to {
  transform: translateX(100%);
}
.slide-right-leave-to {
  position: absolute;
  transform: translateX(-100%) scale(0.8) rotate(-10deg);
  opacity: 0;
}

.slide-left-enter-active,
.slide-left-leave-active {
  transition: transform 0.5s ease-in-out, opacity 0.5s ease;
}
.slide-left-enter-from,
.slide-left-leave-to {
  transform: translateX(-100%);
}

@media screen and (min-width: 1172px) {
  .slide-left-leave-to {
    position: absolute;
    left: 800px;
    transform: translateX(100%) scale(0.8) rotate(10deg);
    opacity: 0;
  }
}

@media (min-width: 992px) and (max-width: 1171px) {
  .slide-left-leave-to {
    position: absolute;
    left: 600px;
    transform: translateX(100%) scale(0.8) rotate(10deg);
    opacity: 0;
  }
}

@media screen and (max-width: 991px) {
  .slide-left-leave-to {
    position: absolute;
    left: 400px;
    transform: translateX(100%) scale(0.8) rotate(10deg);
    opacity: 0;
  }
}

@media screen and (max-width: 768px) {
  .slide-left-leave-to {
    position: absolute;
    left: -200px;
    transform: translateX(100%) scale(0.8) rotate(10deg);
    opacity: 0;
  }
}
</style>
