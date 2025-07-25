<template>
  <BaseLayout>
    <BaseSpinner :loading="loading" />

    <div v-if="!loading">
      <h1 class="text-center my-3">
        {{ messages.pages.courseManagementPage.title }}
      </h1>

      <BaseConfirmDialog />
      <BaseToast />

      <BaseDialog v-if="addCourseDialogVisible" :width="'25rem'" :visible="addCourseDialogVisible"
        :header="messages.pages.courseManagementPage.newCourseDialog.title">
        <FormKit type="form" :actions="false" @submit="postCourses" :incomplete-message="messages.pages.courseManagementPage.newCourseDialog
          .validationMessages.matchAllValidationMessage
          ">
          <FormKit type="text" name="name" label="Kurzus neve" validation="required|length:5,100" :validation-messages="{
            required:
              messages.pages.courseManagementPage.newCourseDialog
                .validationMessages.nameRequired,
            length:
              messages.pages.courseManagementPage.newCourseDialog
                .validationMessages.nameLength,
          }" :classes="{
      input: {
        'mb-1': true,
        'form-control': true,
      },
    }" />
          <FormKit type="file" name="image" @change="handleFileChange" label="Kép hozzáadása"
            accept=".JPEG,.PNG,.JPG,.BMP" :validation-messages="{
              required:
                messages.pages.courseManagementPage.newCourseDialog
                  .validationMessages.imageRequired,
            }" :classes="{
      input: {
        'mb-1': true,
        'form-control': true,
        'file': true
      },
    }" :title="messages.pages.courseManagementPage.newCourseDialog.fileUpload
      " />
          <div class="d-flex justify-content-end mt-2 mb-3">
            <Button type="button" label="Mégse" class="btn btn-danger text-white mx-1"
              @click="addCourseDialogVisible = false"></Button>
            <FormKit type="submit" label="Mentés" :classes="{
              input: {
                btn: true,
                'btn-success': true,
                'text-white': true,
                'w-auto': true,
                'addCourse': true
              },
            }" />
          </div>
        </FormKit>
      </BaseDialog>
      <BaseDialog v-if="modifyCourseDialogVisible" :width="'25rem'" :visible="modifyCourseDialogVisible" :header="messages.pages.courseManagementPage.editCourseDialog.title +
        ' ' +
        currentlyModifyingCourse.name
        ">
        <FormKit type="form" :actions="false" @submit="updateCourse" :value="currentlyModifyingCourse"
          :incomplete-message="messages.pages.courseManagementPage.editCourseDialog
            .validationMessages.matchAllValidationMessage
            ">
          <FormKit type="text" name="name" label="Kurzus neve" validation="length:0,255" :validation-messages="{
            length:
              messages.pages.courseManagementPage.editCourseDialog
                .validationMessages.nameLength,
          }" :classes="{
      input: {
        'mb-1': true,
        'form-control': true,
      },
    }" />
          <FormKit type="file" name="image" @change="handleFileChange" :label="messages.pages.courseManagementPage.editCourseDialog
            .validationMessages.fileUpload
            " accept=".JPEG,.PNG,.JPG,.BMP" :classes="{
        input: {
          'mb-1': true,
          'form-control': true,
        },
      }" />

          <MultiSelect v-model="currentlyModifyingCourse.groups" :options="groupsNoUselessData" filter
            optionLabel="name" :placeholder="messages.pages.courseManagementPage.editCourseDialog.multiSelect.title"
            display="chip" :pt="{
              list: {
                class: 'rounded-3 w-75 list-style-none p-2',
              },
              header: {
                class: 'rounded-3 w-75 mb-2 d-flex justify-content-center align-items-center p-1',
              },
              closeButton: {
                class: 'btn mb-1',
              },
              filterIcon: {
                class: 'd-none',
              },
              filterInput: {
                class: 'form-control mx-2',
                placeholder: messages.pages.courseManagementPage.editCourseDialog.multiSelect.searchPlaceholder,
              },
              headerCheckbox: {
                input: 'form-check-input',
              },
              itemCheckbox: {
                input: 'form-check-input me-2',
              },
              transition: {
                name: 'multi-select-fade',
              },
              item: {
                class: 'd-flex',
              },
              token: {
                class: 'list-style-none badge text-bg-dark p-2 mb-1 mx-1'
              },
              removeTokenIcon: {
                class: 'ms-2'
              },
              inputToken: {
                class: 'list-style-none'
              },
            }" />

          <div class="d-flex justify-content-end mt-2 mb-3">
            <Button type="button" label="Mégse" class="btn btn-danger text-white mx-1"
              @click="modifyCourseDialogVisible = false"></Button>
            <FormKit type="submit" label="Mentés" :classes="{
              input: {
                btn: true,
                'btn-success': true,
                'text-white': true,
                'w-auto': true,
                'modifyCourse': true
              },
            }" />
          </div>
        </FormKit>
      </BaseDialog>
      <div>
        <div class="card darkTheme">
          <Toolbar :pt="{
            start: {
              class:
                'col-sm-12 col-md-5 d-flex justify-content-md-start align-items-center justify-content-center',
            },
            center: {
              class: 'col-sm-12 col-md-2',
            },
            end: {
              class:
                'col-sm-12 col-md-5 d-flex justify-content-md-end align-items-center justify-content-center',
            },
            root: {
              class: 'row mb-2',
            },
          }">
            <template #start>
              <Button :label="messages.pages.courseManagementPage.newUser" icon="pi pi-plus"
                class="mr-2 btn btn-success text-white me-1 mt-2 ms-2" @click="addCourseDialogVisible = true" />
              <Button :label="messages.pages.courseManagementPage.deleteUser" icon="pi pi-trash"
                class="btn btn-danger text-white mt-2" :disabled="selectedCourses.length === 0"
                @click="deleteMultipleCourses" />
            </template>
            <template #end>
              <Button :label="messages.pages.courseManagementPage.exportButton" icon="pi pi-upload"
                class="btn btn-warning text-white mt-2 me-2" @click="exportCSV($event)" />
            </template>
          </Toolbar>
          <DataTable exportFilename="courses" ref="dt" :value="courses" tableStyle="min-width: 50rem" sortField="id"
            :sortOrder="1" v-model:filters="filters" filterDisplay="row" v-model:selection="selectedCourses"
            selectionMode="multiple" dataKey="id" :metaKeySelection="false" :pt="{
              table: {
                class: 'table table-responsive align-middle',
              },
            }">
            <Column>
              <template #header>
                <div class="d-flex justify-content-center">
                  <button type="button" class="btn" style="
                      --bs-btn-padding-y: 0.25rem;
                      --bs-btn-padding-x: 0.5rem;
                      --bs-btn-font-size: 0.75rem;
                      width: 28px;
                    " @click="selectAllCourses">
                    <i class="fa-solid fa-x text-danger" v-if="selectedCourses.length == 0"></i>
                    <i class="fa-solid fa-check text-success" v-else></i>
                  </button>
                </div>
              </template>
              <template #body="slotProp">
                <div class="d-flex justify-content-center">
                  <i class="fa-solid fa-check text-success" v-if="selectedCourses.findIndex((x) => x == slotProp.data) != -1
                  "></i>
                  <i class="fa-solid fa-x text-danger" v-else></i>
                </div>
              </template>
            </Column>
            <Column field="id" :header="messages.pages.courseManagementPage.idText" sortable></Column>
            <Column field="name" :header="messages.pages.courseManagementPage.nameText" sortable :pt="{
              columnfilter: {
                class: 'd-flex',
              },
              filtermenubutton: {
                class: 'btn ms-1',
              },
              headerfilterclearbutton: {
                class: 'btn ms-1',
              },
            }">
              <template #filter="{ filterModel, filterCallback }">
                <InputText v-model="filterModel.value" type="text" @input="filterCallback()" class="form-control"
                  :placeholder="messages.pages.courseManagementPage.namePlaceholder
                    " />
              </template>
            </Column>
            <Column field="image" :header="messages.pages.courseManagementPage.imageText" style="width: 50%">
              <template #body="slotProp">
                <img v-if="slotProp.data.image" :src="'data:image/jpeg;base64,' + slotProp.data.image"
                  alt="Course Image" class="img-fluid w-75" />
              </template>
            </Column>
            <Column :header="messages.pages.courseManagementPage.modifyText">
              <template #body="slotProp">
                <button type="button" class="btn btn-warning" v-if="slotProp.data.email != currentUserData.email"
                  @click="
                    (modifyCourseDialogVisible = true),
                    (currentlyModifyingCourse = {
                      ...slotProp.data
                    })
                    ">
                  <i class="fa-solid fa-pen-to-square"></i>
                </button>
              </template>
            </Column>
            <Column :header="messages.pages.courseManagementPage.deleteText">
              <template #body="slotProp">
                <button type="button" class="btn btn-danger" @click="deleteCourse(slotProp.data.id)">
                  <i class="fa-solid fa-trash"></i>
                </button>
              </template>
            </Column>
          </DataTable>
        </div>
      </div>
    </div>
  </BaseLayout>
