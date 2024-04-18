<template>
    <BaseLayout>
      <div class="rounded-3 my-5 py-2">
        <div class="rounded-3 m-3 p-2 bg-white">
          <h2 class="text-center">{{ messages.pages.newAssignmentPage.title }}</h2>

          <FormKit
          type="form"
          :actions="false"
          @submit="postAssignment"
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
              @click="addUserDialogVisible = false"
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
  

  
  export default {
    components: {
      BaseLayout,
      Toast,
      Button
    },
    data() {
      return {

      };
    },
    computed: {
      ...mapState(languageStore, ["messages"]),
    },
    methods: {
      
    },
    mounted(){

    },
  };
  </script>
  
  <style scoped>

  </style>
  