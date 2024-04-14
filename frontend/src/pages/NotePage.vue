<template>
  <BaseLayout>
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

    <div class="container rounded-3 my-5 pt-1 pb-3">
      <h1 class="text-center my-3">{{ messages.pages.notePage.title }}</h1>
      <div class="d-flex align-items-center justify-content-center pb-4">
        <label for="username" class="form-label me-2 font-weight-bold"
          ><b>{{ messages.pages.notePage.notesNameText }}</b></label
        >
        <div class="w-25">
          <InputText
            id="username"
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
          :label="messages.pages.notePage.cancelButton"
          class="btn text-light btn-danger px-5 me-4"
        />
        <Button
          :label="messages.pages.notePage.saveButton"
          class="btn text-light btn-success px-5"
        />
      </div>
    </div>
  </BaseLayout>
</template>

<script>
import BaseLayout from "@layouts/BaseLayout.vue";
import InputText from "primevue/inputtext";
import Editor from "primevue/editor";
import Button from "primevue/button";
import Toast from "primevue/toast";
import { http } from "@utils/http.mjs";
import { mapState } from "pinia";
import { languageStore } from "@stores/LanguageStore.mjs";

export default {
  data() {
    return {
      title: "",
      text: "",
    };
  },
  components: {
    BaseLayout,
    InputText,
    Editor,
    Button,
    Toast,
  },
  computed: {
    ...mapState(languageStore, ["messages"]),
  },
  methods: {
    async saveNote() {
      try {
        const note = {
          title: this.title,
          text: this.text,
        };

        const response = await http.post("/api/notes", note, {
          //TODO: Backend részről megírni az API-t.
          headers: {
            Authorization: `Bearer ${this.token}`,
          },
        });

        if (response.status === 200) {
          let toast = {
            severity: "success",
            detail: "Jegyzet elmentése sikeres volt!",
            life: 3000,
          };
          if (!this.isDarkMode) {
            toast.styleClass = "bg-success text-white";
          }

          this.$toast.add(toast);
        }
      } catch (error) {
        let toast = {
          severity: "error",
          detail: "Jegyzet elmentése sikertelen volt!",
          life: 3000,
        };
        if (!this.isDarkMode) {
          toast.styleClass = "bg-danger text-white";
        }
        this.$toast.add(toast);
      }
    },
  },
};
</script>
