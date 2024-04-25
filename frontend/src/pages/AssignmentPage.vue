<template>
  <BaseLayout>

    <BaseSpinner :loading="loading"/>

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
        <div class="card" v-if="!isDeadlineReached">
          <Toast
            :pt="{
              root: {
                class: 'w-25',
              },
              detail: {
                class: 'text-center',
              },
              icon: {
                class: 'mt-1 ms-1',
              },
              text: {
                class: 'w-75 mx-auto',
              },
              container: {
                class: ' rounded w-75',
              },
              buttonContainer: {
                class: 'w-25 d-flex justify-content-center ms-auto',
              },
              button: {
                class: 'btn mb-2',
              },
              transition: {
                name: 'slide-fade',
              },
            }"
          />
          <!-- <FileUpload
            :pt="{
              input: {
                class: 'd-none',
              },
            }"
            url="api/assignments"
            @upload="onTemplatedUpload($event)"
            :multiple="true"
            accept=".zip, .rar, .jpg, .jpeg, .png"
            :maxFileSize="100000000"
            @select="onSelectedFiles"
          >
            <template
              #header="{ chooseCallback, uploadCallback, clearCallback, files }"
            >
              <div
                class="d-flex justify-content-between align-items-center flex-wrap flex-grow-1 mt-2"
              >
                <div class="d-flex gap-2 ms-2">
                  <button
                    @click="chooseCallback()"
                    class="btn btn-outline-primary rounded-circle"
                  >
                    <i class="pi pi-images"></i>
                  </button>
                  <button
                    @click="uploadEvent(uploadCallback)"
                    class="btn btn-outline-success rounded-circle"
                    :disabled="!files || files.length === 0 || totalSize >= 100"
                  >
                    <i class="pi pi-cloud-upload"></i>
                  </button>
                  <button
                    @click="clearCallback()"
                    class="btn btn-outline-danger rounded-circle"
                    :disabled="!files || files.length === 0"
                  >
                    <i class="pi pi-times"></i>
                  </button>
                </div>
                <div class="progress mb-3 w-25 me-3">
                  <div
                    class="progress-bar bg-success"
                    role="progressbar"
                    :style="{ width: totalSizePercent + '%' }"
                    :aria-valuenow="totalSizePercent"
                    aria-valuemin="0"
                    aria-valuemax="100"
                  >
                    <span class="text-white">{{ totalSize }}MB / 100MB</span>
                  </div>
                </div>
              </div>
            </template>
            <template
              #content="{
                files,
                uploadedFiles,
                removeUploadedFileCallback,
                removeFileCallback,
              }"
            >
              <div v-if="files.length > 0">
                <h5 class="mt-3 ms-2">Függőben</h5>
                <div
                  class="row row-cols-1 row-cols-sm-2 row-cols-md-3 p-0 sm:p-5 gap-3 mx-2 mb-2"
                >
                  <div
                    v-for="(file, index) of files"
                    :key="file.name + file.type + file.size"
                    class="col card m-0 px-4 py-3 border-1 surface-border align-items-center gap-3"
                  >
                    <div>
                      <img
                        role="presentation"
                        :alt="file.name"
                        :src="file.objectURL"
                        class="img-fluid"
                      />
                    </div>
                    <span class="font-weight-bold">{{ file.name }}</span>
                    <div>{{ formatSize(file.size) }}</div>
                    <span class="badge bg-warning">Függőben</span>
                    <button
                      @click="
                        onRemoveTemplatingFile(file, removeFileCallback, index)
                      "
                      class="btn btn-outline-danger rounded-circle"
                    >
                      <i class="pi pi-times"></i>
                    </button>
                  </div>
                </div>
              </div>

              <div v-if="uploadedFiles.length > 0">
                <h5 class="mt-3 ms-2">Feltöltve</h5>
                <div
                  class="row row-cols-1 row-cols-sm-2 row-cols-md-3 p-0 sm:p-5 gap-3 mx-2 mb-2"
                >
                  <div
                    v-for="(file, index) of uploadedFiles"
                    :key="file.name + file.type + file.size"
                    class="col card m-0 px-4 py-3 border-1 surface-border align-items-center gap-3"
                  >
                    <div>
                      <img
                        role="presentation"
                        :alt="file.name"
                        :src="file.objectURL"
                        class="img-fluid"
                      />
                    </div>
                    <span class="font-weight-bold">{{ file.name }}</span>
                    <div>{{ formatSize(file.size) }}</div>
                    <span class="badge bg-success">Feltöltve</span>
                    <button
                      @click="removeUploadedFileCallback(index)"
                      class="btn btn-outline-danger rounded-circle"
                    >
                      <i class="pi pi-times"></i>
                    </button>
                  </div>
                </div>
              </div>
            </template>
            <template #empty>
              <div
                class="d-flex align-items-center justify-content-center flex-column mb-2"
              >
                <i
                  class="pi pi-cloud-upload border rounded-circle p-3 text-custom"
                />
                <p class="mt-4 mb-0">
                  {{ messages.pages.assignmentPage.dragFileText }}
                </p>
              </div>
            </template>
          </FileUpload> -->
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
            multiple="true"
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
        </FormKit>
        </div>
        <div class="d-flex justify-content-end">
          <RouterLink class="btn btn-outline-danger mt-3 px-5" :to="{ name: 'course', params: { id: assignment.course.id } ,}">{{messages.pages.assignmentPage.returnButton}}</RouterLink>
          <button type="submit" class="btn btn-success mt-3 px-5 ms-1"  @click="postNewStudentAssignment">
            {{ messages.pages.newAssignmentPage.saveButton }}
          </button>
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

export default {
  components: {
    BaseLayout,
    FileUpload,
    Toolbar,
    Button,
    Toast,
    ProgressBar,
    BaseSpinner
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
        console.log(data);
      }
    },
    // onRemoveTemplatingFile(file, removeFileCallback, index) {
    //   removeFileCallback(index);
    //   this.totalSize -= parseFloat(this.formatSize(file.size));
    //   this.totalSizePercent = this.totalSize;
    // },
    // onClearTemplatingUpload(clear) {
    //   clear();
    //   this.totalSize = 0;
    //   this.totalSizePercent = 0;
    // },
    // onSelectedFiles(event) {
    //   this.files = event.files;
    //   this.files.forEach((file) => {
    //     this.totalSize += parseFloat(this.formatSize(file.size));
    //   });
    // },
    // uploadEvent(callback) {
    //   this.totalSizePercent = this.totalSize;
    //   callback();
    // },
    // onTemplatedUpload() {
    //   this.$toast.add({
    //     severity: "info",
    //     summary: "Success",
    //     detail: "File Uploaded",
    //     life: 3000,
    //   });
    // },
    // formatSize(bytes) {
    //   const k = 1024;
    //   const sizes = this.$primevue.config.locale.fileSizeTypes;

    //   if (bytes === 0) {
    //     return `0 ${sizes[0]}`;
    //   }
    //   else if (bytes <= 1000000) {
    //     return `${(bytes/1000000).toFixed(3)} ${sizes[2]}`;
    //   }
    //   else {
    //     const i = Math.floor(Math.log(bytes) / Math.log(k));
    //     const formattedSize = parseFloat((bytes / Math.pow(k, i)).toFixed(0));

    //     return `${formattedSize} ${sizes[i]}`;
    //   }
    // },
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
