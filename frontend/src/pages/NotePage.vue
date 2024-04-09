<template>
  <BaseLayout>
    <div class="container rounded-3 my-5 pt-1 pb-3">
      <h1 class="text-center my-3">{{ this.$route.meta.title }}</h1>
      <div class="d-flex align-items-center justify-content-center pb-4">
        <label for="username" class="form-label me-2 font-weight-bold"
          ><b>Jegyzet neve:</b></label
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
        <Button label="Mégse" class="btn text-light btn-danger px-5 me-4" />
        <Button label="Mentés" class="btn text-light btn-success px-5" />
      </div>
    </div>
  </BaseLayout>
</template>

<script>
import BaseLayout from "@layouts/BaseLayout.vue";
import InputText from "primevue/inputtext";
import Editor from "primevue/editor";
import Button from "primevue/button";

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
  },
};
</script>
