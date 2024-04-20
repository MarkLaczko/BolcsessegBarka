<template>
    <BaseLayout>
      <div class="rounded-3 my-5 py-2">
        <div class="rounded-3 m-3 p-2 bg-white">
          <h2 class="text-center">{{ messages.pages.newAssignmentPage.title }}</h2>

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

          <FormKit
          type="form"
          :actions="false"
          @submit="postNewAssignment"
          :incomplete-message="
            messages.pages.userManagementPage.newUserDialog.validationMessages
              .matchAllValidationMessage
          "
        >
          <FormKit
            type="text"
            name="task_name"
            :label="messages.pages.newAssignmentPage.task_nameLabel"
            validation="required|length:0,255"
            :validation-messages="{
              required:
                messages.pages.newAssignmentPage
                  .validationMessages.task_nameRequired,
              length:
                messages.pages.newAssignmentPage
                  .validationMessages.task_nameLength,
            }"
            :classes="{
              input: {
                'mb-1': true,
                'form-control': true,
              },
            }"
          />
          <FormKit
            type="text"
            name="comment"
            :label="messages.pages.newAssignmentPage.comment"
            validation="length:0,255"
            :validation-messages="{
                length:
                    messages.pages.newAssignmentPage.validationMessages.commentLength,
            }"
            :classes="{
              input: {
                'mb-1': true,
                'form-control': true,
              },
            }"
          />
          <FormKit
            type="datetime-local"
            name="deadline"
            :label="
              messages.pages.newAssignmentPage.deadline
            "
            validation="required"
            :validation-messages="{
              required:
                messages.pages.newAssignmentPage.validationMessages.deadlineRequired,
            }"
            :classes="{
              input: {
                'mb-1': true,
                'form-control': true,
              },
            }"
          />
          <FormKit
            type="file"
            name="teacher_task"
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
          <div class="d-flex justify-content-end mt-2 mb-3">
            <Button
              type="button"
              :label="
                messages.pages.newAssignmentPage.cancelButton
              "
              class="btn btn-outline-danger mx-1 px-5"
              
            ></Button>
            <FormKit
              type="submit"
              :label="
                messages.pages.newAssignmentPage.saveButton
              "
              id="addUserButton"
              :classes="{
                input: {
                  btn: true,
                  'btn-success': true,
                  'w-auto': true,
                  'px-5' : true
                },
              }"
            />
          </div>
        </FormKit>
        </div>
      </div>
    </BaseLayout>
  </template>
  
  <script>
  import BaseLayout from "@layouts/BaseLayout.vue";
  import Toast from "primevue/toast";
  import { mapState } from "pinia";
  import { languageStore } from "@stores/LanguageStore.mjs";
  import Button from 'primevue/button';
  import { http } from "@utils/http";
  import { userStore } from "@stores/UserStore"; 
  import { themeStore } from "@stores/ThemeStore.mjs";
  

  
  export default {
    components: {
      BaseLayout,
      Toast,
      Button
    },
    computed: {
      ...mapState(languageStore, ["messages"]),
      ...mapState(themeStore, ["isDarkMode"]),
    },
    methods: {
      async postNewAssignment(data) {
      try {
        const formData = new FormData();
          formData.append('task_name', data.task_name);
          formData.append('comment', data.comment);
          formData.append('deadline', new Date(data.deadline).toISOString().slice(0, 19).replace('T', ' '));
          formData.append('teacher_task', data.teacher_task[0].file);
          formData.append('courseable_id', 1);
          formData.append('courseable_type', 'App\\Models\\Course');
          formData.append('teacher_task_name', data.teacher_task[0].name);
        

        const user = userStore();
        await http.post(`/assignments`, formData, {
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
    },
    mounted(){

    },
  };
  </script>
  
  <style scoped>

  </style>
  