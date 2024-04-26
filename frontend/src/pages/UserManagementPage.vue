<template>
  <BaseLayout>
    <BaseSpinner :loading="loading" />

    <div v-if="!loading">
      <h1 class="text-center my-3">
        {{ messages.pages.userManagementPage.title }}
      </h1>

      <BaseConfirmDialog />

      <Toast :pt="{
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
    }" />
      <Dialog v-if="addUserDialogVisible" v-model:visible="addUserDialogVisible" modal
        :header="messages.pages.userManagementPage.newUserDialog.title" :style="{ width: '25rem' }" :pt="{
      root: {
        class: 'modal-dialog p-3 rounded shadow border',
      },
      header: {
        class: 'd-flex justify-content-between align-items-center pb-2',
      },
      title: {
        class: 'modal-title fw-bold',
      },
      closeButton: {
        class: 'btn btn-outline-dark btn-sm',
      },
      closeButtonIcon: {
        class: 'fa-solid fa-x',
      },
      transition: {
        name: 'slide-fade',
      },
    }">
        <FormKit type="form" :actions="false" @submit="postUser" :incomplete-message="messages.pages.userManagementPage.newUserDialog.validationMessages
      .matchAllValidationMessage
      ">
          <FormKit type="text" name="name" :label="messages.pages.userManagementPage.newUserDialog.nameLabel"
            validation="required|length:0,255" :validation-messages="{
      required:
        messages.pages.userManagementPage.newUserDialog
          .validationMessages.nameRequired,
      length:
        messages.pages.userManagementPage.newUserDialog
          .validationMessages.nameLength,
    }" :classes="{
      input: {
        'mb-1': true,
        'form-control': true,
      },
    }" />
          <FormKit type="email" name="email" :label="messages.pages.userManagementPage.newUserDialog.emailLabel"
            validation="required|email" :validation-messages="{
      required:
        messages.pages.userManagementPage.newUserDialog
          .validationMessages.emailRequired,
      email:
        messages.pages.userManagementPage.newUserDialog
          .validationMessages.validEmail,
    }" :classes="{
      input: {
        'mb-1': true,
        'form-control': true,
      },
    }" />
          <FormKit type="password" name="password" :label="messages.pages.userManagementPage.newUserDialog.passwordLabel
      " validation="required|length:8,255" :validation-messages="{
      required:
        messages.pages.userManagementPage.newUserDialog
          .validationMessages.passwordRequired,
      length:
        messages.pages.userManagementPage.newUserDialog
          .validationMessages.passwordLength,
    }" :classes="{
      input: {
        'mb-1': true,
        'form-control': true,
      },
    }" />
          <FormKit type="password" name="password_confirm" :label="messages.pages.userManagementPage.newUserDialog
      .confirmPasswordLabel
      " validation="required|length:8,255|confirm" :validation-messages="{
      required:
        messages.pages.userManagementPage.newUserDialog
          .validationMessages.confirmPasswordRequired,
      length:
        messages.pages.userManagementPage.newUserDialog
          .validationMessages.confirmPasswordLength,
      confirm:
        messages.pages.userManagementPage.newUserDialog
          .validationMessages.confirmPasswordConfirm,
    }" :classes="{
      input: {
        'mb-1': true,
        'form-control': true,
      },
    }" />
          <div class="d-flex justify-content-end mt-2 mb-3">
            <Button type="button" :label="messages.pages.userManagementPage.newUserDialog.cancelButton
      " class="btn btn-outline-danger mx-1" @click="addUserDialogVisible = false"></Button>
            <FormKit type="submit" :label="messages.pages.userManagementPage.newUserDialog.saveButton
      " id="addUserButton" :classes="{
      input: {
        btn: true,
        'btn-success': true,
        'w-auto': true,
      },
    }" />
          </div>
        </FormKit>
      </Dialog>
      <Dialog v-if="modifyUserDialogVisible" v-model:visible="modifyUserDialogVisible" modal :header="messages.pages.userManagementPage.editUserDialog.title +
      ': ' +
      currentlyModifyingUser.name
      " :style="{ width: '25rem' }" :pt="{
      root: {
        class: 'modal-dialog p-3 rounded shadow border',
      },
      header: {
        class: 'd-flex justify-content-between align-items-center pb-2',
      },
      title: {
        class: 'modal-title fw-bold',
      },
      closeButton: {
        class: 'btn btn-outline-dark btn-sm',
      },
      closeButtonIcon: {
        class: 'fa-solid fa-x',
      },
      transition: {
        name: 'slide-fade',
      },
    }">
        <FormKit type="form" :actions="false" @submit="updateUser" :value="currentlyModifyingUser" :incomplete-message="messages.pages.userManagementPage.editUserDialog.validationMessages
      .matchAllValidationMessage
      ">
          <FormKit type="text" name="name" :label="messages.pages.userManagementPage.editUserDialog.nameLabel"
            validation="required|length:0,255" :validation-messages="{
      required:
        messages.pages.userManagementPage.editUserDialog
          .validationMessages.nameRequired,
      length:
        messages.pages.userManagementPage.editUserDialog
          .validationMessages.nameLength,
    }" :classes="{
      input: {
        'mb-1': true,
        'form-control': true,
      },
    }" />
          <FormKit type="email" name="email" :label="messages.pages.userManagementPage.editUserDialog.emailLabel"
            validation="required|email" :validation-messages="{
      required:
        messages.pages.userManagementPage.editUserDialog
          .validationMessages.emailRequired,
      email:
        messages.pages.userManagementPage.editUserDialog
          .validationMessages.validEmail,
    }" :classes="{
      input: {
        'mb-1': true,
        'form-control': true,
      },
    }" />
          <FormKit type="password" name="password" :label="messages.pages.userManagementPage.editUserDialog.passwordLabel
      " validation="length:8,255" :validation-messages="{
      length:
        messages.pages.userManagementPage.editUserDialog
          .validationMessages.passwordLength,
    }" :classes="{
      input: {
        'mb-1': true,
        'form-control': true,
      },
    }" />
          <FormKit type="password" name="password_confirm" :label="messages.pages.userManagementPage.editUserDialog
      .confirmPasswordLabel
      " validation="length:8,255|confirm" :validation-messages="{
      length:
        messages.pages.userManagementPage.editUserDialog
          .validationMessages.confirmPasswordLength,
      confirm:
        messages.pages.userManagementPage.editUserDialog
          .validationMessages.confirmPasswordConfirm,
    }" :classes="{
      input: {
        'mb-1': true,
        'form-control': true,
      },
    }" />
          <FormKit type="checkbox" label="Admin-E" name="is_admin" :classes="{
      input: {
        'mb-1': true,
        'form-check-input': true,
        'me-2': true,
      },
    }" />
          <div class="d-flex justify-content-end mt-2 mb-3">
            <Button type="button" label="Mégse" class="btn btn-outline-danger mx-1"
              @click="modifyUserDialogVisible = false"></Button>
            <FormKit type="submit" label="Mentés" :classes="{
      input: {
        btn: true,
        'btn-success': true,
        'w-auto': true,
      },
    }" />
          </div>
        </FormKit>
      </Dialog>
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
              <Button :label="messages.pages.userManagementPage.newUser" id="newUser" icon="pi pi-plus"
                class="mr-2 btn btn-success text-white me-1 mt-2 ms-2" @click="addUserDialogVisible = true" />
              <Button :label="messages.pages.userManagementPage.deleteUser" icon="pi pi-trash"
                class="btn btn-danger text-white mt-2" :disabled="selectedUsers.length === 0"
                @click="deleteMultipleUsers" />
            </template>
            <template #end>
              <Button :label="messages.pages.userManagementPage.exportButton" icon="pi pi-upload"
                class="btn btn-warning text-white mt-2 me-2" @click="exportCSV($event)" />
            </template>
          </Toolbar>
          <DataTable exportFilename="users" ref="dt" :value="users" tableStyle="min-width: 50rem" sortField="id"
            :sortOrder="1" v-model:filters="filters" filterDisplay="row" v-model:selection="selectedUsers"
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
                    " @click="selectAllUsers">
                    <i class="fa-solid fa-x text-danger" v-if="selectedUsers.length == 0"></i>
                    <i class="fa-solid fa-check text-success" v-else></i>
                  </button>
                </div>
              </template>
              <template #body="slotProp">
                <div class="d-flex justify-content-center">
                  <i class="fa-solid fa-check text-success" v-if="selectedUsers.findIndex((x) => x == slotProp.data) != -1
      "></i>
                  <i class="fa-solid fa-x text-danger" v-else></i>
                </div>
              </template>
            </Column>
            <Column field="id" :header="messages.pages.userManagementPage.idText" sortable></Column>
            <Column field="name" :header="messages.pages.userManagementPage.nameText" sortable :pt="{
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
                  :placeholder="messages.pages.userManagementPage.namePlaceholder
      " />
              </template>
            </Column>
            <Column field="email" header="Email" sortable :pt="{
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
                  placeholder="Email..." />
              </template>
            </Column>
            <Column field="is_admin" :header="messages.pages.userManagementPage.adminText" sortable>
              <template #body="slotProp">
                <div class="d-flex justify-content-center">
                  <RadioButton v-model="checked" :value="slotProp.data.is_admin == 1" ű class="radioButton" />
                </div>
              </template>
            </Column>
            <Column :header="messages.pages.userManagementPage.modifyText">
              <template #body="slotProp">
                <button type="button" class="btn btn-warning" v-if="slotProp.data.email != currentUserData.email" @click="
                  (modifyUserDialogVisible = true),
                  (currentlyModifyingUser = {
                    ...slotProp.data,
                    password: '',
                  })
                  ">
                  <i class="fa-solid fa-pen-to-square"></i>
                </button>
              </template>
            </Column>
            <Column :header="messages.pages.userManagementPage.deleteText">
              <template #body="slotProp">
                <button type="button" class="btn btn-danger" v-if="slotProp.data.email != currentUserData.email" @click="deleteUser(slotProp.data.id)">
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
import Button from "primevue/button";
import DataTable from "primevue/datatable";
import Column from "primevue/column";
import RadioButton from "primevue/radiobutton";
import InputText from "primevue/inputtext";
import Dialog from "primevue/dialog";
import Toast from "primevue/toast";
import BaseSpinner from "@components/BaseSpinner.vue";
import { http } from "@utils/http";
import { mapState } from "pinia";
import { userStore } from "@stores/UserStore";
import { FilterMatchMode } from "primevue/api";
import { themeStore } from "@stores/ThemeStore.mjs";
import { languageStore } from "@stores/LanguageStore.mjs";
import BaseConfirmDialog from "@components/BaseConfirmDialog.vue";

