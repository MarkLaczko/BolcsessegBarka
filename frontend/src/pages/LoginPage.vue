<template>
  <div class="d-flex justify-content-center align-items-center vh-100">
    <FormKit
      type="form"
      :actions="false"
      @submit="attemptLogin"
      :classes="{
        form: {
          container: true,
          'form-control-width': true,
          'bg-info': true,
          'rounded-4': true,
          'p-0': true,
        },
      }"
    >
      <Message
        v-for="msg of message"
        :key="msg.id"
        :icon="`fa-solid fa-${msg.icon}`"
        :pt="{
          wrapper: `alert alert-${msg.color} d-flex flex-row w-100 justify-content-between align-items-center p-2 mt-4 mb-0`,
          button: `btn btn-outline-${msg.color} rounded-circle`,
        }"
      >
        {{ msg.content }}
        <template #closeicon>
          <i class="fa-solid fa-xmark"></i>
        </template>
      </Message>

      <div class="dropdown d-flex justify-content-end">
          <button class="btn dropdown-toggle" type="button" id="dropdownMenuButton" data-bs-toggle="dropdown" aria-expanded="false">
            <i class="fa-solid fa-gear"></i>
          </button>
          <ul class="dropdown-menu dropdown-menu-end" aria-labelledby="dropdownMenuButton">
            <li><a class="dropdown-item" href="#">Action</a></li>
            <li><a class="dropdown-item" href="#">Another action</a></li>
            <li><a class="dropdown-item" href="#">Something else here</a></li>
          </ul>
        </div>
      
      <h1 class="text-center">{{ messages.pages.loginPage.title }}</h1>

      <FormKit
        type="email"
        :placeholder="messages.pages.loginPage.emailPlaceholder"
        name="email"
        :classes="{
          input: {
            'm-auto': true,
            'my-2': true,
            'form-control': true,
          },
          outer: {
            'px-5': true,
          }
        }"
      />
      <FormKit
        type="password"
        :placeholder="messages.pages.loginPage.passwordPlaceholder"
        name="password"
        :classes="{
          input: {
            'm-auto': true,
            'form-control': true,
          },
          outer: {
            'px-5': true,
          }
        }"
      />

      <div
        class="d-flex gap-2 justify-content-center align-items-center mt-3 mb-2"
      >
        <FormKit
          type="submit"
          :classes="{
            input: {
              'text-white': true,
              'bg-primary': true,
              'px-3': true,
            },
          }"
        >
          {{ messages.pages.loginPage.loginButtonText }}
        </FormKit>

        <RouterLink :to="{ name: 'register' }" class="btn btn-secondary h-100">
          {{ messages.pages.loginPage.accountText }}
        </RouterLink>
      </div>
    </FormKit>
  </div>
  </template>

<script>
import Message from "primevue/message";
import { RouterLink } from "vue-router";
import { mapActions, mapState } from "pinia";
import { userStore } from "@stores/UserStore.mjs";
import { languageStore } from "@stores/LanguageStore.mjs";

export default {
  components: {
    Message,
    RouterLink,
  },
  data() {
    return {
      message: [],
      count: 0,
    };
  },
  methods: {
    ...mapActions(userStore, ["login"]),
    async attemptLogin(data) {
      try {
        await this.login(data);
        this.$router.push({ name: "home" });
      } catch (error) {
        this.message = [
          {
            color: "danger",
            icon: "triangle-exclamation",
            content: messages.pages.loginPage.loginFailed,
            id: this.count++,
          },
        ];
      }
    },
  },
  computed: {
    ...mapState(languageStore, ["messages"]),
  }
};
</script>

<style scoped>
.form-control-width {
  width: 40%;
}

@media screen and (max-width: 910px) {
  .form-control-width {
    width: 80%;
  }
}

.dropdown-toggle::after {
  display: none !important;
}
</style>
