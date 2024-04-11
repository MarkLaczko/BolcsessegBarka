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
        v-if="addCourseDialogVisible"
        v-model:visible="addCourseDialogVisible"
        modal
        header="Kurzus hozzáadása"
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
          @submit="postCourse"
          incomplete-message="Sajnáljuk, nem minden mezőt töltöttek ki helyesen."
        >
          <FormKit
            type="text"
            name="name"
            label="Kurzus neve"
            validation="required|length:0,255"
            :validation-messages="{
              required: 'A kurzus nevének kitöltése kötelező.',
              length:
                'A kurzus nevének kevesebbnek kell lennie, mint 255 karakter.',
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
            label="Kép hozzáadása"
            accept=".JPEG,.PNG,.JPG,.BMP"
            :validation-messages="{
              required: 'Kép feltöltése kötelező.',
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
              @click="addCourseDialogVisible = false"
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
        v-if="modifyCourseDialogVisible"
        v-model:visible="modifyCourseDialogVisible"
        modal
        :header="`${currentlyModifyingCourse.name} módosítása`"
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
          @submit="updateCourse"
          :value="currentlyModifyingCourse"
          incomplete-message="Sajnáljuk, nem minden mezőt töltöttek ki helyesen."
        >
          <FormKit
            type="text"
            name="name"
            label="Kurzus neve"
            validation="length:0,255"
            :validation-messages="{
              length:
                'A kurzus nevének kevesebbnek kell lennie, mint 255 karakter.',
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
            name="image"
            label="Kép hozzáadása"
            accept=".JPEG,.PNG,.JPG,.BMP"
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
              @click="modifyCourseDialogVisible = false"
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
                label=" Új kurzus"
                icon="pi pi-plus"
                class="mr-2 btn btn-success text-white me-1 mt-2 ms-2"
                @click="addCourseDialogVisible = true"
              />
              <Button
                label=" Törlés"
                icon="pi pi-trash"
                class="btn btn-danger text-white mt-2"
                @click="deleteMultipleCourses"
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
            exportFilename="courses"
            ref="dt"
            :value="courses"
            tableStyle="min-width: 50rem"
            sortField="id"
            :sortOrder="1"
            v-model:filters="filters"
            filterDisplay="row"
            v-model:selection="selectedCourses"
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
                    @click="selectAllCourses"
                  >
                    <i
                      class="fa-solid fa-x text-danger"
                      v-if="selectedCourses.length == 0"
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
                      selectedCourses.findIndex((x) => x == slotProp.data) != -1
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
            <Column field="image" header="Kép">
              <template #body="slotProp">
                <div class="d-flex justify-content-center">
                  <img
                    v-if="slotProp.data.image"
                    :src="'data:image/jpeg;base64,' + slotProp.data.image"
                    alt="Course Image"
                    style="max-height: 100px; max-width: 100%"
                  />
                </div>
              </template>
            </Column>
            <Column header="Módosítás">
              <template #body="slotProp">
                <button
                  type="button"
                  class="btn btn-warning"
                  v-if="slotProp.data.email != currentUserData.email"
                >
                  <i
                    class="fa-solid fa-pen-to-square"
                    @click="
                      (modifyCourseDialogVisible = true),
                        (currentlyModifyingCourse = {
                          ...slotProp.data,
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
                    @click="deleteCourse(slotProp.data.id)"
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
import { http } from "@utils/http";
import { mapActions, mapState } from "pinia";
import { userStore } from "@stores/UserStore";
import { FilterMatchMode } from "primevue/api";
import { themeStore } from "@stores/ThemeStore.mjs";
import { courseStore } from "@stores/CourseStore.mjs";

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
      checked: true,
    };
  },
  computed: {
    ...mapState(userStore, ["token", "currentUserData"]),
    ...mapState(themeStore, ["isDarkMode"]),
    ...mapState(courseStore, ["courses"]),
  },
  methods: {
    ...mapActions(courseStore, [
      "getCourses",
      "postCourse",
      "destroyCourse",
      "putCourse",
    ]),
    async postCourse(data) {
      try {
        const response = await this.postCourse(data);

        if (response.status === 200) {
          let toast = {
            severity: "success",
            detail: "Kurzus hozzáadása sikeres volt!",
            life: 3000,
          };
          if (!this.isDarkMode) {
            toast.styleClass = "bg-success text-white";
          }

          this.$toast.add(toast);
        }

        await this.getCourses();
      } catch (error) {
        let toast = {
          severity: "error",
          detail: "Kurzus hozzáadása sikertelen volt!",
          life: 3000,
        };
        if (!this.isDarkMode) {
          toast.styleClass = "bg-danger text-white";
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
    async deleteCourse(userId) {
      try {
        await this.destroyCourse(userId);

        if (this.isDarkMode) {
          this.$toast.add({
            severity: "success",
            detail: "Kurzus törlése sikeres volt!",
            life: 3000,
          });
        } else {
          this.$toast.add({
            severity: "success",
            detail: "Kurzus törlése sikeres volt!",
            styleClass: "bg-success text-white",
            life: 3000,
          });
        }

        await this.getCourses();
      } catch (error) {
        if (this.isDarkMode) {
          this.$toast.add({
            severity: "error",
            detail: "Kurzus törlése sikertelen volt!",
            life: 3000,
          });
        } else {
          this.$toast.add({
            severity: "error",
            detail: "Kurzus törlése sikertelen volt!",
            styleClass: "bg-danger text-white",
            life: 3000,
          });
        }
      }
    },
    async deleteMultipleCourses() {
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

        if (this.isDarkMode) {
          this.$toast.add({
            severity: "success",
            detail: "Kurzus(ok) törlése sikeres volt!",
            life: 3000,
          });
        } else {
          this.$toast.add({
            severity: "success",
            detail: "Kurzus(ok) törlése sikeres volt!",
            styleClass: "bg-success text-white",
            life: 3000,
          });
        }

        await this.getCourses();
      } catch (error) {
        if (this.isDarkMode) {
          this.$toast.add({
            severity: "error",
            detail: "Kurzus(ok) törlése sikertelen volt!",
            life: 3000,
          });
        } else {
          this.$toast.add({
            severity: "error",
            detail: "Kurzus(ok) törlése sikertelen volt!",
            styleClass: "bg-danger text-white",
            life: 3000,
          });
        }
      }
    },
    async updateCourse(data) {
      try {
        await this.putCourse(data);
        this.modifyCourseDialogVisible = false;

        if (this.isDarkMode) {
          this.$toast.add({
            severity: "success",
            detail: "Kurzus módosítása sikeres volt!",
            life: 3000,
          });
        } else {
          this.$toast.add({
            severity: "success",
            detail: "Kurzus módosítása sikeres volt!",
            styleClass: "bg-success text-white",
            life: 3000,
          });
        }

        await this.getCourses();
      } catch (error) {
        if (this.isDarkMode) {
          this.$toast.add({
            severity: "error",
            detail: "Kurzus módosítása sikertelen volt!",
            life: 3000,
          });
        } else {
          this.$toast.add({
            severity: "error",
            detail: "Kurzus módosítása sikertelen volt!",
            styleClass: "bg-danger text-white",
            life: 3000,
          });
        }
      }
    },
    exportCSV() {
      this.$refs.dt.exportCSV();
    },
  },
  async mounted() {
    this.getCourses();
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
