<template>
  <BaseLayout>

    <BaseSpinner :loading="loading"/>
    <BaseToast/>

    <div class="rounded-3 my-5 py-2" v-if="!loading">
      <div class="rounded-3 m-3 p-2 bg-white" v-for="assignment in assignments" :key="assignment.id">
        <p >
          {{ messages.pages.assignmentPage.task_name }} {{ assignment.task_name }} <br />
          {{ messages.pages.assignmentPage.courseName }} {{ assignment.course.name }} <br />
          {{ messages.pages.assignmentPage.deadline }} {{ assignment.deadline }}
          <p v-if="assignment.comment">{{ messages.pages.assignmentPage.comment }} {{ assignment.comment }}</p>
        </p>
        <div v-if="isDeadlineReached" class=" py-4 text-center alert alert-danger">
          {{ messages.pages.assignmentPage.deadlineExpired }}
        </div>
        <div class="d-flex justify-content-end">
          <RouterLink v-if="isDeadlineReached" class="btn btn-outline-danger px-5" :to="{ name: 'course', params: { id: assignment.course.id }, query: {groupName: this.$route.query.groupName}}">{{messages.pages.assignmentPage.returnButton}}</RouterLink>
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
            :label="
              messages.pages.newAssignmentPage.teacher_task
            "
            multiple="false"
            validation=""
            :classes="{
              input: {
                'mb-1': true,
                'form-control': true,
              },
              noFiles: {
                'd-none': true
              },
            }"
          />
          <div class="d-flex justify-content-end mt-3 mb-3">
            <RouterLink class="btn btn-outline-danger px-5" :to="{ name: 'course', params: { id: assignment.course.id }, query: {groupName: this.$route.query.groupName}}">{{messages.pages.assignmentPage.returnButton}}</RouterLink>
            <FormKit
              type="submit"
              :label="
                messages.pages.newAssignmentPage.saveButton
              "
              id="addAssignmentButton"
              :classes="{
                input: {
                  btn: true,
                  'btn-success': true,
                  'w-auto': true,
                  'px-5' : true,
                  'ms-1' : true
                },
              }"
            />
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
import { http } from "@utils/http";
import { userStore } from "@stores/UserStore";
import BaseSpinner from "@components/BaseSpinner.vue"
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
    BaseToast
  },
  data() {
    return {
      files: [],
      totalSize: 0,
      totalSizePercent: 0,
      assignments: [],
      isDeadlineReached: false,
      deadlineDate: null,
      loading: true
    };
  },
  computed: {
    ...mapState(languageStore, ["messages"]),
  },
  methods: {
    async getAssignments(){
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
          formData.append('assignment_id', this.$route.params.id)
          formData.append('student_task_name', data.student_task[0].name);
          formData.append('student_task', data.student_task[0].file);
          
        const user = userStore();
        await http.post(`/studentAssignments`, formData, {
          headers: {
            Authorization: `Bearer ${user.token}`,
            'Content-Type': 'application/x-www-form-urlencoded' 
          },
        });


        let toast = {
          severity: "success",
          detail:
            this.messages.pages.newAssignmentPage.toastMessages.successfullyCreatedAssignment,
          life: 3000,
        };
        if (!this.isDarkMode) {
          toast.styleClass = "bg-success text-white";
        }

        this.$toast.add(toast);

      } catch (error) {
        let toast = {
          severity: "error",
          detail:
            this.messages.pages.newAssignmentPage.toastMessages.failedToCreateAssignment,
          life: 3000,
        };
        if (!this.isDarkMode) {
          toast.styleClass = "bg-danger text-white";
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
  async mounted(){
    await this.getAssignments();
    this.checkDeadline();
    this.loading = false;
  },
  watch: {
  assignments: {
    handler: function(newAssignments) {
      this.checkDeadline();
    },
    deep: true
  }
},
};
</script>

<style scoped>
.text-custom {
  font-size: 5rem;
}

.border {
  border-width: 2px;
}
</style>
