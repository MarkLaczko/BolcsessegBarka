<template>
  <BaseLayout>
    <BaseSpinner :loading="loading" />
    <BaseToast/>

    <BaseDialog v-if="newTopicDialogVisible" :visible="newTopicDialogVisible"
      :header="messages.pages.coursePage.newTopicDialog.title" :width="'25rem'">
      <FormKit type="form" :actions="false" @submit="addTopic" :incomplete-message="messages.pages.coursePage.newTopicDialog.validationMessages
      .matchAllValidationMessage
      ">
        <FormKit type="text" name="name" :label="messages.pages.coursePage.newTopicDialog.nameLabel"
          validation="required|length:0,60" :validation-messages="{
      required:
        messages.pages.coursePage.newTopicDialog.validationMessages
          .nameRequired,
      length:
        messages.pages.coursePage.newTopicDialog.validationMessages
          .nameLength,
    }" :classes="{
      input: {
        'mb-1': true,
        'form-control': true,
      },
    }" />

        <FormKit type="text" name="order" :label="messages.pages.coursePage.newTopicDialog.orderLabel"
          validation="number" :validation-messages="{
      number:
        messages.pages.coursePage.newTopicDialog.validationMessages
          .orderNumber,
    }" :classes="{
      input: {
        'mb-1': true,
        'form-control': true,
      },
    }" />

        <div class="d-flex justify-content-end mt-2 mb-3">
          <Button type="button" class="btn btn-outline-danger mx-1" @click="newTopicDialogVisible = false">{{
      messages.pages.coursePage.newTopicDialog.cancelButton }}</Button>
          <FormKit type="submit" :label="messages.pages.coursePage.newTopicDialog.saveButton" :classes="{
      input: {
        btn: true,
        'btn-success': true,
        'w-auto': true,
      },
    }" />
        </div>
      </FormKit>
    </BaseDialog>

    <BaseDialog v-if="editTopicDialogVisible" :visible="editTopicDialogVisible"
      :header="messages.pages.coursePage.editTopicDialog.title" :width="'25rem'">
      <FormKit type="form" :actions="false" @submit="editTopic" :incomplete-message="messages.pages.coursePage.editTopicDialog.validationMessages
      .matchAllValidationMessage
      ">
        <FormKit type="text" name="name" :label="messages.pages.coursePage.editTopicDialog.nameLabel"
          validation="length:0,60" :validation-messages="{
      length:
        messages.pages.coursePage.editTopicDialog.validationMessages
          .nameLength,
    }" :classes="{
      input: {
        'mb-1': true,
        'form-control': true,
      },
    }" />

        <FormKit type="text" name="order" :label="messages.pages.coursePage.editTopicDialog.orderLabel"
          validation="number" :validation-messages="{
      number:
        messages.pages.coursePage.editTopicDialog.validationMessages
          .orderNumber,
    }" :classes="{
      input: {
        'mb-1': true,
        'form-control': true,
      },
    }" />

        <div class="d-flex justify-content-end mt-2 mb-3">
          <Button type="button" class="btn btn-outline-danger mx-1" @click="editTopicDialogVisible = false">{{
      messages.pages.coursePage.editTopicDialog.cancelButton }}</Button>
          <FormKit type="submit" :label="messages.pages.coursePage.editTopicDialog.saveButton" :classes="{
      input: {
        btn: true,
        'btn-success': true,
        'w-auto': true,
      },
    }" />
        </div>
      </FormKit>
    </BaseDialog>

    <BaseDialog v-if="groupTreatmentDialog" :visible="groupTreatmentDialog" :header="messages.pages.coursePage.groupTreatmentDialog.title"
      :width="'25rem'">

      <MultiSelect v-model="currentGroups" :options="groups" filter :maxSelectedLabels="3" optionLabel="name"
        :placeholder="messages.pages.coursePage.groupTreatmentDialog.multiSelectPlaceholder" display="chip" :pt="{
      list: {
        class: 'rounded-3 w-75 list-style-none p-2',
      },
      header: {
        class:
          'rounded-3 w-75 mb-2 d-flex justify-content-center align-items-center p-1',
      },
      closeButton: {
        class: 'btn mb-1',
      },
      filterIcon: {
        class: 'd-none',
      },
      filterInput: {
        class: 'form-control mx-2',
        placeholder:
          messages.pages.courseManagementPage.editCourseDialog
            .multiSelect.searchPlaceholder,
      },
      headerCheckbox: {
        input: 'form-check-input',
      },
      itemCheckbox: {
        input: 'form-check-input me-2',
      },
      transition: {
        name: 'slide-fade',
      },
      item: {
        class: 'd-flex',
      },
      removeTokenIcon: {
        class: 'ms-1',
      },
    }" />

      <div class="d-flex justify-content-end mt-2 mb-3">
        <Button type="button" class="btn btn-outline-danger mx-1" @click="groupTreatmentDialog = false">{{ messages.pages.coursePage.groupTreatmentDialog.cancelButton }}</Button>
        <Button type="button" class="btn btn-outline-success mx-1" @click="saveGroups">{{ messages.pages.coursePage.groupTreatmentDialog.saveButton }}</Button>
      </div>
    </BaseDialog>

    <BaseDialog v-if="newAssignmentDialogVisible" :visible="newAssignmentDialogVisible"
      :header="messages.pages.coursePage.newTopicDialog.title" :width="'25rem'">
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
              @click="newAssignmentDialogVisible = false"
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
    </BaseDialog>

    <div v-if="!loading">
      <h1 class="text-center my-3">
        {{ course.name }} ({{ this.$route.query.groupName }})
        <button class="btn buttons text-light" v-if="currentUserData.is_admin ||
      member.permission == 'Tanár' ||
      (currentUserData.is_admin && member.permission == 'Tanár')
      " @click="newTopicDialogVisible = true">
          {{ messages.pages.coursePage.newTopicButton }}
        </button>
        <button class="btn ms-1 buttons text-light" id="manageCourses" v-if="currentUserData.is_admin ||
      member.permission == 'Tanár' ||
      (currentUserData.is_admin && member.permission == 'Tanár')
      " @click="groupTreatmentDialog = true">
          {{ messages.pages.coursePage.groupTreatmentButton }}
        </button>
      </h1>

      <div class="accordion mb-3" id="accordionExample" v-for="topic in topics" :key="topic.id">
        <div class="accordion-item">
          <h2 class="accordion-header" :id="'heading' + topic.id">

            <button class="accordion-button" type="button" :class="{ collapsed: activeTopicId !== topic.id }"
              :data-bs-toggle="activeTopicId !== topic.id ? 'collapse' : ''" :data-bs-target="'#collapse' + topic.id"
              :aria-expanded="activeTopicId === topic.id ? 'true' : 'false'" :aria-controls="'collapse' + topic.id">
              <h2>{{ topic.name }}</h2>

              <div v-if="currentUserData.is_admin ||
                member.permission == 'Tanár' ||
                (currentUserData.is_admin && member.permission == 'Tanár')
                " class="dropdown ms-2">
                <button class="btn dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false">
                  Műveletek
                </button>
                <ul class="dropdown-menu"> 
                  <li><a class="dropdown-item" @click="newAssignmentDialogVisible = true, activeTopicId = topic.id">Új feladat</a></li>
                  <li><a class="dropdown-item" @click="editTopicDialogVisible = true, activeTopicId = topic.id">{{messages.pages.coursePage.editButton}}</a></li>
                  <li><a class="dropdown-item" @click="deleteTopic(topic.id)">{{ messages.pages.coursePage.deleteButton }}</a></li>
                </ul>
              </div>
            
            
            </button>
          </h2>
          <div :id="'collapse' + topic.id" class="accordion-collapse collapse"
            :class="{ show: activeTopicId === topic.id }">
            <div class="accordion-body">
              <strong>This is the accordion body for {{ topic.name }}.</strong>
              It is shown by default, until the collapse plugin adds the
              appropriate classes that we use to style each element. These
              classes control the overall appearance, as well as the showing and
              hiding via CSS transitions. You can modify any of this with custom
              CSS or overriding our default variables. It's also worth noting
              that just about any HTML can go within the
              <code>.accordion-body</code>, though the transition does limit
              overflow.
            </div>
          </div>
        </div>
      </div>
    </div>
  </BaseLayout>