export default {
  components: {
    BaseLayout,
    Toolbar,
    Button,
    DataTable,
    Column,
    RadioButton,
    InputText,
    Dialog,
    Toast,
    RadioButton,
    BaseSpinner,
    BaseConfirmDialog
  },
  data() {
    return {
      users: [],
      filters: {
        name: { value: null, matchMode: FilterMatchMode.STARTS_WITH },
        email: { value: null, matchMode: FilterMatchMode.CONTAINS },
      },
      selectedUsers: [],
      addUserDialogVisible: false,
      modifyUserDialogVisible: false,
      currentlyModifyingUser: [],
      loading: true,
      checked: true,
    };
  },
  computed: {
    ...mapState(userStore, ["token", "currentUserData"]),
    ...mapState(themeStore, ["isDarkMode"]),
    ...mapState(languageStore, ["messages"]),
  },
  methods: {
    async getUsers() {
      const response = await http.get("/users", {
        headers: {
          Authorization: `Bearer ${this.token}`,
        },
      });
      this.users = response.data.data;
    },
    async postUser(data) {
      try {
        data.password_confirmation = data.password_confirm;
        const response = await http.post("/users/register", data, {
          headers: {
            Authorization: `Bearer ${this.token}`,
          },
        });

        let toast = {
          severity: "success",
          detail:
            this.messages.pages.userManagementPage.toastMessages
              .successfullyCreatedUser,
          life: 3000,
        };
        if (!this.isDarkMode) {
          toast.styleClass = "bg-success text-white";
        }
        else {
          toast.styleClass = "toast-success text-white";
        }

        this.$toast.add(toast);
        await this.getUsers();
      } catch (error) {
        let toast = {
          severity: "error",
          detail:
            this.messages.pages.userManagementPage.toastMessages
              .failedToCreateUser,
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

      this.addUserDialogVisible = false;
    },
    selectAllUsers() {
      if (this.users.length === this.selectedUsers.length) {
        this.selectedUsers = [];
      } else {
        this.selectedUsers = [...this.users];
      }
    },
    async deleteUser(userId) {
      this.$confirm.require({
        message: this.messages.pages.userManagementPage.confirmDialogs.message,
        icon: 'pi pi-exclamation-triangle',
        rejectClass: 'btn btn-danger',
        acceptClass: 'btn btn-success ',
        rejectLabel: this.messages.pages.userManagementPage.confirmDialogs.rejectLabel,
        acceptLabel: this.messages.pages.userManagementPage.confirmDialogs.acceptLabel,
        accept: async () => {
          try {
            await http.delete(`/users/${userId}`, {
              headers: {
                Authorization: `Bearer ${this.token}`,
              },
            });

            let toast = {
              severity: "success",
              detail:
                this.messages.pages.userManagementPage.toastMessages
                  .successfullyDeletedUser,
              life: 3000,
            };
            if (!this.isDarkMode) {
              toast.styleClass = "bg-success text-white";
            }
            else {
              toast.styleClass = "toast-success text-white";
            }

            this.$toast.add(toast);
            await this.getUsers();
          } catch (error) {
            let toast = {
              severity: "error",
              detail:
                this.messages.pages.userManagementPage.toastMessages
                  .failedToDeleteUser,
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
    async deleteMultipleUsers() {
      this.$confirm.require({
        message: this.messages.pages.userManagementPage.confirmDialogs.message,
        icon: 'pi pi-exclamation-triangle',
        rejectClass: 'btn btn-danger',
        acceptClass: 'btn btn-success ',
        rejectLabel: this.messages.pages.userManagementPage.confirmDialogs.rejectLabel,
        acceptLabel: this.messages.pages.userManagementPage.confirmDialogs.acceptLabel,
        accept: async () => {
          try {
            let userIds = this.selectedUsers.map((x) => x.id);
            await http.post(
              "/users/delete",
              { userIds: userIds },
              {
                headers: {
                  Authorization: `Bearer ${this.token}`,
                },
              }
            );

            let toast = {
              severity: "success",
              detail:
                this.messages.pages.userManagementPage.toastMessages
                  .successfullyDeletedMultipleUsers,
              life: 3000,
            };
            if (!this.isDarkMode) {
              toast.styleClass = "bg-success text-white";
            }
            else {
              toast.styleClass = "toast-success text-white";
            }

            this.$toast.add(toast);
            await this.getUsers();
          } catch (error) {

            let toast = {
              severity: "error",
              detail:
                this.messages.pages.userManagementPage.toastMessages
                  .failedToDeleteMultipleUsers,
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
    async updateUser(data) {
      try {
        data.password_confirmation = data.password_confirm;
        if (data.password == "") {
          delete data.password;
          delete data.password_confirmation;
        }
        await http.put(`/users/${data.id}`, data, {
          headers: {
            Authorization: `Bearer ${this.token}`,
          },
        });
        this.modifyUserDialogVisible = false;

        let toast = {
          severity: "success",
          detail:
            this.messages.pages.userManagementPage.toastMessages
              .successfullyUpdatedUser,
          life: 3000,
        };
        if (!this.isDarkMode) {
          toast.styleClass = "bg-success text-white";
        }
        else {
          toast.styleClass = "toast-success text-white";
        }

        this.$toast.add(toast);
        await this.getUsers();
      } catch (error) {

        let toast = {
          severity: "error",
          detail:
            this.messages.pages.userManagementPage.toastMessages
              .failedToUpdateUser,
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
    await this.getUsers();
    this.loading = false;
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
