<template>
  <BaseLayout>
    <div class="d-flex justify-content-center" v-if="loading">
      <img
        src="../assets/images/logo.png"
        alt="logo"
        width="300px"
        class="rotating-pulsating"
      />
    </div>

    <div v-if="!loading">
      <h1 class="text-center my-3">
        {{ messages.pages.groupManagementPage.title }}
      </h1>

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
      <Dialog
        v-if="addGroupDialogVisible"
        v-model:visible="addGroupDialogVisible"
        modal
        :header="messages.pages.groupManagementPage.newGroupDialog.title"
        :style="{ width: '25rem' }"
        :pt="{
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
        }"
      >
        <FormKit
          type="form"
          :actions="false"
          @submit="postGroup"
          :incomplete-message="
            messages.pages.groupManagementPage.newGroupDialog
              .matchAllValidationMessage
          "
        >
          <FormKit
            type="text"
            name="name"
            :label="messages.pages.groupManagementPage.newGroupDialog.nameLabel"
            validation="required|length:0,100"
            :validation-messages="{
              required:
                messages.pages.groupManagementPage.newGroupDialog.nameRequired,
              length:
                messages.pages.groupManagementPage.newGroupDialog.nameLength,
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
              :label="
                messages.pages.groupManagementPage.newGroupDialog.cancelButton
              "
              class="btn btn-outline-danger mx-1"
              @click="addGroupDialogVisible = false"
            ></Button>
            <FormKit
              type="submit"
              :label="
                messages.pages.groupManagementPage.newGroupDialog.saveButton
              "
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
      </Dialog>
      <Dialog
        v-if="modifyGroupDialogVisible"
        v-model:visible="modifyGroupDialogVisible"
        modal
        :header="messages.pages.groupManagementPage.editGroupDialog.title+' '+currentlyModifyingGroup.name"
        :style="{ width: '50rem' }"
        :pt="{
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
        }"
      >
        <FormKit
          type="form"
          :actions="false"
          @submit="sendUpdateGroup"
          :value="currentlyModifyingGroup"
          :incomplete-message="messages.pages.groupManagementPage.editGroupDialog.matchAllValidationMessage"
        >
          <FormKit
            type="text"
            name="name"
            :label="messages.pages.groupManagementPage.editGroupDialog.nameLabel"
            validation="required|length:0,100"
            :validation-messages="{
              required: messages.pages.groupManagementPage.editGroupDialog.validationMessages.nameRequired,
              length:
              messages.pages.groupManagementPage.editGroupDialog.validationMessages.nameLength,
            }"
            :classes="{
              input: {
                'mb-1': true,
                'form-control': true,
              },
            }"
          />

          <DataTable
            :value="users"
            tableStyle="min-width: 40rem"
            sortField="name"
            :sortOrder="1"
            v-model:filters="userFilters"
            filterDisplay="row"
            :pt="{
              table: {
                class: 'table table-responsive align-middle',
              },
            }"
          >
            <Column>
              <template #header>
                <div class="d-flex justify-content-center">
                  <button
                    type="button"
                    class="btn"
                    style="
                      --bs-btn-padding-y: 0.25rem;
                      --bs-btn-padding-x: 0.5rem;
                      --bs-btn-font-size: 0.75rem;
                      width: 28px;
                    "
                    @click="selectAllGroups"
                  >
                    <i
                      class="fa-solid fa-x text-danger"
                      v-if="currentlyModifyingGroup.selectedUsers.length == 0"
                    ></i>
                    <i class="fa-solid fa-check text-success" v-else></i>
                  </button>
                </div>
              </template>
              <template #body="slotProp">
                <div class="d-flex justify-content-center">
                  <button
                    type="button"
                    class="btn btn-light"
                    style="
                      --bs-btn-padding-y: 0.25rem;
                      --bs-btn-padding-x: 0.5rem;
                      --bs-btn-font-size: 0.75rem;
                      width: 28px;
                    "
                    v-if="
                      currentlyModifyingGroup.selectedUsers.findIndex(
                        (x) => x.id == slotProp.data.id
                      ) != -1
                    "
                    @click="unSelectUser(slotProp.data)"
                  >
                    <i class="fa-solid fa-check text-success"></i>
                  </button>
                  <button
                    type="button"
                    class="btn btn-light"
                    style="
                      --bs-btn-padding-y: 0.25rem;
                      --bs-btn-padding-x: 0.5rem;
                      --bs-btn-font-size: 0.75rem;
                      width: 28px;
                    "
                    v-else
                    @click="selectUser(slotProp.data)"
                  >
                    <i class="fa-solid fa-x text-danger"></i>
                  </button>
                </div>
              </template>
            </Column>
            <Column field="id" :header="messages.pages.groupManagementPage.editGroupDialog.idLabel" sortable></Column>
            <Column
              field="name"
              :header="messages.pages.groupManagementPage.editGroupDialog.nameLabel"
              sortable
              :pt="{
                columnfilter: {
                  class: 'd-flex',
                },
                filtermenubutton: {
                  class: 'btn ms-1',
                },
                headerfilterclearbutton: {
                  class: 'btn ms-1',
                },
              }"
            >
              <template #filter="{ filterModel, filterCallback }">
                <InputText
                  v-model="filterModel.value"
                  type="text"
                  @input="filterCallback()"
                  class="form-control"
                  :placeholder="messages.pages.groupManagementPage.editGroupDialog.namePlaceholder"
                />
              </template>
            </Column>
            <Column
              field="email"
              :header="messages.pages.groupManagementPage.editGroupDialog.emailLabel"
              sortable
              :pt="{
                columnfilter: {
                  class: 'd-flex',
                },
                filtermenubutton: {
                  class: 'btn ms-1',
                },
                headerfilterclearbutton: {
                  class: 'btn ms-1',
                },
              }"
            >
              <template #filter="{ filterModel, filterCallback }">
                <InputText
                  v-model="filterModel.value"
                  type="text"
                  @input="filterCallback()"
                  class="form-control"
                  :placeholder="messages.pages.groupManagementPage.editGroupDialog.emailPlaceholder"
                />
              </template>
            </Column>
            <Column
              field="permission"
              :header="messages.pages.groupManagementPage.editGroupDialog.permissionLabel"
              :pt="{
                columnfilter: {
                  class: 'd-flex',
                },
                filtermenubutton: {
                  class: 'btn ms-1',
                },
                headerfilterclearbutton: {
                  class: 'btn ms-1',
                },
              }"
            >
              <template #body="slotProp">
                <select
                  v-if="
                    currentlyModifyingGroup.selectedUsers.findIndex(
                      (x) => x.id == slotProp.data.id
                    ) != -1
                  "
                  v-model="
                    currentlyModifyingGroup.selectedUsers[
                      currentlyModifyingGroup.selectedUsers.findIndex(
                        (x) => x.id == slotProp.data.id
                      )
                    ].permission
                  "
                  class="form-select"
                >
                  <option value="Tanár">Tanár</option>
                  <option value="Tanuló">Tanuló</option>
                </select>
                <select v-else class="form-select" disabled></select>
              </template>
            </Column>
          </DataTable>
          <div class="d-flex justify-content-end mt-2 mb-3">
            <Button
              type="button"
              :label="messages.pages.groupManagementPage.editGroupDialog.cancelButton"
              class="btn btn-outline-danger mx-1"
              @click="closeModifyWindow"
            ></Button>
            <FormKit
              type="submit"
              :label="messages.pages.groupManagementPage.editGroupDialog.saveButton"
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
      </Dialog>
      <div>
        <div class="card darkTheme">
          <Toolbar
            :pt="{
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
            }"
          >
            <template #start>
              <Button
                :label="messages.pages.groupManagementPage.newGroup"
                id="newUser"
                icon="pi pi-plus"
                class="mr-2 btn btn-success text-white me-1 mt-2 ms-2"
                @click="addGroupDialogVisible = true"
              />
              <Button
                :label="messages.pages.groupManagementPage.deleteGroup"
                icon="pi pi-trash"
                class="btn btn-danger text-white mt-2"
                @click="deleteMultipleGroups"
              />
            </template>
            <template #end>
              <Button
                :label="messages.pages.groupManagementPage.exportButton"
                icon="pi pi-upload"
                class="btn btn-warning text-white mt-2 me-2"
                @click="exportCSV($event)"
              />
            </template>
          </Toolbar>
          <DataTable
            exportFilename="groups"
            ref="dt"
            :value="groups"
            tableStyle="min-width: 50rem"
            sortField="id"
            :sortOrder="1"
            v-model:filters="filters"
            filterDisplay="row"
            v-model:selection="selectedGroups"
            selectionMode="multiple"
            dataKey="id"
            :metaKeySelection="false"
            :pt="{
              table: {
                class: 'table table-responsive align-middle',
              },
            }"
          >
            <Column>
              <template #header>
                <div class="d-flex justify-content-center">
                  <button
                    type="button"
                    class="btn"
                    style="
                      --bs-btn-padding-y: 0.25rem;
                      --bs-btn-padding-x: 0.5rem;
                      --bs-btn-font-size: 0.75rem;
                      width: 28px;
                    "
                    @click="selectAllGroups"
                  >
                    <i
                      class="fa-solid fa-x text-danger"
                      v-if="selectedGroups.length == 0"
                    ></i>
                    <i class="fa-solid fa-check text-success" v-else></i>
                  </button>
                </div>
              </template>
              <template #body="slotProp">
                <div class="d-flex justify-content-center">
                  <i
                    class="fa-solid fa-check text-success"
                    v-if="
                      selectedGroups.findIndex((x) => x == slotProp.data) != -1
                    "
                  ></i>
                  <i class="fa-solid fa-x text-danger" v-else></i>
                </div>
              </template>
            </Column>
            <Column field="id" :header="messages.pages.groupManagementPage.editGroupDialog.idLabel" sortable></Column>
            <Column
              field="name"
              :header="messages.pages.groupManagementPage.editGroupDialog.nameLabel"
              sortable
              :pt="{
                columnfilter: {
                  class: 'd-flex',
                },
                filtermenubutton: {
                  class: 'btn ms-1',
                },
                headerfilterclearbutton: {
                  class: 'btn ms-1',
                },
              }"
            >
              <template #filter="{ filterModel, filterCallback }">
                <InputText
                  v-model="filterModel.value"
                  type="text"
                  @input="filterCallback()"
                  class="form-control"
                  :placeholder="messages.pages.groupManagementPage.editGroupDialog.namePlaceholder"
                />
              </template>
            </Column>
            <Column field="users.length" :header="messages.pages.groupManagementPage.memberText" sortable>
              <template #body="slotProp">
                {{ slotProp.data.users.length }} {{ messages.pages.groupManagementPage.member }}
              </template>
            </Column>
            <Column field="courses.length" :header="messages.pages.groupManagementPage.courseText" sortable>
              <template #body="slotProp">
                {{ slotProp.data.courses.length }} {{ messages.pages.groupManagementPage.course }}
              </template>
            </Column>
            <Column :header="messages.pages.groupManagementPage.modifyText">
              <template #body="slotProp">
                <button type="button" class="btn btn-warning">
                  <i
                    class="fa-solid fa-pen-to-square"
                    @click="openModifyWindow(slotProp.data)"
                  ></i>
                </button>
              </template>
            </Column>
            <Column :header="messages.pages.groupManagementPage.deleteText">
              <template #body="slotProp">
                <button type="button" class="btn btn-danger">
                  <i
                    class="fa-solid fa-trash"
                    @click="deleteGroup(slotProp.data.id)"
                  ></i>
                </button>
              </template>
            </Column>
          </DataTable>
          <Dialog
            v-if="modifyGroupDialogVisible"
            v-model:visible="modifyGroupDialogVisible"
            modal
            :header="
              messages.pages.groupManagementPage.editGroupDialog.title +
              ' ' +
              currentlyModifyingGroup.name
            "
            :style="{ width: '50rem' }"
            :pt="{
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
            }"
          >
            <FormKit
              type="form"
              :actions="false"
              @submit="sendUpdateGroup"
              :value="currentlyModifyingGroup"
              :incomplete-message="
                messages.pages.groupManagementPage.editGroupDialog
                  .validationMessages.matchAllValidationMessage
              "
            >
              <FormKit
                type="text"
                name="name"
                :label="
                  messages.pages.groupManagementPage.editGroupDialog.nameLabel
                "
                validation="required|length:0,100"
                :validation-messages="{
                  required:
                    messages.pages.groupManagementPage.editGroupDialog
                      .validationMessages.nameRequired,
                  length:
                    messages.pages.groupManagementPage.editGroupDialog
                      .validationMessages.nameLength,
                }"
                :classes="{
                  input: {
                    'mb-1': true,
                    'form-control': true,
                  },
                }"
              />
              {{ currentlyModifyingGroup }}
              <DataTable
                :value="users"
                tableStyle="min-width: 40rem"
                sortField="name"
                :sortOrder="1"
                v-model:filters="userFilters"
                filterDisplay="row"
                v-model:selection="currentlyModifyingGroup.selectedUsers"
                selectionMode="multiple"
                dataKey="id"
                :metaKeySelection="false"
                :pt="{
                  table: {
                    class: 'table table-responsive align-middle',
                  },
                }"
              >
                <Column>
                  <template #header>
                    <div class="d-flex justify-content-center">
                      <button
                        type="button"
                        class="btn"
                        style="
                          --bs-btn-padding-y: 0.25rem;
                          --bs-btn-padding-x: 0.5rem;
                          --bs-btn-font-size: 0.75rem;
                          width: 28px;
                        "
                        @click="selectAllGroups"
                      >
                        <i
                          class="fa-solid fa-x text-danger"
                          v-if="
                            currentlyModifyingGroup.selectedUsers.length == 0
                          "
                        ></i>
                        <i class="fa-solid fa-check text-success" v-else></i>
                      </button>
                    </div>
                  </template>
                  <template #body="slotProp">
                    <div class="d-flex justify-content-center">
                      <i
                        class="fa-solid fa-check text-success"
                        v-if="
                          currentlyModifyingGroup.selectedUsers.findIndex(
                            (x) => x == slotProp.data
                          ) != -1
                        "
                      ></i>
                      <i class="fa-solid fa-x text-danger" v-else></i>
                    </div>
                  </template>
                </Column>
                <Column
                  field="id"
                  :header="
                    messages.pages.groupManagementPage.editGroupDialog.idLabel
                  "
                  sortable
                ></Column>
                <Column
                  field="name"
                  :header="
                    messages.pages.groupManagementPage.editGroupDialog.nameLabel
                  "
                  sortable
                  :pt="{
                    columnfilter: {
                      class: 'd-flex',
                    },
                    filtermenubutton: {
                      class: 'btn ms-1',
                    },
                    headerfilterclearbutton: {
                      class: 'btn ms-1',
                    },
                  }"
                >
                  <template #filter="{ filterModel, filterCallback }">
                    <InputText
                      v-model="filterModel.value"
                      type="text"
                      @input="filterCallback()"
                      class="form-control"
                      :placeholder="
                        messages.pages.groupManagementPage.editGroupDialog
                          .namePlaceholder
                      "
                    />
                  </template>
                </Column>
                <Column
                  field="email"
                  :header="
                    messages.pages.groupManagementPage.editGroupDialog
                      .emailLabel
                  "
                  sortable
                  :pt="{
                    columnfilter: {
                      class: 'd-flex',
                    },
                    filtermenubutton: {
                      class: 'btn ms-1',
                    },
                    headerfilterclearbutton: {
                      class: 'btn ms-1',
                    },
                  }"
                >
                  <template #filter="{ filterModel, filterCallback }">
                    <InputText
                      v-model="filterModel.value"
                      type="text"
                      @input="filterCallback()"
                      class="form-control"
                      :placeholder="
                        messages.pages.groupManagementPage.editGroupDialog
                          .emailPlaceholder
                      "
                    />
                  </template>
                </Column>
                <Column
                  field="permission"
                  :header="
                    messages.pages.groupManagementPage.editGroupDialog
                      .permissionLabel
                  "
                  :pt="{
                    columnfilter: {
                      class: 'd-flex',
                    },
                    filtermenubutton: {
                      class: 'btn ms-1',
                    },
                    headerfilterclearbutton: {
                      class: 'btn ms-1',
                    },
                  }"
                >
                  <template #body="slotProp">
                    <select
                      v-model="slotProp.data.permission"
                      class="form-select"
                    >
                      <option value="Tanár">Tanár</option>
                      <option value="Tanuló">Tanuló</option>
                    </select>
                  </template>
                </Column>
              </DataTable>
              <div class="d-flex justify-content-end mt-2 mb-3">
                <Button
                  type="button"
                  :label="messages.pages.groupManagementPage.editGroupDialog.cancelButton"
                  class="btn btn-outline-danger mx-1"
                  @click="modifyGroupDialogVisible = false"
                ></Button>
                <FormKit
                  type="submit"
                  :label="messages.pages.groupManagementPage.editGroupDialog.saveButton"
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
          </Dialog>
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
import Dialog from "primevue/dialog";
import Toast from "primevue/toast";
import Dropdown from "primevue/dropdown";
import { http } from "@utils/http";
import { mapActions, mapState } from "pinia";
import { groupStore } from "@stores/GroupStore";
import { FilterMatchMode } from "primevue/api";
import { themeStore } from "@stores/ThemeStore.mjs";
import { userStore } from "@stores/UserStore";
import { permissionStore } from "@stores/PermissionStore.mjs";
import { languageStore } from "@stores/LanguageStore.mjs";
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
    Dialog,
    Toast,
    RadioButton,
    Dropdown,
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
    };
  },
  computed: {
    ...mapState(userStore, ["token"]),
    ...mapState(groupStore, ["groups"]),
    ...mapState(themeStore, ["isDarkMode"]),
    ...mapState(permissionStore, ["permissions"]),
    ...mapState(languageStore, ["messages"]),
    loading() {
      return this.groups == null;
    },
  },
  methods: {
    ...mapActions(groupStore, [
      "getGroups",
      "getGroup",
      "postGroup",
      "updateGroup",
      "deleteGroup",
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
        let userIds = this.selectedGroups.map((x) => x.id);
        await http.post(
          "/users/delete",
          { userIds: userIds },
          {
            headers: {
              Authorization: `Bearer ${this.token}`,
            },
          }
        );

        if (this.isDarkMode) {
          this.$toast.add({
            severity: "success",
            detail:
              this.messages.groupManagementPage.toastMessages
                .successfullyDeletedMultipleGroups,
            life: 3000,
          });
        } else {
          this.$toast.add({
            severity: "success",
            detail:
              this.messages.groupManagementPage.toastMessages
                .successfullyDeletedMultipleGroups,
            styleClass: "bg-success text-white",
            life: 3000,
          });
        }

        await this.getUsers();
      } catch (error) {
        if (this.isDarkMode) {
          this.$toast.add({
            severity: "error",
            detail:
              this.messages.groupManagementPage.toastMessages
                .failedToDeleteMultipleGroups,
            life: 3000,
          });
        } else {
          this.$toast.add({
            severity: "error",
            detail:
              this.messages.groupManagementPage.toastMessages
                .failedToDeleteMultipleGroups,
            styleClass: "bg-danger text-white",
            life: 3000,
          });
        }
      }
    },
    async sendUpdateGroup(data) {
      data.selectedUsers = this.currentlyModifyingGroup.selectedUsers;
      await this.updateGroup(data);
    },
    async sendUpdateGroup(data) {
      data.selectedUsers = this.currentlyModifyingGroup.selectedUsers;
      await this.updateGroup(data);
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
  },
  mounted() {
    this.getGroups();
    this.getUsers();
    this.addPermissionFieldToAllGroups();
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

