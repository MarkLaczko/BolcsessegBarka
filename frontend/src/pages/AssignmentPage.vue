<template>
  <BaseLayout>
    <BaseSpinner :loading="loading" />
    <BaseToast />

    <div class="rounded-3 my-5 py-2" v-if="!loading">
      <div class="rounded-3 m-3 p-2" :class="{'bg-white' : !isDarkMode}" v-for="assignment in assignments" :key="assignment.id">
        <p >
          <strong>{{ messages.pages.assignmentPage.task_name }}</strong> {{ assignment.task_name }} <br />
          <strong>{{ messages.pages.assignmentPage.courseName }}</strong> {{ assignment.course.name }} <br />
          <strong>{{ messages.pages.assignmentPage.deadline }}</strong> {{ assignment.deadline }} <br>
          <span v-if="assignment.comment"><strong>{{ messages.pages.assignmentPage.comment }}</strong> {{ assignment.comment }}</span> <br>
          <span v-if="assignment.teacher_task_name !== null"><strong>{{ messages.pages.assignmentPage.teacherTask }}</strong> {{ assignment.teacher_task_name }}</span>
        </p>
        <div
          v-if="isDeadlineReached"
          class="py-4 text-center alert alert-danger"
        >
          {{ messages.pages.assignmentPage.deadlineExpired }}
        </div>
        <div class="d-flex justify-content-end">
          <RouterLink
            v-if="isDeadlineReached"
            class="btn btn-danger text-white px-3"
            :to="{ name: 'course', params: { id: assignment.course.id } }"
            >{{ messages.pages.assignmentPage.returnButton }}</RouterLink
          >
        </div>
        <div v-if="!isDeadlineReached">
          <FormKit
            type="form"
            :actions="false"
            @submit="postNewStudentAssignment"
            :incomplete-message="
              messages.pages.userManagementPage.newUserDialog.validationMessages
                .matchAllValidationMessage
            "
          >
            <FormKit
              type="file"
              name="student_task"
              :label="messages.pages.newAssignmentPage.teacher_task"
              multiple="false"
              validation=""
              :classes="{
                input: {
                  'mb-1': true,
                  'form-control': true,
                },
                noFiles: {
                  'd-none': true,
                },
              }"
            />
            <div class="d-flex justify-content-between mt-3 mb-3">
              <div class="d-flex">
                <button
                  v-if="assignment.teacher_task_name !== null"
                  class="btn btn-success ms-2 text-white"
                  type="button"
                  @click="
                    downloadAssignment(
                      assignment.id,
                      assignment.teacher_task_name
                    )
                  "
                >
                  {{ messages.pages.assignmentPage.downloadTeacherAssignment }}
                </button>
                <div v-else></div>
              </div>
              <div class="d-flex justify-content-center align-items-center">
                <RouterLink
                  class="btn btn-danger text-white px-3 ms-2"
                  :to="{ name: 'course', params: { id: assignment.course.id } }"
                  >{{ messages.pages.assignmentPage.returnButton }}</RouterLink
                >
                <FormKit
                  type="submit"
                  :label="messages.pages.newAssignmentPage.saveButton"
                  id="addAssignmentButton"
                  :classes="{
                    input: {
                      btn: true,
                      'btn-success': true,
                      'text-white': true,
                      'w-auto': true,
                      'px-3': true,
                      'ms-2': true,
                    },
                  }"
                />
              </div>
            </div>
          </FormKit>
        </div>
      </div>
    </div>
  </BaseLayout>
</template>

<script>
import BaseLayout from "@layouts/BaseLayout.vue";
import FileUpload from "primevue/fileupload";
import Toolbar from "primevue/toolbar";
import Button from "primevue/button";
import Toast from "primevue/toast";
import ProgressBar from "primevue/progressbar";
import { mapState } from "pinia";
import { languageStore } from "@stores/LanguageStore.mjs";
import { themeStore } from "@stores/ThemeStore.mjs";
import { http } from "@utils/http";
import { userStore } from "@stores/UserStore";
import BaseSpinner from "@components/BaseSpinner.vue";
import BaseToast from "@components/BaseToast.vue";