</template>

<script>
import BaseLayout from "@layouts/BaseLayout.vue";
import Toolbar from "primevue/toolbar";
import FileUpload from "primevue/fileupload";
import Button from "primevue/button";
import DataTable from "primevue/datatable";
import Column from "primevue/column";
import RadioButton from "primevue/radiobutton";
import InputText from "primevue/inputtext";
import BaseToast from "@components/BaseToast.vue";
import BaseSpinner from "@components/BaseSpinner.vue";
import BaseDialog from "@components/BaseDialog.vue";
import BaseConfirmDialog from "@components/BaseConfirmDialog.vue";
import { http } from "@utils/http";
import { mapActions, mapState } from "pinia";
import { userStore } from "@stores/UserStore";
import { FilterMatchMode } from "primevue/api";
import { themeStore } from "@stores/ThemeStore.mjs";
import { courseStore } from "@stores/CourseStore.mjs";
import { languageStore } from "@stores/LanguageStore.mjs";
import MultiSelect from "primevue/multiselect";
import { groupStore } from "@stores/GroupStore.mjs";

export default {
  components: {
    BaseLayout,
    Toolbar,
    FileUpload,
    Button,
    DataTable,
    Column,
    RadioButton,
    InputText,
    BaseToast,
    RadioButton,
    MultiSelect,
    BaseSpinner,
    BaseConfirmDialog,
    BaseDialog
  },
  data() {
    return {
      filters: {
        name: { value: null, matchMode: FilterMatchMode.STARTS_WITH },
      },
      selectedCourses: [],
      addCourseDialogVisible: false,
      modifyCourseDialogVisible: false,
      currentlyModifyingCourse: [],
      loading: true,
    };
  },
  computed: {
    ...mapState(userStore, ["token", "currentUserData"]),

    ...mapState(themeStore, ["isDarkMode"]),
    ...mapState(courseStore, ["courses"]),
    ...mapState(languageStore, ["messages"]),
    ...mapState(groupStore, ["groups"]),
    groupsNoUselessData() {
      let returnGroups = [];
      for (const group of this.groups) {
        let groupData = {
          id: group.id,
          name: group.name,
          users: []
        }
        for (const user of group.users) {
          groupData.users.push({
            id: user.id,
            name: user.name,
            email: user.email,
            is_admin: user.is_admin,
            member: {
              ...user.member
            }
          })
        }
        returnGroups.push(groupData)
      }
      return returnGroups;
    }
  },
  methods: {
    ...mapActions(groupStore, ["getGroups"]),
    ...mapActions(courseStore, [
      "getCourses",
      "postCourse",
      "destroyCourse",
      "putCourse",
    ]),
    handleFileChange(e) {
      const file = e.target.files[0];
      if (file) {
        const reader = new FileReader();
        reader.onload = (e) => {
          this.currentlyModifyingCourse.image = e.target.result;
        };
        reader.readAsDataURL(file);
      }
    },
    async postCourses(data) {
      try {
        const base64Image = this.currentlyModifyingCourse.image.split(",")[1];

        const formData = {
          name: data.name,
          image: base64Image
        };

        await this.postCourse(formData);

        let toast = {
          severity: "success",
          detail:
            this.messages.pages.courseManagementPage.toastMessages
              .successfullyCreatedCourse,
          life: 3000,
        };
        if (!this.isDarkMode) {
          toast.styleClass = "bg-success text-white";
        }
        else {
          toast.styleClass = "toast-success text-white";
        }

        this.$toast.add(toast);
        await this.getCourses();
      } catch (error) {
        console.log(error);
        let toast = {
          severity: "error",
          detail:
            this.messages.pages.courseManagementPage.toastMessages
              .failedToCreateCourse,
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
      this.addCourseDialogVisible = false;
    },
    selectAllCourses() {
      if (this.courses.length === this.selectedCourses.length) {
        this.selectedCourses = [];
      } else {
        this.selectedCourses = [...this.courses];
      }
    },
    deleteCourse(userId) {
      this.$confirm.require({
        message: this.messages.pages.courseManagementPage.confirmDialogs.message,
        icon: 'pi pi-exclamation-triangle',
        rejectClass: 'btn btn-danger text-white',
        acceptClass: 'btn btn-success text-white',
        rejectLabel: this.messages.pages.courseManagementPage.confirmDialogs.rejectLabel,
        acceptLabel: this.messages.pages.courseManagementPage.confirmDialogs.acceptLabel,
        accept: async () => {
          try {
            await this.destroyCourse(userId);

            let toast = {
              severity: "success",
              detail:
                this.messages.pages.courseManagementPage.toastMessages
                  .successfullyDeletedCourse,
              life: 3000,
            };
            if (!this.isDarkMode) {
              toast.styleClass = "bg-success text-white";
            }
            else {
              toast.styleClass = "toast-success text-white";
            }

            this.$toast.add(toast);
            await this.getCourses();
          } catch (error) {

            let toast = {
              severity: "error",
              detail:
                this.messages.pages.courseManagementPage.toastMessages
                  .failedToDeleteCourse,
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
        }
      });
    },
    async deleteMultipleCourses() {
      this.$confirm.require({
        message: this.messages.pages.courseManagementPage.confirmDialogs.message,
        icon: 'pi pi-exclamation-triangle',
        rejectClass: 'btn btn-danger text-white',
        acceptClass: 'btn btn-success text-white',
        rejectLabel: this.messages.pages.courseManagementPage.confirmDialogs.rejectLabel,
        acceptLabel: this.messages.pages.courseManagementPage.confirmDialogs.acceptLabel,
        accept: async () => {
          try {
            let courseIds = this.selectedCourses.map((x) => x.id);
            await http.post(
              "/courses/delete",
              { courseIds: courseIds },
              {
                headers: {
                  Authorization: `Bearer ${this.token}`,
                },
              }
            );
            this.selectedCourses = [];

            let toast = {
              severity: "success",
              detail:
                this.messages.pages.courseManagementPage.toastMessages
                  .successfullyDeletedMultipleCourses,
              life: 3000,
            };
            if (!this.isDarkMode) {
              toast.styleClass = "bg-success text-white";
            }
            else {
              toast.styleClass = "toast-success text-white";
            }

            this.$toast.add(toast);
            await this.getCourses();
          } catch (error) {

            let toast = {
              severity: "error",
              detail:
                this.messages.pages.courseManagementPage.toastMessages
                  .failedToDeleteMultipleCourses,
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
        }
      });
    },
    async updateCourse(data) {
      try {
        const base64Image = this.currentlyModifyingCourse?.image?.split(",")[1];

        const group_ids = this.currentlyModifyingCourse?.groups?.map(
          (group) => group.id
        );

        const formData = {
          name: data.name,
          image: base64Image,
        };

        await this.putCourse(data.id, formData);

        await http.post(
          `/courses/${this.currentlyModifyingCourse.id}/groups`,
          {
            group_ids: group_ids === undefined ? [] : group_ids,
          },
          {
            headers: {
              Authorization: `Bearer ${this.token}`,
            },
          }
        );
        this.modifyCourseDialogVisible = false;

        let toast = {
          severity: "success",
          detail:
            this.messages.pages.courseManagementPage.toastMessages
              .successfullyUpdatedCourse,
          life: 3000,
        };
        if (!this.isDarkMode) {
          toast.styleClass = "bg-success text-white";
        }
        else {
          toast.styleClass = "toast-success text-white";
        }

        this.$toast.add(toast);
        await this.getCourses();
      } catch (error) {
        let toast = {
          severity: "error",
          detail:
            this.messages.pages.courseManagementPage.toastMessages
              .failedToUpdateCourse,
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
    exportCSV() {
      this.$refs.dt.exportCSV();
    },
  },
  async mounted() {
    await this.getCourses();
    await this.getGroups();
    this.loading = false;
  },
};
</script>

<style>
span.formkit-no-files {
  display: none;
}

div[data-pc-section="box"] {
  display: none;
}
</style>
