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
        <h1 class="text-center my-3">{{ this.$route.meta.title }}</h1>
  
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
          header="Csoport hozzáadása"
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
            incomplete-message="Sajnáljuk, nem minden mezőt töltöttek ki helyesen."
          >
            <FormKit
              type="text"
              name="name"
              label="Név"
              validation="required|length:0,100"
              :validation-messages="{
                required: 'A csoport neve mező kitöltése kötelező.',
                length:
                  'A csoport nevének kevesebbnek kell lennie, mint 100 karakter.',
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
                label="Mégse"
                class="btn btn-outline-danger mx-1"
                @click="addGroupDialogVisible = false"
              ></Button>
              <FormKit
                type="submit"
                label="Mentés"
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
          :header="`${currentlyModifyingGroup.name} módosítása`"
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
            incomplete-message="Sajnáljuk, nem minden mezőt töltöttek ki helyesen."
          >
            <FormKit
              type="text"
              name="name"
              label="Név"
              validation="required|length:0,100"
              :validation-messages="{
                required: 'A csoport neve mező kitöltése kötelező.',
                length:
                  'A csoport nevének kevesebbnek kell lennie, mint 100 karakter.',
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
                        v-if="currentlyModifyingGroup.selectedUsers.length == 0"
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
                        currentlyModifyingGroup.selectedUsers.findIndex((x) => x == slotProp.data) != -1
                      "
                    ></i>
                    <i class="fa-solid fa-x text-danger" v-else></i>
                  </div>
                </template>
              </Column>
              <Column field="id" header="ID" sortable></Column>
              <Column
                field="name"
                header="Név"
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
                    placeholder="Név..."
                  />
                </template>
              </Column>
              <Column
                field="email"
                header="Email"
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
                    placeholder="Email..."
                  />
                </template>
              </Column>
              <Column
                field="permission"
                header="Jogosultság"
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
                  <select v-model="slotProp.data.permission_id" class="form-select">
                    <option v-for="permission of permissions" :key="permission.id" :value="permission.id">{{ permission.name }}</option>
                  </select>
                </template>
              </Column>
            </DataTable>
            <div class="d-flex justify-content-end mt-2 mb-3">
              <Button
                type="button"
                label="Mégse"
                class="btn btn-outline-danger mx-1"
                @click="modifyGroupDialogVisible = false"
              ></Button>
              <FormKit
                type="submit"
                label="Mentés"
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
                  label=" Új csoport"
                  id="newUser"
                  icon="pi pi-plus"
                  class="mr-2 btn btn-success text-white me-1 mt-2 ms-2"
                  @click="addGroupDialogVisible = true"
                />
                <Button
                  label=" Törlés"
                  icon="pi pi-trash"
                  class="btn btn-danger text-white mt-2"
                  @click="deleteMultipleGroups"
                />
              </template>
              <template #end>
                <Button
                  label=" Exportálás"
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
              <Column field="id" header="ID" sortable></Column>
              <Column
                field="name"
                header="Név"
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
                    placeholder="Név..."
                  />
                </template>
              </Column>
              <Column field="users.length" header="Tagok száma" sortable>
                <template #body="slotProp">
                  {{ slotProp.data.users.length }} tag
                </template>
              </Column>
              <Column field="courses.length" header="Kurzusok száma" sortable>
                <template #body="slotProp">
                  {{ slotProp.data.courses.length }} kurzus
                </template>
              </Column>
              <Column header="Módosítás">
                <template #body="slotProp">
                  <button type="button" class="btn btn-warning">
                    <i
                      class="fa-solid fa-pen-to-square"
                      @click="
                        (modifyGroupDialogVisible = true),
                          (currentlyModifyingGroup = {
                            ...slotProp.data,
                            selectedUsers: slotProp.data.users
                          })
                      "
                    ></i>
                  </button>
                </template>
              </Column>
              <Column header="Törlés">
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
  import Dropdown from 'primevue/dropdown';
  import { http } from "@utils/http";
  import { mapActions, mapState } from "pinia";
  import { groupStore } from "@stores/GroupStore";
  import { FilterMatchMode } from "primevue/api";
  import { themeStore } from "@stores/ThemeStore.mjs";
  import { userStore } from '@stores/UserStore';
  import { permissionStore } from "@stores/PermissionStore.mjs";
  
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
      ...mapState(userStore, ["token",]),
      ...mapState(groupStore, ["groups",]),
      ...mapState(themeStore, ["isDarkMode"]),
      ...mapState(permissionStore, ["permissions"]),
      loading() {
        return this.groups == null;
      }
    },
    methods: {
      ...mapActions(groupStore, ['getGroups', 'getGroup', 'postGroup', 'updateGroup', 'deleteGroup']),
      ...mapActions(permissionStore, ['getPermissions']),
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
              detail: "Felhasználó(k) törlése sikeres volt!",
              life: 3000,
            });
          } else {
            this.$toast.add({
              severity: "success",
              detail: "Felhasználó(k) törlése sikeres volt!",
              styleClass: "bg-success text-white",
              life: 3000,
            });
          }
  
          await this.getUsers();
        } catch (error) {
          if (this.isDarkMode) {
            this.$toast.add({
              severity: "error",
              detail: "Felhasználó(k) törlése sikertelen volt!",
              life: 3000,
            });
          } else {
            this.$toast.add({
              severity: "error",
              detail: "Felhasználó(k) törlése sikertelen volt!",
              styleClass: "bg-danger text-white",
              life: 3000,
            });
          }
        }
      },
      async sendUpdateGroup(data){
        data.selectedUsers = this.currentlyModifyingGroup.selectedUsers;
        await this.updateGroup(data);
      },
      exportCSV() {
        this.$refs.dt.exportCSV();
        console.log(this.$refs.dt);
      },
    },
    mounted() {
      this.getGroups();
      this.getPermissions();
      this.getUsers();
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
  