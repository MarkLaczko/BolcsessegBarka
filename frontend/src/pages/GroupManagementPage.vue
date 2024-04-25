<template>
  <BaseLayout>
    <BaseSpinner :loading="loading" />

    <div v-if="!loading">
      <h1 class="text-center my-3">
        {{ messages.pages.groupManagementPage.title }}
      </h1>

      <BaseConfirmDialog />
      <BaseToast />

      <BaseDialog v-if="addGroupDialogVisible" :visible="addGroupDialogVisible"
        :header="messages.pages.groupManagementPage.newGroupDialog.title" :width="'25rem'">
        <FormKit type="form" :actions="false" @submit="addGroup" :incomplete-message="messages.pages.groupManagementPage.newGroupDialog
      .matchAllValidationMessage
      ">
          <FormKit type="text" name="name" :label="messages.pages.groupManagementPage.newGroupDialog.nameLabel"
            validation="required|length:0,100" :validation-messages="{
      required:
        messages.pages.groupManagementPage.newGroupDialog.nameRequired,
      length:
        messages.pages.groupManagementPage.newGroupDialog.nameLength,
    }" :classes="{
      input: {
        'mb-1': true,
        'form-control': true,
      },
    }" />
          <div class="d-flex justify-content-end mt-2 mb-3">
            <Button type="button" :label="messages.pages.groupManagementPage.newGroupDialog.cancelButton
      " class="btn btn-outline-danger mx-1" @click="addGroupDialogVisible = false"></Button>
            <FormKit type="submit" :label="messages.pages.groupManagementPage.newGroupDialog.saveButton
      " id="addGroupButton" :classes="{
      input: {
        btn: true,
        'btn-success': true,
        'w-auto': true,
      },
    }" />
          </div>
        </FormKit>
      </BaseDialog>

      <BaseDialog v-if="modifyGroupDialogVisible" :visible="modifyGroupDialogVisible" :header="messages.pages.groupManagementPage.editGroupDialog.title +
      ' ' +
      currentlyModifyingGroup.name
      " :width="'60rem'">
        <FormKit type="form" :actions="false" @submit="sendUpdateGroup" :value="currentlyModifyingGroup"
          :incomplete-message="messages.pages.groupManagementPage.editGroupDialog
      .matchAllValidationMessage
      ">
          <FormKit type="text" name="name" :label="messages.pages.groupManagementPage.editGroupDialog.nameLabel
      " validation="required|length:0,100" :validation-messages="{
      required:
        messages.pages.groupManagementPage.editGroupDialog
          .validationMessages.nameRequired,
      length:
        messages.pages.groupManagementPage.editGroupDialog
          .validationMessages.nameLength,
    }" :classes="{
      input: {
        'mb-1': true,
        'form-control': true,
      },
    }" />

          <DataTable :value="users" tableStyle="max-width: 90wv" sortField="name" :sortOrder="1"
            v-model:filters="userFilters" filterDisplay="row" :pt="{
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
                    " @click="selectAllGroups">
                    <i class="fa-solid fa-x text-danger" v-if="currentlyModifyingGroup.selectedUsers.length == 0"></i>
                    <i class="fa-solid fa-check text-success" v-else></i>
                  </button>
                </div>
              </template>
              <template #body="slotProp">
                <div class="d-flex justify-content-center">
                  <button type="button" class="btn btn-light" style="
                      --bs-btn-padding-y: 0.25rem;
                      --bs-btn-padding-x: 0.5rem;
                      --bs-btn-font-size: 0.75rem;
                      width: 28px;
                    " v-if="currentlyModifyingGroup.selectedUsers.findIndex(
      (x) => x.id == slotProp.data.id
    ) != -1
      " @click="unSelectUser(slotProp.data)">
                    <i class="fa-solid fa-check text-success"></i>
                  </button>
                  <button type="button" class="btn btn-light" style="
                      --bs-btn-padding-y: 0.25rem;
                      --bs-btn-padding-x: 0.5rem;
                      --bs-btn-font-size: 0.75rem;
                      width: 28px;
                    " v-else @click="selectUser(slotProp.data)">
                    <i class="fa-solid fa-x text-danger"></i>
                  </button>
                </div>
              </template>
            </Column>
            <Column field="id" :header="messages.pages.groupManagementPage.editGroupDialog.idLabel
      " sortable></Column>
            <Column field="name" :header="messages.pages.groupManagementPage.editGroupDialog.nameLabel
      " sortable :pt="{
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
                  :placeholder="messages.pages.groupManagementPage.editGroupDialog.namePlaceholder" />
              </template>
            </Column>
            <Column field="email" :header="messages.pages.groupManagementPage.editGroupDialog.emailLabel" sortable :pt="{
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
                  :placeholder="messages.pages.groupManagementPage.editGroupDialog
      .emailPlaceholder
      " />
              </template>
            </Column>
            <Column field="permission" :header="messages.pages.groupManagementPage.editGroupDialog
      .permissionLabel
      " :pt="{
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
              <template #body="slotProp">
                <select v-if="currentlyModifyingGroup.selectedUsers.findIndex(
      (x) => x.id == slotProp.data.id
    ) != -1
      " v-model="currentlyModifyingGroup.selectedUsers[
      currentlyModifyingGroup.selectedUsers.findIndex(
        (x) => x.id == slotProp.data.id
      )
    ].permission
      " class="form-select">
                  <option value="Tanár">Tanár</option>
                  <option value="Tanuló">Tanuló</option>
                </select>
                <select v-else class="form-select" disabled></select>
              </template>
            </Column>
          </DataTable>
          <div class="d-flex justify-content-end mt-2 mb-3">
            <Button type="button" :label="messages.pages.groupManagementPage.editGroupDialog.cancelButton
      " class="btn btn-outline-danger mx-1" @click="closeModifyWindow"></Button>
            <FormKit type="submit" :label="messages.pages.groupManagementPage.editGroupDialog.saveButton
      " id="modifyGroupButton" :classes="{
      input: {
        btn: true,
        'btn-success': true,
        'w-auto': true,
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
              <Button :label="messages.pages.groupManagementPage.newGroup" id="newGroup" icon="pi pi-plus"
                class="mr-2 btn btn-success text-white me-1 mt-2 ms-2" @click="addGroupDialogVisible = true" />
              <Button :label="messages.pages.groupManagementPage.deleteGroup" id="deleteGroups" icon="pi pi-trash"
                class="btn btn-danger text-white mt-2" :disabled="selectedGroups.length === 0" @click="confirmBulkDelete" />
            </template>
            <template #end>
              <Button :label="messages.pages.groupManagementPage.exportButton" icon="pi pi-upload"
                class="btn btn-warning text-white mt-2 me-2" @click="exportCSV($event)" />
            </template>
          </Toolbar>
          <DataTable exportFilename="groups" ref="dt" :value="groups" tableStyle="min-width: 50wv" sortField="id"
            :sortOrder="1" v-model:filters="filters" filterDisplay="row" v-model:selection="selectedGroups"
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
                    " @click="selectAllGroups">
                    <i class="fa-solid fa-x text-danger" v-if="selectedGroups.length == 0"></i>
                    <i class="fa-solid fa-check text-success" v-else></i>
                  </button>
                </div>
              </template>
              <template #body="slotProp">
                <div class="d-flex justify-content-center">
                  <i class="fa-solid fa-check text-success" v-if="selectedGroups.findIndex((x) => x == slotProp.data) != -1
      "></i>
                  <i class="fa-solid fa-x text-danger" v-else></i>
                </div>
              </template>
            </Column>
            <Column field="id" :header="messages.pages.groupManagementPage.editGroupDialog.idLabel
      " sortable></Column>
            <Column field="name" :header="messages.pages.groupManagementPage.editGroupDialog.nameLabel
      " sortable :pt="{
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
                  :placeholder="messages.pages.groupManagementPage.editGroupDialog
      .namePlaceholder
      " />
              </template>
            </Column>
            <Column field="users.length" :header="messages.pages.groupManagementPage.memberText" sortable>
              <template #body="slotProp">
                {{ slotProp.data.users.length }}
                {{ messages.pages.groupManagementPage.member }}
              </template>
            </Column>
            <Column field="courses.length" :header="messages.pages.groupManagementPage.courseText" sortable>
              <template #body="slotProp">
                {{ slotProp.data.courses.length }}
                {{ messages.pages.groupManagementPage.course }}
              </template>
            </Column>
            <Column :header="messages.pages.groupManagementPage.modifyText">
              <template #body="slotProp">
                <button type="button" class="btn btn-warning modifyGroup" @click="openModifyWindow(slotProp.data)">
                  <i class="fa-solid fa-pen-to-square"></i>
                </button>
              </template>
            </Column>
            <Column :header="messages.pages.groupManagementPage.deleteText">
              <template #body="slotProp">
                <button type="button" class="btn btn-danger deleteGroup" @click="confirmDelete(slotProp.data.id)">
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
import Dropdown from "primevue/dropdown";
import BaseSpinner from "@components/BaseSpinner.vue";
import { http } from "@utils/http";
import { mapActions, mapState } from "pinia";
import { groupStore } from "@stores/GroupStore";
import { FilterMatchMode } from "primevue/api";
import { themeStore } from "@stores/ThemeStore.mjs";
import { userStore } from "@stores/UserStore";
import { permissionStore } from "@stores/PermissionStore.mjs";
import { languageStore } from "@stores/LanguageStore.mjs";
import BaseDialog from "@components/BaseDialog.vue";
import BaseToast from "@components/BaseToast.vue";
import BaseConfirmDialog from "@components/BaseConfirmDialog.vue";

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
    RadioButton,
    Dropdown,
    BaseDialog,
    BaseSpinner,
    BaseToast,
    BaseConfirmDialog,
  },
  data() {
    return {
      filters: {
        name: { value: null, matchMode: FilterMatchMode.CONTAINS },
      },
      userFilters: {
        name: { value: null, matchMode: FilterMatchMode.STARTS_WITH },
        email: { value: null, matchMode: FilterMatchMode.CONTAINS },
      },
      selectedGroups: [],
      addGroupDialogVisible: false,
      modifyGroupDialogVisible: false,
      currentlyModifyingGroup: [],
      checked: true,
      users: [],
      loading: true,
      toDelete: null,
    };
  },
  computed: {
    ...mapState(userStore, ["token"]),
    ...mapState(groupStore, ["groups"]),
    ...mapState(themeStore, ["isDarkMode"]),
    ...mapState(permissionStore, ["permissions"]),
    ...mapState(languageStore, ["messages"]),
  },
  methods: {
    ...mapActions(groupStore, [
      "getGroups",
      "getGroup",
      "postGroup",
      "updateGroup",
      "deleteGroup",
      "bulkDeleteGroups",
    ]),
    ...mapActions(permissionStore, ["getPermissions"]),
    selectAllGroups() {
      if (this.groups.length === this.selectedGroups.length) {
        this.selectedGroups = [];
      } else {
        this.selectedGroups = [...this.groups];
      }
    },
    async getUsers() {
      const response = await http.get("/users", {
        headers: {
          Authorization: `Bearer ${this.token}`,
        },
      });
      this.users = response.data.data;
    },
    async deleteMultipleGroups() {
      try {
        await this.bulkDeleteGroups(this.selectedGroups);

        let toast = {
          severity: "success",
          detail:
            this.messages.pages.groupManagementPage.toastMessages
              .successfullyDeletedMultipleGroups,
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
            this.messages.pages.groupManagementPage.toastMessages
              .failedToDeleteMultipleGroups,
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
    async addGroup(data) {
      try {
        await this.postGroup(data);
        let toast = {
          severity: "success",
          detail:
            this.messages.pages.groupManagementPage.toastMessages
              .successfullyCreatedGroup,
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
            this.messages.pages.groupManagementPage.toastMessages
              .failedToCreateGroup,
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
      this.addGroupDialogVisible = false;
    },
    async sendUpdateGroup(data) {
      data.selectedUsers = this.currentlyModifyingGroup.selectedUsers.map(
        (x) => ({ id: x.id, permission: x.permission })
      );
      data.users = data.users.map((x) => x.id);
      try {
        await this.updateGroup(data);

        let toast = {
          severity: "success",
          detail:
            this.messages.pages.groupManagementPage.toastMessages
              .successfullyUpdatedGroup,
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
            this.messages.pages.groupManagementPage.toastMessages
              .failedToUpdateGroup,
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
      this.closeModifyWindow();
    },
    selectUser(data) {
      data.permission = data.permission == null ? "Tanuló" : data.permission;
      this.currentlyModifyingGroup.selectedUsers.push(data);
    },
    unSelectUser(data) {
      const idx = this.currentlyModifyingGroup.selectedUsers.findIndex(
        (x) => x.id == data.id
      );
      this.currentlyModifyingGroup.selectedUsers.splice(idx, 1);
    },
    openModifyWindow(data) {
      this.modifyGroupDialogVisible = true;
      for (let user of data.users) {
        user.permission =
          user.member == null ? "Tanuló" : user.member.permission;
      }
      this.currentlyModifyingGroup = {
        ...data,
        selectedUsers: data.users == null ? {} : data.users,
      };
    },
    addPermissionFieldToGroupUsers(group) {
      for (let user of group.users) {
        user.permission = "Tanuló";
      }
    },
    addPermissionFieldToAllGroups() {
      for (let group of this.groups) {
        this.addPermissionFieldToGroupUsers(group);
      }
    },
    closeModifyWindow() {
      this.modifyGroupDialogVisible = false;
      this.currentlyModifyingGroup = {};
    },
    exportCSV() {
      this.$refs.dt.exportCSV();
    },
    confirmDelete(id) {
      this.$confirm.require({
        message: this.messages.pages.groupManagementPage.confirmDialogs.delete,
        icon: 'pi pi-exclamation-triangle',
        rejectClass: 'btn btn-danger',
        acceptClass: 'btn btn-success ',
        rejectLabel: 'Mégse',
        acceptLabel: 'Törlés',
        accept: () => {
          try {
            this.deleteGroup(id);
            let toast = {
              severity: "success",
              detail:
                this.messages.pages.groupManagementPage.toastMessages
                  .successfullyDeletedGroup,
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
                this.messages.pages.groupManagementPage.toastMessages
                  .failedToDeleteGroup,
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
    confirmBulkDelete() {
      this.$confirm.require({
        message: this.messages.pages.groupManagementPage.confirmDialogs.bulkDelete,
        icon: 'pi pi-exclamation-triangle',
        rejectClass: 'btn btn-danger',
        acceptClass: 'btn btn-success ',
        rejectLabel: 'Mégse',
        acceptLabel: 'Törlés',
        accept: () => {
          this.deleteMultipleGroups();
        }
      });
    },
  },
  async mounted() {
    await this.getGroups();
    await this.getUsers();
    await this.addPermissionFieldToAllGroups();
    this.loading = false;
  },
  exportCSV() {
    this.$refs.dt.exportCSV();
    console.log(this.$refs.dt);
  },
};
</script>

<style>
.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.5s ease;
}

.fade-enter-from,
.fade-leave-to {
  opacity: 0;
}

.slide-fade-enter-active {
  transition: all 0.3s ease-out;
}

.slide-fade-leave-active {
  transition: all 0.8s cubic-bezier(1, 0.5, 0.8, 1);
}

.slide-fade-enter-from,
.slide-fade-leave-to {
  transform: translateY(-20px);
  opacity: 0;
}
</style>
