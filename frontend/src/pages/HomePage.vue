<template>
  <BaseLayout>
    <BaseSpinner :loading="loading" />

    <BaseDialog
      v-if="viewNoteVisible"
      :visible="viewNoteVisible"
      :width="'50rem'"
    >
      <div class="container rounded-3 pt-1 pb-3">
        <h1 class="text-center my-3">
          {{ messages.pages.homePage.viewNoteDialog.title }}
        </h1>
        <div class="card">
          <Editor
            :readonly="true"            
            :model-value="selectedMaterial.text"
            editorStyle="height: 320px"
          >
            <template v-slot:toolbar>
              <span class="text-center">
                 {{ messages.pages.homePage.viewNoteDialog.noteName }} {{ selectedMaterial.title }}
              </span>
          </template>
          </Editor>
        </div>

        <div class="d-flex justify-content-center align-items-center mt-3">
          <button
              type="button"
              class="btn"
              :class="{
                'btn-outline-danger': isDarkMode,
                'btn-danger': !isDarkMode,
              }"
              @click="(viewNoteVisible = false)"
            >
              {{ messages.pages.homePage.viewNoteDialog.cancelButton }}
            </button>
        </div>
      </div>
    </BaseDialog>

    <div v-if="!loading">
      <h1 class="text-center my-3">
        {{ messages.pages.homePage.title }} {{ currentUserData.name }}!
      </h1>

      <div class="rounded-3 pt-3">
        <h2 class="text-center mb-3">
          <b>{{ messages.pages.homePage.taskTitle }}</b>
        </h2>

        <h3 v-if="assignments.length == 0" class="text-center pb-3">{{ messages.pages.homePage.noAssignmentsTitle }}</h3>
        
        <div v-if="assignments.length > 0">
          <div class="row px-5 pb-5 g-4">
            <TransitionGroup :name="`slide-${animationDirection}`">
              <div class="col-12 col-lg-4 col-md-6" v-for="assignment in paginatedAssignments" :key="assignment.id">
                <BaseAssignmentCard  :assignment="assignment"/>
              </div>
            </TransitionGroup>
          </div>
          <Paginator :total-pages="totalPages" :current-page="currentPage" @page-changed="onPageChanged" />
        </div>
      </div>
      <div class="rounded-3 pt-3 pb-2 mb-2">
        <h2 class="text-center mb-3">
          <b>{{ messages.pages.homePage.materialsTitle }}</b>
        </h2>
        <div v-if="learningMaterials.length > 0" v-for="learningMaterial in learningMaterials" :key="learningMaterial.id">
          <BaseLearningMaterialCard :learningMaterial="learningMaterial" @open-material-dialog="handleMaterialDialog"/>
        </div>
        <h3 v-if="learningMaterials.length == 0" class="text-center my-3">{{ messages.pages.homePage.noMaterialsTitle }}</h3>

      </div>
    </div>
  </BaseLayout>
</template>

<script>
import BaseLayout from "@layouts/BaseLayout.vue";
import BaseAssignmentCard from "@components/BaseAssignmentCard.vue";
import Editor from "primevue/editor";
import InputText from "primevue/inputtext";
import BaseSpinner from "@components/BaseSpinner.vue";
import BaseDialog from "@components/BaseDialog.vue"
import BaseLearningMaterialCard from "@components/BaseLearningMaterialCard.vue";
import { userStore } from "@stores/UserStore.mjs";
import { mapActions, mapState } from "pinia";
import Paginator from "@components/BasePaginator.vue";
import { languageStore } from "@stores/LanguageStore.mjs";
import { noteStore } from "@stores/NoteStore.mjs";
import { themeStore } from "@stores/ThemeStore.mjs";
import { http } from "@utils/http.mjs";

export default {
  data() {
    return {
      assignments: [],
      learningMaterials: [],
      viewNoteVisible: false,
      selectedMaterial: null,
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
    BaseSpinner,
    BaseDialog,
    InputText,
    Editor
  },
  methods: {
    ...mapActions(userStore, ["getUser"]),
    ...mapActions(noteStore, ["getTeacherNotes"]),

    onPageChanged(page) {
      this.currentPage = page;
    },

    handleMaterialDialog(material) {
      this.selectedMaterial = material;
      this.viewNoteVisible = true;
    },

    async getAssignments() {
      const response = await http.get("/getCurrentAssignments", {
        headers: {
          Authorization: `Bearer ${userStore().token}`,
        },
      });

      this.assignments = response.data.data;
    }
  },
  computed: {
    ...mapState(userStore, ["currentUserData"]),
    ...mapState(languageStore, ["messages"]),
    ...mapState(themeStore, ["isDarkMode"]),

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
    await this.getAssignments();
    this.learningMaterials = await this.getTeacherNotes();
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
