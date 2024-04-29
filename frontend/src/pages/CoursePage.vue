<template>
  <BaseLayout>
    <BaseSpinner :loading="loading" />
    <BaseToast />
    <BaseConfirmDialog />

    <BaseDialog
      v-if="newTopicDialogVisible"
      :visible="newTopicDialogVisible"
      :header="messages.pages.coursePage.newTopicDialog.title"
      :width="'25rem'"
    >
      <FormKit
        type="form"
        :actions="false"
        @submit="addTopic"
        :incomplete-message="
          messages.pages.coursePage.newTopicDialog.validationMessages
            .matchAllValidationMessage
        "
      >
        <FormKit
          type="text"
          name="name"
          :label="messages.pages.coursePage.newTopicDialog.nameLabel"
          validation="required|length:0,60"
          :validation-messages="{
            required:
              messages.pages.coursePage.newTopicDialog.validationMessages
                .nameRequired,
            length:
              messages.pages.coursePage.newTopicDialog.validationMessages
                .nameLength,
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
          name="order"
          :label="messages.pages.coursePage.newTopicDialog.orderLabel"
          validation="number"
          :validation-messages="{
            number:
              messages.pages.coursePage.newTopicDialog.validationMessages
                .orderNumber,
          }"
          :classes="{
            input: {
              'mb-1': true,
              'form-control': true,
            },
          }"
        />

        <div class="d-flex justify-content-end mt-2 mb-3">
          <Button
            type="button"
            class="btn btn-outline-danger mx-1"
            @click="newTopicDialogVisible = false"
            >{{ messages.pages.coursePage.newTopicDialog.cancelButton }}</Button
          >
          <FormKit
            type="submit"
            :label="messages.pages.coursePage.newTopicDialog.saveButton"
            :classes="{
              input: {
                btn: true,
                'btn-success': true,
                'w-auto': true,
              },
            }"
          />
        </div>
      </FormKit>
    </BaseDialog>

    <BaseDialog
      v-if="editTopicDialogVisible"
      :visible="editTopicDialogVisible"
      :header="messages.pages.coursePage.editTopicDialog.title"
      :width="'25rem'"
    >
      <FormKit
        type="form"
        :actions="false"
        @submit="editTopic"
        :incomplete-message="
          messages.pages.coursePage.editTopicDialog.validationMessages
            .matchAllValidationMessage
        "
      >
        <FormKit
          type="text"
          name="name"
          :label="messages.pages.coursePage.editTopicDialog.nameLabel"
          validation="length:0,60"
          :validation-messages="{
            length:
              messages.pages.coursePage.editTopicDialog.validationMessages
                .nameLength,
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
          name="order"
          :label="messages.pages.coursePage.editTopicDialog.orderLabel"
          validation="number"
          :validation-messages="{
            number:
              messages.pages.coursePage.editTopicDialog.validationMessages
                .orderNumber,
          }"
          :classes="{
            input: {
              'mb-1': true,
              'form-control': true,
            },
          }"
        />

        <div class="d-flex justify-content-end mt-2 mb-3">
          <Button
            type="button"
            class="btn btn-outline-danger mx-1"
            @click="editTopicDialogVisible = false"
            >{{
              messages.pages.coursePage.editTopicDialog.cancelButton
            }}</Button
          >
          <FormKit
            type="submit"
            :label="messages.pages.coursePage.editTopicDialog.saveButton"
            :classes="{
              input: {
                btn: true,
                'btn-success': true,
                'w-auto': true,
              },
            }"
          />
        </div>
      </FormKit>
    </BaseDialog>

    <BaseDialog
      v-if="groupTreatmentDialog"
      :visible="groupTreatmentDialog"
      :header="messages.pages.coursePage.groupTreatmentDialog.title"
      :width="'25rem'"
    >
      <MultiSelect
        v-model="currentGroups"
        :options="groups"
        filter
        :maxSelectedLabels="3"
        optionLabel="name"
        :placeholder="
          messages.pages.coursePage.groupTreatmentDialog.multiSelectPlaceholder
        "
        display="chip"
        :pt="{
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
              messages.pages.courseManagementPage.editCourseDialog.multiSelect
                .searchPlaceholder,
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
        }"
      />

      <div class="d-flex justify-content-end mt-2 mb-3">
        <Button
          type="button"
          class="btn btn-outline-danger mx-1"
          @click="groupTreatmentDialog = false"
          >{{
            messages.pages.coursePage.groupTreatmentDialog.cancelButton
          }}</Button
        >
        <Button
          type="button"
          id="modifyGroups"
          class="btn btn-outline-success mx-1"
          @click="saveGroups"
          >{{
            messages.pages.coursePage.groupTreatmentDialog.saveButton
          }}</Button
        >
      </div>
    </BaseDialog>

    <BaseDialog
      v-if="newAssignmentDialogVisible"
      :visible="newAssignmentDialogVisible"
      :header="messages.pages.newAssignmentPage.title"
      :width="'25rem'"
    >
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
              messages.pages.newAssignmentPage.validationMessages
                .task_nameRequired,
            length:
              messages.pages.newAssignmentPage.validationMessages
                .task_nameLength,
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
          :label="messages.pages.newAssignmentPage.deadline"
          validation="required"
          :validation-messages="{
            required:
              messages.pages.newAssignmentPage.validationMessages
                .deadlineRequired,
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
          :label="messages.pages.newAssignmentPage.teacher_task"
          multiple="true"
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
        <div class="d-flex justify-content-end mt-2 mb-3">
          <Button
            type="button"
            :label="messages.pages.newAssignmentPage.cancelButton"
            class="btn btn-outline-danger mx-1 px-5"
            @click="newAssignmentDialogVisible = false"
          ></Button>
          <FormKit
            type="submit"
            :label="messages.pages.newAssignmentPage.saveButton"
            id="addUserButton"
            :classes="{
              input: {
                btn: true,
                'btn-success': true,
                'w-auto': true,
                'px-5': true,
              },
            }"
          />
        </div>
      </FormKit>
    </BaseDialog>

    <BaseDialog
      v-if="UpdateAssignmentDialogVisible"
      :visible="UpdateAssignmentDialogVisible"
      :header="messages.pages.newAssignmentPage.updateTitle"
      :width="'25rem'"
    >
      <FormKit
        type="form"
        :actions="false"
        @submit="updateAssignment"
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
              messages.pages.newAssignmentPage.validationMessages
                .task_nameRequired,
            length:
              messages.pages.newAssignmentPage.validationMessages
                .task_nameLength,
          }"
          :value="currentAssignment.task_name"
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
          :value="currentAssignment.comment"
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
          :label="messages.pages.newAssignmentPage.deadline"
          validation="required"
          :validation-messages="{
            required:
              messages.pages.newAssignmentPage.validationMessages
                .deadlineRequired,
          }"
          :value="currentAssignment.deadline"
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
          :label="messages.pages.newAssignmentPage.teacher_task"
          multiple="true"
          validation=""
          :value="currentAssignment.teacher_task"
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
        <div class="d-flex justify-content-end mt-2 mb-3">
          <Button
            type="button"
            :label="messages.pages.newAssignmentPage.cancelButton"
            class="btn btn-outline-danger mx-1 px-5"
            @click="UpdateAssignmentDialogVisible = false"
          ></Button>
          <FormKit
            type="submit"
            :label="messages.pages.newAssignmentPage.saveButton"
            id="addUserButton"
            :classes="{
              input: {
                btn: true,
                'btn-success': true,
                'w-auto': true,
                'px-5': true,
              },
            }"
          />
        </div>
      </FormKit>
    </BaseDialog>

    <BaseDialog
      v-if="newNoteDialogVisible"
      :visible="newNoteDialogVisible"
      :width="'50rem'"
    >
      <div class="container rounded-3 pt-1 pb-3">
        <h1 class="text-center my-3">
          {{ messages.pages.coursePage.newNoteDialog.title }}
        </h1>
        <div class="d-flex align-items-center justify-content-center pb-4">
          <label for="notetitle" class="form-label me-2 font-weight-bold"
            ><b>{{
              messages.pages.coursePage.newNoteDialog.notesNameText
            }}</b></label
          >
          <div class="w-25">
            <InputText
              id="notetitle"
              v-model="title"
              class="form-control"
              :pt="{
                root: {
                  style: 'border-color: black 1px solid',
                },
              }"
            />
          </div>
        </div>
        <div class="card">
          <Editor v-model="text" editorStyle="height: 320px" />
        </div>
        <div class="d-flex justify-content-center align-items-center mt-3">
          <Button
            :label="messages.pages.coursePage.newNoteDialog.cancelButton"
            class="btn text-light btn-danger px-5 me-4"
            @click="newNoteDialogVisible = false"
          ></Button>
          <Button
            id="saveNoteButton"
            :label="messages.pages.coursePage.newNoteDialog.saveButton"
            class="btn text-light btn-success px-5"
            @click="saveNote"
          ></Button>
        </div>
      </div>
    </BaseDialog>

    <BaseDialog
      v-if="currentNoteVisible"
      :visible="currentNoteVisible"
      :width="'50rem'"
    >
      <div class="container rounded-3 pt-1 pb-3">
        <h1 class="text-center my-3">
          {{ messages.pages.coursePage.currentNoteDialog.title }}
        </h1>
        <div class="d-flex align-items-center justify-content-center pb-4">
          <label for="title" class="form-label me-2 font-weight-bold"
            ><b>{{
              messages.pages.coursePage.currentNoteDialog.notesNameText
            }}</b></label
          >
          <div class="w-25">
            <InputText
              id="title"
              v-model="currentTitle"
              class="form-control"
              :pt="{
                root: {
                  style: 'border-color: black 1px solid',
                },
              }"
            />
          </div>
        </div>
        <div class="card">
          <Editor
            :readonly="isNoteReadonly"
            v-model="currentText"
            :model-value="currentNote.text"
            editorStyle="height: 320px"
          />
        </div>

        <div class="d-flex justify-content-center align-items-center mt-3">
          <div class="btn-group" role="group">
            <button
              type="button"
              class="btn"
              :class="{
                'btn-outline-danger': isDarkMode,
                'btn-danger': !isDarkMode,
              }"
              @click="(currentNoteVisible = false), (isNoteReadonly = true)"
            >
              {{ messages.pages.coursePage.currentNoteDialog.cancelButton }}
            </button>
            <button
              type="button"
              class="btn"
              :class="{
                'btn-outline-primary': isDarkMode,
                'btn-primary': !isDarkMode,
              }"
              @click="isNoteReadonly = false"
            >
              {{ messages.pages.coursePage.currentNoteDialog.editButton }}
            </button>
            <button
              type="button"
              id="deleteNoteButton"
              class="btn"
              :class="{
                'btn-outline-warning': isDarkMode,
                'btn-warning': !isDarkMode,
              }"
              @click="deleteNote()"
            >
              {{ messages.pages.coursePage.currentNoteDialog.deleteButton }}
            </button>
            <button
              type="button"
              id="modifyNoteButton"
              class="btn"
              :class="{
                'btn-outline-success': isDarkMode,
                'btn-success': !isDarkMode,
              }"
              @click="(isNoteReadonly = true), updateNote()"
            >
              {{ messages.pages.coursePage.currentNoteDialog.saveButton }}
            </button>
          </div>
        </div>
      </div>
    </BaseDialog>

    <div v-if="!loading">
      <h1 class="text-center my-3">
        {{ course.name }} ({{ this.$route.query.groupName }})
        <button
          class="btn buttons text-light"
          id="newTopic"
          v-if="
            currentUserData.is_admin ||
            member.permission == 'Tanár' ||
            (currentUserData.is_admin && member.permission == 'Tanár')
          "
          @click="newTopicDialogVisible = true"
        >
          {{ messages.pages.coursePage.newTopicButton }}
        </button>
        <button
          class="btn ms-1 buttons text-light"
          id="manageCourses"
          v-if="
            currentUserData.is_admin ||
            member.permission == 'Tanár' ||
            (currentUserData.is_admin && member.permission == 'Tanár')
          "
          @click="groupTreatmentDialog = true"
        >
          {{ messages.pages.coursePage.groupTreatmentButton }}
        </button>
      </h1>

      <div
        class="accordion mb-3"
        id="accordionExample"
        v-for="topic in topics"
        :key="topic.id"
      >
        <div class="accordion-item">
          <h2 class="accordion-header" :id="'heading' + topic.id">
            <button
              class="accordion-button"
              type="button"
              :class="{ collapsed: activeTopicId !== topic.id }"
              :data-bs-toggle="activeTopicId !== topic.id ? 'collapse' : ''"
              :data-bs-target="'#collapse' + topic.id"
              :aria-expanded="activeTopicId === topic.id ? 'true' : 'false'"
              :aria-controls="'collapse' + topic.id"
              @click="toggleTopic(topic.id)"
            >
              <h2>{{ topic.name }}</h2>

              <button
                class="btn buttons ms-2"
                @click="
                  (newNoteDialogVisible = true), (activeTopicId = topic.id)
                "
                v-if="
                  member.permission == 'Tanuló' && !currentUserData.is_admin
                "
              >
                {{ messages.pages.coursePage.accordionText.createNoteButton }}
              </button>
            </button>
          </h2>
          <div
            :id="'collapse' + topic.id"
            class="accordion-collapse collapse"
            :class="{ show: activeTopicId === topic.id }"
          >
            <div
              v-if="
                currentUserData.is_admin ||
                member.permission == 'Tanár' ||
                (currentUserData.is_admin && member.permission == 'Tanár')
              "
              class="dropdown-center text-center"
            >
              <button
                id="dropdownMenu"
                class="btn dropdown-toggle"
                type="button"
                data-bs-toggle="dropdown"
                aria-expanded="false"
              >
                {{ messages.pages.coursePage.accordionText.actions }}
              </button>
              <ul class="dropdown-menu">
                <li>
                  <a
                    class="dropdown-item text-center btn"
                    @click="
                      (newAssignmentDialogVisible = true),
                        (activeTopicId = topic.id)
                    "
                    >{{
                      messages.pages.coursePage.accordionText.newAssignment
                    }}</a
                  >
                </li>
                <li>
                  <a
                    class="dropdown-item text-center btn"
                    @click="
                      (newNoteDialogVisible = true), (activeTopicId = topic.id)
                    "
                    >{{ messages.pages.coursePage.accordionText.newNote }}</a
                  >
                </li>
                <li>
                  <button
                    class="dropdown-item text-center"
                    @click="navigateToNewQuizPage(course.id, topic.id)"
                  >
                    {{ messages.pages.coursePage.accordionText.newQuiz }}
                  </button>
                </li>
                <li>
                  <a
                    class="dropdown-item text-center btn"
                    @click="
                      (editTopicDialogVisible = true),
                        (activeTopicId = topic.id)
                    "
                    >{{ messages.pages.coursePage.accordionText.edit }}</a
                  >
                </li>
                <li>
                  <a class="dropdown-item text-center btn" @click="deleteTopic(topic.id)">{{
                    messages.pages.coursePage.accordionText.delete
                  }}</a>
                </li>
              </ul>
            </div>
            <div class="accordion-body">
              <div
                class="card mt-2"
                v-for="assignment in topic.assignment"
                :key="assignment.id"
              >
                <div class="d-flex justify-content-between align-items-center">
                  <p class="flex ms-2 mt-2 mb-2">
                    {{ messages.pages.assignmentPage.task_name }}
                    {{ assignment.task_name }} <br />
                    {{ messages.pages.assignmentPage.deadline }}
                    {{ assignment.deadline }} <br />
                    <span v-if="assignment.comment"
                      >{{ messages.pages.assignmentPage.comment }}
                      {{ assignment.comment }}</span
                    >
                  </p>
                  <div class="flex">
                    <div
                      class="btn-group me-2"
                      role="group"
                      aria-label="Basic mixed styles example"
                      v-if="
                        currentUserData.is_admin ||
                        member.permission == 'Tanár' ||
                        (currentUserData.is_admin &&
                          member.permission == 'Tanár')
                      "
                    >
                      <button
                        type="button"
                        class="btn btn-outline-danger"
                        @click="deleteAssignment(assignment.id)"
                      >
                        <i class="fa-solid fa-trash"></i>
                      </button>
                      <button
                        type="button"
                        class="btn btn-outline-warning"
                        @click="openUpdateAssignment(assignment.id)"
                      >
                        <i class="fa-solid fa-pencil"></i>
                      </button>
                    </div>
                    <RouterLink
                      class="btn btn-outline-primary me-2 px-5"
                      :to="{
                        name: 'assignment',
                        params: { id: assignment.id },
                        query: { groupName: this.$route.query.groupName },
                      }"
                      >{{ messages.pages.coursePage.viewButton }}</RouterLink
                    >
                  </div>
                </div>
              </div>
              <div class="row">
                <div
                  class="col-12 col-md-4 col-lg-3 h-100"
                  v-for="note in topic.notes"
                  :key="note.id"
                >
                  <div class="card mt-2 text-center">
                    <div class="card-header">
                      <h4>{{ messages.pages.coursePage.note.name }}</h4>
                    </div>
                    <div class="card-body">
                      {{ messages.pages.coursePage.note.text }} {{ note.title }}
                    </div>
                    <div class="card-footer">
                      <button
                        class="btn buttons"
                        type="button"
                        @click="
                          openCurrentNote(note),
                            (currentlyModifyingNote = note.id),
                            (activeTopicId = topic.id)
                        "
                      >
                        {{ messages.pages.coursePage.note.viewButton }}
                      </button>
                    </div>
                  </div>
                </div>
              </div>
              <div class="row">
                <div
                  class="col-12 col-lg-6 my-3"
                  v-for="quiz in topic.quizzes"
                  :key="quiz.id"
                >
                  <div class="card h-100 mt-2 text-center">
                    <div class="card-header">
                      <h4>{{ messages.pages.coursePage.quiz.name }}</h4>
                    </div>
                    <div class="card-body">
                      <span class="fw-bold fs-5">{{ quiz.name }}</span>
                      <table class="mt-2 mb-0 table table-striped">
                        <tbody>
                          <tr>
                            <td class="w-50">
                              {{ messages.pages.coursePage.quiz.opens }}
                            </td>
                            <td class="w-50" v-if="quiz.opens != null">
                              {{ toDate(quiz.opens) }}
                            </td>
                            <td class="w-50" v-else>-</td>
                          </tr>
                          <tr>
                            <td class="w-50">
                              {{ messages.pages.coursePage.quiz.closes }}
                            </td>
                            <td class="w-50" v-if="quiz.closes != null">
                              {{ toDate(quiz.closes) }}
                            </td>
                            <td class="w-50" v-else>-</td>
                          </tr>
                          <tr>
                            <td class="w-50">
                              {{ messages.pages.coursePage.quiz.time }}
                            </td>
                            <td class="w-50" v-if="quiz.time != null">
                              {{ quiz.time }}
                              {{ messages.pages.coursePage.quiz.minutes }}
                            </td>
                            <td class="w-50" v-else>-</td>
                          </tr>
                          <tr>
                            <td class="w-50">
                              {{ messages.pages.coursePage.quiz.attempts }}
                            </td>
                            <td class="w-50" v-if="quiz.max_attempts != null">
                              {{ quiz.max_attempts }}
                            </td>
                            <td class="w-50" v-else>-</td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                    <div class="card-footer">
                      <div
                        class="d-flex justify-content-center align-self-center gap-1"
                      >
                        <button class="btn btn-primary" type="button" @click="">
                          {{ messages.pages.coursePage.note.viewButton }}
                        </button>
                        <button
                          v-if="
                            currentUserData.is_admin ||
                            member.permission == 'Tanár' ||
                            (currentUserData.is_admin &&
                              member.permission == 'Tanár')
                          "
                          class="btn btn-secondary text-white"
                          type="button"
                          @click="navigateToEditQuizPage(quiz.id)"
                        >
                          <i class="fa-solid fa-pen"></i>
                        </button>
                        <button
                          v-if="
                            currentUserData.is_admin ||
                            member.permission == 'Tanár' ||
                            (currentUserData.is_admin &&
                              member.permission == 'Tanár')
                          "
                          class="btn btn-danger text-white"
                          type="button"
                          @click="confirmDeleteSubtask(quiz.id, topic.id)"
                        >
                          <i class="fa-solid fa-trash"></i>
                        </button>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
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
import { noteStore } from "@stores/NoteStore.mjs";
import { topicStore } from "@stores/TopicStore.mjs";
import { themeStore } from "@stores/ThemeStore.mjs";
import { languageStore } from "@stores/LanguageStore.mjs";
import BaseDialog from "@components/BaseDialog.vue";
import BaseToast from "@components/BaseToast.vue";
import { http } from "@utils/http.mjs";
import Button from "primevue/button";
import InputText from "primevue/inputtext";
import Editor from "primevue/editor";
import BaseConfirmDialog from "@components/BaseConfirmDialog.vue";
import { RouterLink } from "vue-router";
import { useRouter } from "vue-router";
import { getCurrentInstance } from "vue";

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
      UpdateAssignmentDialogVisible: false,
      newNoteDialogVisible: false,
      currentNoteVisible: false,
      isNoteReadonly: true,
      activeTopicId: null,
      currentGroups: [],
      currentNote: {},
      title: "",
      text: "",
      currentTitle: "",
      currentText: "",
      currentlyModifyingNote: null,
      currentAssignment: {
        task_name: "",
        comment: "",
        deadline: "",
        teacher_task: null,
        teacher_task_name: "",
      },
    };
  },
  components: {
    BaseLayout,
    BaseDialog,
    Button,
    BaseSpinner,
    MultiSelect,
    BaseToast,
    InputText,
    Editor,
    BaseConfirmDialog,
  },
  methods: {
    ...mapActions(courseStore, ["getCourse"]),
    ...mapActions(groupStore, ["getGroups", "getGroup"]),
    ...mapActions(topicStore, ["postTopic", "putTopic", "destroyTopic"]),
    ...mapActions(noteStore, ["postNote", "putNote", "destroyNote"]),

    toggleTopic(id) {
      this.activeTopicId = this.activeTopicId === id ? null : id;
    },

    openUpdateAssignment(id) {
      console.log(this.activeTopicId);
      let topic = this.topics.find((x) => x.id == this.activeTopicId);
      if (topic) {
        let assignment = topic.assignment.find((a) => a.id === id);
        if (assignment) {
          this.currentAssignment.task_name = assignment.task_name;
          this.currentAssignment.comment = assignment.comment;
          this.currentAssignment.deadline = assignment.deadline;
          this.currentAssignment.teacher_task = assignment.teacher_task;
          this.currentAssignment.teacher_task_name =
            assignment.teacher_task_name;
          this.UpdateAssignmentDialogVisible = true;
        }
      }
    },

    openCurrentNote(note) {
      this.currentNote = note;
      this.currentTitle = note.title;
      this.currentText = note.text;
      this.currentNoteVisible = true;
    },

    async deleteNote() {
      try {
        this.$confirm.require({
          message: this.messages.pages.coursePage.confirmDialogs.messageNote,
          icon: "pi pi-exclamation-triangle",
          rejectClass: "btn btn-danger",
          acceptClass: "btn btn-success ",
          rejectLabel:
            this.messages.pages.coursePage.confirmDialogs.rejectLabel,
          acceptLabel:
            this.messages.pages.coursePage.confirmDialogs.acceptLabel,
          accept: async () => {
            await this.destroyNote(this.currentNote.id);

            const topicIndex = this.topics.findIndex(
              (topic) => topic.id == this.activeTopicId
            );
            const noteIndex = this.topics[topicIndex].notes.findIndex(
              (note) => note.id == this.currentlyModifyingNote
            );
            if (noteIndex !== -1) {
              this.topics[topicIndex].notes.splice(noteIndex, 1);
            }

            this.currentNoteVisible = false;

            let toast = {
              severity: "success",
              detail:
                this.messages.pages.coursePage.note.toastMessages
                  .successfullyDeletedNote,
              life: 3000,
            };
            if (!this.isDarkMode) {
              toast.styleClass = "bg-success text-white";
            } else {
              toast.styleClass = "toast-success text-white";
            }

            this.$toast.add(toast);
          },
        });
      } catch (error) {
        let toast = {
          severity: "error",
          detail:
            this.messages.pages.coursePage.note.toastMessages
              .failedToDeleteNote,
          life: 3000,
        };
        if (!this.isDarkMode) {
          toast.styleClass = "bg-danger text-white";
        } else {
          toast.styleClass = "toast-danger text-white";
        }
        this.$toast.add(toast);
      }
    },

    async updateNote() {
      try {
        const newNote = {
          title: this.currentTitle,
          text: this.currentText,
        };

        const response = await this.putNote(
          this.currentlyModifyingNote,
          newNote
        );

        const topicIndex = this.topics.findIndex(
          (topic) => topic.id == this.activeTopicId
        );
        const noteIndex = this.topics[topicIndex].notes.findIndex(
          (note) => note.id == this.currentlyModifyingNote
        );
        if (noteIndex !== -1) {
          this.topics[topicIndex].notes.splice(noteIndex, 1, response);
        }

        this.currentNoteVisible = false;

        let toast = {
          severity: "success",
          detail:
            this.messages.pages.coursePage.note.toastMessages
              .successfullyUpdatedNote,
          life: 3000,
        };
        if (!this.isDarkMode) {
          toast.styleClass = "bg-success text-white";
        } else {
          toast.styleClass = "toast-success text-white";
        }

        this.$toast.add(toast);
      } catch (error) {
        let toast = {
          severity: "error",
          detail:
            this.messages.pages.coursePage.note.toastMessages
              .failedToUpdateNote,
          life: 3000,
        };
        if (!this.isDarkMode) {
          toast.styleClass = "bg-danger text-white";
        } else {
          toast.styleClass = "toast-danger text-white";
        }
        this.$toast.add(toast);
      }
    },

    async saveNote() {
      try {
        const note = {
          title: this.title,
          text: this.text,
          topic_id: this.activeTopicId,
          user_id: this.currentUserData.id,
        };

        const response = await this.postNote(note);

        const updatedTopicIndex = this.topics.findIndex(
          (topic) => topic.id === this.activeTopicId
        );
        if (updatedTopicIndex !== -1) {
          this.topics[updatedTopicIndex].notes.push(response);
        }

        this.newNoteDialogVisible = false;

        let toast = {
          severity: "success",
          detail:
            this.messages.pages.coursePage.note.toastMessages
              .successfullyCreatedNote,
          life: 3000,
        };
        if (!this.isDarkMode) {
          toast.styleClass = "bg-success text-white";
        } else {
          toast.styleClass = "toast-success text-white";
        }

        this.$toast.add(toast);
      } catch (error) {
        let toast = {
          severity: "error",
          detail:
            this.messages.pages.coursePage.note.toastMessages
              .failedToCreateNote,
          life: 3000,
        };
        if (!this.isDarkMode) {
          toast.styleClass = "bg-danger text-white";
        } else {
          toast.styleClass = "toast-danger text-white";
        }
        this.$toast.add(toast);
      }
    },

    async deleteTopic(id) {
      try {
        this.$confirm.require({
          message: this.messages.pages.coursePage.confirmDialogs.messageTopic,
          icon: "pi pi-exclamation-triangle",
          rejectClass: "btn btn-danger",
          acceptClass: "btn btn-success ",
          rejectLabel:
            this.messages.pages.coursePage.confirmDialogs.rejectLabel,
          acceptLabel:
            this.messages.pages.coursePage.confirmDialogs.acceptLabel,
          accept: async () => {
            await this.destroyTopic(id);
            this.course = await this.getCourse(this.$route.params.id);
            this.topics = this.course.topics;

            let toast = {
              severity: "success",
              detail:
                this.messages.pages.coursePage.deleteTopic.toastMessages
                  .successfullyDeletedTopic,
              life: 3000,
            };
            if (!this.isDarkMode) {
              toast.styleClass = "bg-success text-white";
            } else {
              toast.styleClass = "toast-success text-white";
            }
            this.$toast.add(toast);
          },
        });
      } catch (error) {
        let toast = {
          severity: "error",
          detail:
            this.messages.pages.coursePage.deleteTopic.toastMessages
              .successfullyDeletedTopic,
          life: 3000,
        };
        if (!this.isDarkMode) {
          toast.styleClass = "bg-danger text-white";
        } else {
          toast.styleClass = "toast-danger text-white";
        }
        this.$toast.add(toast);
      }
    },

    async addTopic(data) {
      try {
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

        let toast = {
          severity: "success",
          detail:
            this.messages.pages.coursePage.addTopic.toastMessages
              .successfulyCreatedTopic,
          life: 3000,
        };
        if (!this.isDarkMode) {
          toast.styleClass = "bg-success text-white";
        } else {
          toast.styleClass = "toast-success text-white";
        }
        this.$toast.add(toast);
      } catch (error) {
        let toast = {
          severity: "error",
          detail:
            this.messages.pages.coursePage.addTopic.toastMessages
              .failedToCreateTopic,
          life: 3000,
        };
        if (!this.isDarkMode) {
          toast.styleClass = "bg-danger text-white";
        } else {
          toast.styleClass = "toast-danger text-white";
        }
        this.$toast.add(toast);
      }
    },

    async editTopic(data) {
      try {
        await this.putTopic(this.activeTopicId, data);

        const updatedTopicIndex = this.topics.findIndex(
          (topic) => topic.id === this.activeTopicId
        );
        if (updatedTopicIndex !== -1) {
          this.topics[updatedTopicIndex] = {
            ...this.topics[updatedTopicIndex],
            ...data,
          };
        }
        this.editTopicDialogVisible = false;

        let toast = {
          severity: "success",
          detail:
            this.messages.pages.coursePage.editTopic.toastMessages
              .successfullyUpdatedTopic,
          life: 3000,
        };
        if (!this.isDarkMode) {
          toast.styleClass = "bg-success text-white";
        } else {
          toast.styleClass = "toast-success text-white";
        }
        this.$toast.add(toast);
      } catch (error) {
        let toast = {
          severity: "error",
          detail:
            this.messages.pages.coursePage.editTopic.toastMessages
              .failedToUpdateTopic,
          life: 3000,
        };
        if (!this.isDarkMode) {
          toast.styleClass = "bg-danger text-white";
        } else {
          toast.styleClass = "toast-danger text-white";
        }
        this.$toast.add(toast);
      }
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
      try {
        const group_ids = this.currentGroups?.map((group) => group.id);

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

        let toast = {
          severity: "success",
          detail:
            this.messages.pages.coursePage.saveGroups.toastMessages
              .successfullyUpdatedGroups,
          life: 3000,
        };
        if (!this.isDarkMode) {
          toast.styleClass = "bg-success text-white";
        } else {
          toast.styleClass = "toast-success text-white";
        }
        this.$toast.add(toast);
      } catch (error) {
        let toast = {
          severity: "error",
          detail:
            this.messages.pages.coursePage.saveGroups.toastMessages
              .failedToUpdateGroups,
          life: 3000,
        };
        if (!this.isDarkMode) {
          toast.styleClass = "bg-danger text-white";
        } else {
          toast.styleClass = "toast-danger text-white";
        }
        this.$toast.add(toast);
      }
    },
    async postNewAssignment(data) {
      try {
        const formData = new FormData();
        formData.append("task_name", data.task_name);
        formData.append("comment", data.comment);
        formData.append(
          "deadline",
          new Date(data.deadline).toISOString().slice(0, 19).replace("T", " ")
        );
        formData.append("teacher_task", data?.teacher_task[0]?.file);
        formData.append("courseable_id", this.$route.params.id);
        formData.append("teacher_task_name", data?.teacher_task[0]?.name);
        formData.append("topic_id", this.activeTopicId);
        
        const user = userStore();
        const response = await http.post(`/assignments`, formData, {
          headers: {
            Authorization: `Bearer ${user.token}`,
            "Content-Type": "application/x-www-form-urlencoded",
          },
        });

        this.topics
          .find((x) => x.id == this.activeTopicId)
          .assignment.push(response.data.data);
        let toast = {
          severity: "success",
          detail:
            this.messages.pages.newAssignmentPage.toastMessages
              .successfullyCreatedAssignment,
          life: 3000,
        };
        if (!this.isDarkMode) {
          toast.styleClass = "bg-success text-white";
        } else {
          toast.styleClass = "toast-success text-white";
        }
        this.$toast.add(toast);
        this.newAssignmentDialogVisible = false;
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
        } else {
          toast.styleClass = "toast-danger text-white";
        }
        this.$toast.add(toast);
      }
    },
    async updateAssignment(data) {
      try {
        const formData = new FormData();
        formData.append("task_name", data.task_name);
        formData.append("comment", data.comment);
        formData.append(
          "deadline",
          new Date(data.deadline).toISOString().slice(0, 19).replace("T", " ")
        );
        formData.append("teacher_task", data?.teacher_task[0]?.file);
        formData.append("teacher_task_name", data?.teacher_task[0]?.name);

        const user = userStore();
        const response = await http.put(`/assignments`, formData, {
          headers: {
            Authorization: `Bearer ${user.token}`,
            "Content-Type": "application/x-www-form-urlencoded",
          },
        });

        this.topics
          .find((x) => x.id == this.activeTopicId)
          .assignment.push(response.data.data);
        let toast = {
          severity: "success",
          detail:
            this.messages.pages.newAssignmentPage.toastMessages
              .successfullyCreatedAssignment,
          life: 3000,
        };
        if (!this.isDarkMode) {
          toast.styleClass = "bg-success text-white";
        } else {
          toast.styleClass = "toast-success text-white";
        }

        this.$toast.add(toast);
        this.newAssignmentDialogVisible = false;
      } catch (error) {
        console.log(error);
        let toast = {
          severity: "error",
          detail:
            this.messages.pages.newAssignmentPage.toastMessages
              .failedToCreateAssignment,
          life: 3000,
        };
        if (!this.isDarkMode) {
          toast.styleClass = "bg-danger text-white";
        } else {
          toast.styleClass = "toast-danger text-white";
        }
        this.$toast.add(toast);
      }
    },
    async deleteAssignment(id) {
      try {
        this.$confirm.require({
          message:
            this.messages.pages.coursePage.confirmDialogs.messageAssignment,
          icon: "pi pi-exclamation-triangle",
          rejectClass: "btn btn-danger",
          acceptClass: "btn btn-success ",
          rejectLabel:
            this.messages.pages.coursePage.confirmDialogs.rejectLabel,
          acceptLabel:
            this.messages.pages.coursePage.confirmDialogs.acceptLabel,
          accept: async () => {
            const user = userStore();
            await http.delete(`/assignments/${id}`, {
              headers: {
                Authorization: `Bearer ${user.token}`,
              },
            });
            const idx = this.topics
              .find((x) => x.id == this.activeTopicId)
              .assignment.findIndex((x) => x.id == id);
            this.topics
              .find((x) => x.id == this.activeTopicId)
              .assignment.splice(idx, 1);

            let toast = {
              severity: "success",
              detail:
                this.messages.pages.coursePage.deleteAssignment.toastMessages
                  .successfullyDeletedAssignment,
              life: 3000,
            };
            if (!this.isDarkMode) {
              toast.styleClass = "bg-success text-white";
            } else {
              toast.styleClass = "toast-success text-white";
            }

            this.$toast.add(toast);
          },
        });
      } catch (error) {
        let toast = {
          severity: "error",
          detail:
            this.messages.pages.coursePage.deleteAssignment.toastMessages
              .failedToDeleteAssignment,
          life: 3000,
        };
        if (!this.isDarkMode) {
          toast.styleClass = "bg-danger text-white";
        } else {
          toast.styleClass = "toast-danger text-white";
        }
        this.$toast.add(toast);
      }
    },
    async confirmDeleteSubtask(id, topicId) {
      this.$confirm.require({
        message: "Biztos ki akarja törölni ezt a feladatot?",
        icon: "pi pi-exclamation-triangle",
        rejectClass: "btn btn-danger",
        acceptClass: "btn btn-success ",
        rejectLabel: "Mégse",
        acceptLabel: "Törlés",
        accept: async () => {
          try {
            await http.delete(`quizzes/${id}`, {
              headers: {
                Authorization: `Bearer ${this.token}`,
              },
            });

            const topicIndex = this.topics.findIndex((x) => x.id == topicId);
            const idx = this.topics[topicIndex].quizzes.findIndex(
              (x) => x.id == id
            );
            this.topics[topicIndex].quizzes.splice(idx, 1);

            let toastToAdd = {
              severity: "success",
              detail: "Sikeres törlés!",
              life: 3000,
            };
            if (!this.isDarkMode) {
              toastToAdd.styleClass = "bg-success text-white";
            } else {
              toastToAdd.styleClass = "toast-success text-white";
            }
            this.$toast.add(toastToAdd);
          } catch (error) {
            let toastToAdd = {
              severity: "error",
              detail: "Váratlan hiba a törlésnél!",
              life: 3000,
            };
            if (!this.isDarkMode) {
              toastToAdd.styleClass = "bg-danger text-white";
            } else {
              toastToAdd.styleClass = "toast-danger text-white";
            }
            this.$toast.add(toastToAdd);
          }
        },
      });
    },
  },
  computed: {
    ...mapState(userStore, ["currentUserData", "token"]),
    ...mapState(languageStore, ["messages"]),
    ...mapState(themeStore, ["isDarkMode"]),
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
  setup() {
    const navigateToNewQuizPage = (courseId, topicId) => {
      window.location = `/course/${courseId}/topic/${topicId}/create-quiz`;
    };

    const navigateToEditQuizPage = (id) => {
      window.location = `/quiz/${id}/edit`;
    };

    const toDate = (date) => {
      return new Date(date * 1000).toLocaleString();
    };

    return {
      navigateToNewQuizPage,
      navigateToEditQuizPage,
      toDate,
    };
  },
};
</script>
