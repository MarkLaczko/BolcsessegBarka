<template>
  <BaseLayout>
    <div class="rounded-3 my-5 py-2">
      <div class="rounded-3 m-3 p-2 bg-white">
        <p>
          Beadandó feladat neve <br />
          Tantárgy <br />
          Leadási határidő
        </p>
        <div class="card">
          <Toast />
          <FileUpload
            :pt="{
              input: {
                class: 'd-none',
              },
            }"
            name="demo[]"
            url="/api/upload"
            @upload="onTemplatedUpload($event)"
            :multiple="true"
            accept=".zip, .rar, .jpg, .jpeg, .png"
            :maxFileSize="100000000"
            @select="onSelectedFiles"
          >
            <template
              #header="{ chooseCallback, uploadCallback, clearCallback, files }"
            >
              <div
                class="d-flex justify-content-between align-items-center flex-wrap flex-grow-1 mt-2"
              >
                <div class="d-flex gap-2 ms-2">
                  <button
                    @click="chooseCallback()"
                    class="btn btn-outline-primary rounded-circle"
                  >
                    <i class="pi pi-images"></i>
                  </button>
                  <button
                    @click="uploadEvent(uploadCallback)"
                    class="btn btn-outline-success rounded-circle"
                    :disabled="!files || files.length === 0"
                  >
                    <i class="pi pi-cloud-upload"></i>
                  </button>
                  <button
                    @click="clearCallback()"
                    class="btn btn-outline-danger rounded-circle"
                    :disabled="!files || files.length === 0"
                  >
                    <i class="pi pi-times"></i>
                  </button>
                </div>
                <div class="progress mb-3 w-25 me-3">
                  <div
                    class="progress-bar bg-success"
                    role="progressbar"
                    :style="{ width: totalSizePercent + '%' }"
                    :aria-valuenow="totalSizePercent"
                    aria-valuemin="0"
                    aria-valuemax="100"
                  >
                    <span class="text-white">{{ totalSize }}B / 100MB</span>
                  </div>
                </div>
              </div>
            </template>
            <template
              #content="{
                files,
                uploadedFiles,
                removeUploadedFileCallback,
                removeFileCallback,
              }"
            >
              <div v-if="files.length > 0">
                <h5 class="mt-3 ms-2">Függőben</h5>
                <div
                  class="row row-cols-1 row-cols-sm-2 row-cols-md-3 p-0 sm:p-5 gap-3 mx-2 mb-2"
                >
                  <div
                    v-for="(file, index) of files"
                    :key="file.name + file.type + file.size"
                    class="col card m-0 px-4 py-3 border-1 surface-border align-items-center gap-3"
                  >
                    <div>
                      <img
                        role="presentation"
                        :alt="file.name"
                        :src="file.objectURL"
                        class="img-fluid"
                      />
                    </div>
                    <span class="font-weight-bold">{{ file.name }}</span>
                    <div>{{ formatSize(file.size) }}</div>
                    <span class="badge bg-warning">Függőben</span>
                    <button
                      @click="
                        onRemoveTemplatingFile(file, removeFileCallback, index)
                      "
                      class="btn btn-outline-danger rounded-circle"
                    >
                      <i class="pi pi-times"></i>
                    </button>
                  </div>
                </div>
              </div>

              <div v-if="uploadedFiles.length > 0">
                <h5 class="mt-3 ms-2">Feltöltve</h5>
                <div
                  class="row row-cols-1 row-cols-sm-2 row-cols-md-3 p-0 sm:p-5 gap-3 mx-2 mb-2"
                >
                  <div
                    v-for="(file, index) of uploadedFiles"
                    :key="file.name + file.type + file.size"
                    class="col card m-0 px-4 py-3 border-1 surface-border align-items-center gap-3"
                  >
                    <div>
                      <img
                        role="presentation"
                        :alt="file.name"
                        :src="file.objectURL"
                        class="img-fluid"
                      />
                    </div>
                    <span class="font-weight-bold">{{ file.name }}</span>
                    <div>{{ formatSize(file.size) }}</div>
                    <span class="badge bg-success">Feltöltve</span>
                    <button
                      @click="removeUploadedFileCallback(index)"
                      class="btn btn-outline-danger rounded-circle"
                    >
                      <i class="pi pi-times"></i>
                    </button>
                  </div>
                </div>
              </div>
            </template>
            <template #empty>
              <div
                class="d-flex align-items-center justify-content-center flex-column mb-2"
              >
                <i
                  class="pi pi-cloud-upload border rounded-circle p-3 text-custom"
                />
                <p class="mt-4 mb-0">Húzza ide a fájlokat a feltöltéshez.</p>
              </div>
            </template>
          </FileUpload>
        </div>
        <div class="d-flex justify-content-end">
          <button class="btn btn-outline-danger mt-3 px-5">Mégsem</button>
        </div>
      </div>
    </div>
  </BaseLayout>
</template>

<script>
import BaseLayout from "@layouts/BaseLayout.vue";
import FileUpload from "primevue/fileupload";
import Toolbar from "primevue/toolbar";
import Button from "primevue/button";
import Toast from "primevue/toast";
import ProgressBar from "primevue/progressbar";

export default {
  components: {
    BaseLayout,
    FileUpload,
    Toolbar,
    Button,
    Toast,
    ProgressBar,
  },
  data() {
    return {
      files: [],
      totalSize: 0,
      totalSizePercent: 0,
    };
  },
  methods: {
    onRemoveTemplatingFile(file, removeFileCallback, index) {
      removeFileCallback(index);
      this.totalSize -= parseInt(this.formatSize(file.size));
      this.totalSizePercent = this.totalSize / 10;
    },
    onClearTemplatingUpload(clear) {
      clear();
      this.totalSize = 0;
      this.totalSizePercent = 0;
    },
    onSelectedFiles(event) {
      this.files = event.files;
      this.files.forEach((file) => {
        this.totalSize += parseInt(this.formatSize(file.size));
      });
    },
    uploadEvent(callback) {
      this.totalSizePercent = this.totalSize / 10;
      callback();
    },
    onTemplatedUpload() {
      this.$toast.add({
        severity: "info",
        summary: "Success",
        detail: "File Uploaded",
        life: 3000,
      });
    },
    formatSize(bytes) {
      const k = 1024;
      const dm = 3;
      const sizes = this.$primevue.config.locale.fileSizeTypes;

      if (bytes === 0) {
        return `0 ${sizes[0]}`;
      }

      const i = Math.floor(Math.log(bytes) / Math.log(k));
      const formattedSize = parseFloat((bytes / Math.pow(k, i)).toFixed(dm));

      return `${formattedSize} ${sizes[i]}`;
    },
  },
};
</script>

<style scoped>
.text-custom {
  font-size: 5rem;
}

.border {
  border-width: 2px;
}
</style>
