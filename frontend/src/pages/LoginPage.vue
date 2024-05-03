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
          'rounded-4': true,
          'p-0': true,
        },
      }"
    >
      <Message
        v-for="msg of message"
        :key="msg.id"
        :icon="`fa-solid fa-${msg.icon}`"
        class="w-75 mx-auto"
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
            <div class="d-flex justify-content-center align-items-center">
                  <li class="ms-3 mb-md-3 mt-md-3">
                    <i
                      :class="`fa-regular ${
                        !isDarkMode ? 'fa-moon' : 'fa-sun'
                      } fa-xl my-2`"
                      id="icon"
                      @click="toggleTheme"
                    ></i>
                  </li>
                  <li>
                    <a class="dropdown-item">
                      <div class="form-check form-switch">
                        <input
                          class="form-check-input"
                          type="checkbox"
                          @input="switchLanguage"
                          id="languageSwitch"
                          :checked="language === 'EN'"
                        />
                        <label
                          class="form-check-label text-white"
                          for="languageSwitch"
                          id="switchLabel"
                          >{{ language }}</label
                        >
                      </div></a
                    >
                  </li>
                </div>
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
              'px-3': true,
              'formButton' : true
            },
            outer: {
              'ms-2' : true
            }
          }"
        >
          {{ messages.pages.loginPage.loginButtonText }}
        </FormKit>

        <RouterLink :to="{ name: 'register' }" class="btn formLink h-100 me-2">
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
import { themeStore } from "@stores/ThemeStore.mjs";
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
    ...mapActions(languageStore, ["switchLanguage"]),

    toggleTheme() {
      themeStore().toggleTheme();
    },

    async attemptLogin(data) {
      try {
        await this.login(data);
        this.$router.push({ name: "home" });
      } catch (error) {
        this.message = [
          {
            color: "danger",
            icon: "triangle-exclamation",
            content: this.messages.pages.loginPage.loginFailed,
            id: this.count++,
          },
        ];
      }
    },
  },
  computed: {
    ...mapState(languageStore, ["messages","language"]),
    ...mapState(themeStore, ["isDarkMode"]),
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
  display: none;
}

.btn.dropdown-toggle{
  border: none;
}
</style>