export default {
  components: {
    BaseLayout,
    FileUpload,
    Toolbar,
    Button,
    Toast,
    ProgressBar,
    BaseSpinner,
    BaseToast,
  },
  data() {
    return {
      files: [],
      totalSize: 0,
      totalSizePercent: 0,
      assignments: [],
      isDeadlineReached: false,
      deadlineDate: null,
      loading: true,
    };
  },
  computed: {
    ...mapState(languageStore, ["messages"]),
    ...mapState(userStore, ["currentUserData"]),
    ...mapState(themeStore, ["isDarkMode"]),
  },
  methods: {
    async downloadAssignment(assignmentId) {
      const user = userStore();
      try {
        const response = await http.get(
          `/assignments/${assignmentId}/download`,
          {
            headers: {
              Authorization: `Bearer ${user.token}`,
            },
            responseType: "blob",
          }
        );
        const contentDisposition = response.headers["content-disposition"];
        let file = null;
        const filenameRegex = /filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/;
        let matches = filenameRegex.exec(contentDisposition);
        if (matches != null && matches[1]) {
          file = matches[1].replace(/['"]/g, "");
        }

        const blob = new Blob([response.data], {
          type: "application/octet-stream",
        });

        const downloadUrl = window.URL.createObjectURL(blob);

        const link = document.createElement("a");
        link.href = downloadUrl;
        link.setAttribute("download", file);
        document.body.appendChild(link);
        link.click();

        link.parentNode.removeChild(link);
      } catch (error) {
        console.error("Download error:", error);
      }
    },
    async getAssignments() {
      const user = userStore();
      const response = await http.get(`/assignments/${this.$route.params.id}`, {
        headers: {
          Authorization: `Bearer ${user.token}`,
        },
      });
      this.assignments.push(response.data.data);
      this.deadlineDate = response.data.data.deadline;
    },
    async postNewStudentAssignment(data) {
      try {
        const formData = new FormData();
        formData.append("assignment_id", this.$route.params.id);
        formData.append("student_task_name", data.student_task[0].name);
        formData.append("student_task", data.student_task[0].file);
        formData.append("user_id", this.currentUserData.id);

        const user = userStore();
        await http.post(`/studentAssignments`, formData, {
          headers: {
            Authorization: `Bearer ${user.token}`,
            "Content-Type": "application/x-www-form-urlencoded",
          },
        });

        let toast = {
          severity: "success",
          detail:
            this.messages.pages.newAssignmentPage.toastMessages
              .successfullyCreatedAssignment,
          life: 3000,
        };
        if (!this.isDarkMode) {
          toast.styleClass = "bg-success text-white";
        }
        else {
          toast.styleClass = "toast-success text-white";
        }

        this.$toast.add(toast);
      } catch (error) {
        let toast = {
          severity: "error",
          detail:
            this.messages.pages.newAssignmentPage.toastMessages
              .failedToCreateAssignment,
          life: 3000,
        };
        if (!this.isDarkMode) {
          toast.styleClass = "bg-danger text-white";
        }
        else {
          toast.styleClass = "toast-danger text-white";
        }
        this.$toast.add(toast);
      }
    },
    checkDeadline() {
      const currentDateTime = new Date();
      const deadlineDateTime = new Date(Date.parse(this.deadlineDate));

      if (currentDateTime >= deadlineDateTime) {
        this.isDeadlineReached = true;
      } else {
        this.isDeadlineReached = false;
      }
    },
  },
  async mounted() {
    await this.getAssignments();
    this.checkDeadline();
    this.loading = false;
  },
  watch: {
    assignments: {
      handler: function (newAssignments) {
        this.checkDeadline();
      },
      deep: true,
    },
  },
};
</script>
