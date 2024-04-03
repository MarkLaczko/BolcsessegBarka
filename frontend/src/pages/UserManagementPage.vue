<template>
  <BaseLayout>
    <h1 class="text-center my-3">{{ this.$route.meta.title }}</h1>

    <Toast
      :pt="{
        root: {
          class: 'w-25',
        },
        transition: {
          name: 'slide-fade',
        },
      }"
    />

    <Dialog
      v-if="addUserDialogVisible"
      v-model:visible="addUserDialogVisible"
      modal
      header="Felhasználó hozzáadása"
      :style="{ width: '25rem' }"
      :pt="{
        root: {
          class: 'modal-dialog bg-light p-3 rounded shadow border',
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
      <FormKit type="form" :actions="false" @submit="postUser">
        <FormKit
          type="text"
          name="name"
          label="Név"
          validation="required|length:0,255"
          :validation-messages="{
            required: 'Ez a mező kötelező.',
            length:
              'A felhasználónévnek kevesebbnek kell lennie, mint 255 karakter.',
          }"
          :classes="{
            input: {
              'mb-1': true,
            },
          }"
        />

        <FormKit
          type="email"
          name="email"
          label="Email"
          validation="required|email"
          :validation-messages="{
            required: 'Ez a mező kötelező.',
            email: 'Adjon meg egy érvényes email címet.',
          }"
          :classes="{
            input: {
              'mb-1': true,
            },
          }"
        />
        <FormKit
          type="password"
          name="password"
          label="Jelszó"
          validation="required|length:8,255"
          :validation-messages="{
            required: 'Ez a mező kötelező.',
            length: 'Legalább 8, maximum 255 karakter hosszú lehet a jelszó.',
          }"
          :classes="{
            input: {
              'mb-1': true,
            },
          }"
        />
        <FormKit
          type="password"
          name="password_confirm"
          label="Jelszó megerősítés"
          validation="required|length:8,255|confirm"
          :validation-messages="{
            required: 'Ez a mező kötelező.',
            length: 'Legalább 8, maximum 255 karakter hosszú lehet a jelszó.',
            confirm: 'A jelszavak nem egyeznek.',
          }"
          :classes="{
            input: {
              'mb-1': true,
            },
          }"
        />
        <div class="d-flex justify-content-end mt-2">
          <Button
            type="button"
            label="Mégse"
            class="btn btn-outline-danger mx-1"
            @click="
              addUserDialogVisible = false;
              this.$toast.add({
                severity: 'info',
                summary: 'Siker!',
                detail: 'Message Content',
                life: 5000,
              });
            "
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
              label=" Új felhasználó"
              icon="pi pi-plus"
              class="mr-2 btn btn-success text-white me-1 mt-2 ms-2"
              @click="addUserDialogVisible = true"
            />
            <Button
              label=" Törlés"
              icon="pi pi-trash"
              class="btn btn-danger text-white mt-2"
            />
          </template>
          <template #end>
            <FileUpload
              :pt="{
                input: {
                  class: 'd-none',
                },
              }"
              mode="basic"
              accept="image/*"
              :maxFileSize="1000000"
              label="Importálás"
              chooseLabel=" Importálás"
              class="mr-2 btn btn-success text-white inline-block me-1 mt-2"
            />
            <Button
              label=" Exportálás"
              icon="pi pi-upload"
              class="btn btn-warning text-white mt-2 me-2"
            />
          </template>
        </Toolbar>
        <DataTable
          :value="users"
          tableStyle="min-width: 50rem"
          sortField="id"
          :sortOrder="1"
          v-model:filters="filters"
          filterDisplay="row"
          v-model:selection="selectedUsers"
          selectionMode="multiple"
          dataKey="id"
          :metaKeySelection="false"
          :pt="{
            table: {
              class: 'table table-responsive align-middle',
            },
          }"
        >
          <!-- <Column selectionMode="multiple" headerStyle="width: 3rem"
                        :pt = "{
                            'rowcheckbox': {
                                class: 'bg-primary'
                            },
                            box: {
                                class: 'd-none'
                            }
                        }"></Column> -->
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
                  @click="selectAllUsers"
                >
                  <i
                    class="fa-solid fa-x text-danger"
                    v-if="selectedUsers.length == 0"
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
                    selectedUsers.findIndex((x) => x == slotProp.data) != -1
                  "
                ></i>
                <i class="fa-solid fa-x text-danger" v-else></i>
              </div>
            </template>
          </Column>
          <Column field="id" header="ID" sortable></Column>
          <Column
            field="name"
            header="Name"
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
          <Column field="is_admin" header="Admin" sortable>
            <template #body="slotProp">
              <div class="d-flex justify-content-center">
                <input
                  type="radio"
                  :checked="slotProp.data.is_admin == 1"
                  disabled
                  class="disabled-black"
                />
              </div>
            </template>
          </Column>
          <Column header="Módosítás">
            <template #body="slotProp">
              <button type="button" class="btn btn-dark">💀</button>
            </template>
          </Column>
          <Column header="Törlés">
            <template #body="slotProp">
              <button type="button" class="btn btn-dark">💀</button>
            </template>
          </Column>
        </DataTable>
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
import { http } from "@utils/http";
import { mapState } from "pinia";
import { userStore } from "@stores/UserStore";
import { FilterMatchMode } from "primevue/api";

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
    };
  },
  computed: {
    ...mapState(userStore, ["token"]),
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
    async postUser() {
      this.$toast.add({
        severity: "info",
        summary: "Siker!",
        detail: "Message Content",
        life: 3000,
      });
    },
    selectAllUsers() {
      if (this.users.length != this.selectedUsers.length) {
        this.selectedUsers = this.selectedUsers.concat(this.users);
      } else {
        this.selectedUsers = [];
      }
    },
  },
  mounted() {
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