</template>

<script>
import BaseLayout from "@layouts/BaseLayout.vue";
import BaseSpinner from "@components/BaseSpinner.vue";
import MultiSelect from "primevue/multiselect";
import { mapState, mapActions } from "pinia";
import { courseStore } from "@stores/CourseStore.mjs";
import { groupStore } from "@stores/GroupStore.mjs";
import { userStore } from "@stores/UserStore.mjs";
import { topicStore } from "@stores/TopicStore.mjs";
import { languageStore } from "@stores/LanguageStore.mjs";
import BaseDialog from "@components/BaseDialog.vue";
import BaseToast from "@components/BaseToast.vue";
import { http } from "@utils/http.mjs";
import Button from "primevue/button";

export default {
  data() {
    return {
      course: {},
      topics: [],
      member: {},
      loading: true,
      newTopicDialogVisible: false,
      editTopicDialogVisible: false,
      groupTreatmentDialog: false,
      newAssignmentDialogVisible: false,
      activeTopicId: null,
      currentGroups: []
    };
  },
  components: {
    BaseLayout,
    BaseDialog,
    Button,
    BaseSpinner,
    MultiSelect,
    BaseToast
  },
  methods: {
    ...mapActions(courseStore, ["getCourse"]),
    ...mapActions(groupStore, ["getGroups", "getGroup"]),
    ...mapActions(topicStore, ["postTopic", "putTopic", "destroyTopic"]),

    async deleteTopic(id) {
      await this.destroyTopic(id);
      this.course = await this.getCourse(this.$route.params.id);
      this.topics = this.course.topics;
    },

    async addTopic(data) {
      const response = await this.postTopic(data);
      await http.post(
        `/courses/${this.$route.params.id}/topics`,
        {
          topic_ids: [response.id],
        },
        {
          headers: {
            Authorization: `Bearer ${this.token}`,
          },
        }
      );

      this.course = await this.getCourse(this.$route.params.id);
      this.topics = this.course.topics;
      this.newTopicDialogVisible = false;
    },

    async editTopic(data) {
      await this.putTopic(this.activeTopicId, data);

      const updatedTopicIndex = this.topics.findIndex(topic => topic.id === this.activeTopicId);
      if (updatedTopicIndex !== -1) {
        this.topics[updatedTopicIndex] = { ...this.topics[updatedTopicIndex], ...data };
      }
      this.editTopicDialogVisible = false;
    },

    async findUserDetails(courseId, userId, groupName) {
      let groups = await this.getGroups();

      if (!groups || !Array.isArray(groups)) {
        console.error("No valid groups array found.");
        return null;
      }

      const group = groups.find((g) => g.name === groupName);
      if (!group) {
        console.error("Group not found with name:", groupName);
        return null;
      }

      const courseFound = group.courses.some(
        (course) => course.id === courseId
      );
      if (!courseFound) {
        console.error("Course not found within group with name:", groupName);
        return null;
      }

      const user = group.users.find((user) => user.id === userId);
      if (!user) {
        console.error("User not found in group with ID:", userId);
        return null;
      }

      this.member = user.member;
      return user;
    },

    async saveGroups() {
      const group_ids = this.currentGroups?.map(
        (group) => group.id
      );

      await http.post(  
        `/courses/${this.$route.params.id}/groups`,
        {
          group_ids: group_ids === undefined ? [] : group_ids,
        },
        {
          headers: {
            Authorization: `Bearer ${this.token}`,
          },
        }
      );
      this.groupTreatmentDialog = false;
    },
    async postNewAssignment(data) {
      try {
        const formData = new FormData();
          formData.append('task_name', data.task_name);
          formData.append('comment', data.comment);
          formData.append('deadline', new Date(data.deadline).toISOString().slice(0, 19).replace('T', ' '));
          formData.append('teacher_task', data.teacher_task[0].file);
          formData.append('courseable_id', this.$route.params.id);
          formData.append('teacher_task_name', data.teacher_task[0].name);
          formData.append('topic_id', this.activeTopicId);
        

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
        this.newAssignmentDialogVisible = false;

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
  computed: {
    ...mapState(userStore, ["currentUserData", "token"]),
    ...mapState(languageStore, ["messages"]),
    ...mapState(groupStore, ["groups"]),
  },
  async mounted() {
    this.course = await this.getCourse(this.$route.params.id);
    this.topics = this.course.topics;
    await this.findUserDetails(
      this.course.id,
      this.currentUserData.id,
      this.$route.query.groupName
    );
    document.title = this.course.name;
    this.loading = false;
  },
};
</script>
