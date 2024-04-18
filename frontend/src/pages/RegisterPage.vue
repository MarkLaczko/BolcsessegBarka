<template>
  <div class="d-flex justify-content-center align-items-center vh-100">
    <FormKit
      type="form"
      :actions="false"
      :classes="{
        form: {
          container: true,
          'form-control-width': true,
          'rounded-4': true,
          'pb-2': true,
        },
      }"
      @submit="register"
      :incomplete-message="messages.pages.registerPage.validationMessages.matchAllValidationMessage"
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
          <button class="btn dropdown-toggle p-0 pt-1" type="button" id="dropdownMenuButton" data-bs-toggle="dropdown" aria-expanded="false">
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

      <h1 class="text-center">{{ messages.pages.registerPage.title }}</h1>

      <FormKit
        type="text"
        name="name"
        v-model="name"
        :placeholder="messages.pages.registerPage.namePlaceholder"
        validation="required|length:0,255"
        :validation-messages="{
          required: messages.pages.registerPage.validationMessages.nameRequired,
          length:
            messages.pages.registerPage.validationMessages.nameLength,
        }"
        :classes="{
          input: {
            'm-auto': true,
            'form-control': true,
          },
          messages: {
            'list-style-none': true,
          },
          message: {
            'text-danger': true,
            'p-0': true,
          },
          outer: {
            'px-5': true,
          }
        }"
      />
      <FormKit
        type="email"
        name="email"
        v-model="email"
        :placeholder="messages.pages.registerPage.emailPlaceholder"
        validation="required|email"
        :validation-messages="{
          required: messages.pages.registerPage.validationMessages.emailRequired,
          email: messages.pages.registerPage.validationMessages.validEmail,
        }"
        :classes="{
          input: {
            'm-auto': true,
            'mt-2': true,
            'form-control': true,
          },
          messages: {
            'list-style-none': true,
          },
          message: {
            'text-danger': true,
            'p-0': true,
          },
          outer: {
            'px-5': true,
          }
        }"
      />
      <FormKit
        type="password"
        name="password"
        v-model="password"
        :placeholder="messages.pages.registerPage.passwordPlaceholder"
        validation="required|length:8,255"
        :validation-messages="{
          required: messages.pages.registerPage.validationMessages.passwordRequired,
          length: messages.pages.registerPage.validationMessages.passwordLength,
        }"
        :classes="{
          input: {
            'm-auto': true,
            'mt-2': true,
            'form-control': true,
          },
          messages: {
            'list-style-none': true,
          },
          message: {
            'text-danger': true,
            'p-0': true,
          },
          outer: {
            'px-5': true,
          }
        }"
      />
      <FormKit
        type="password"
        name="password_confirm"
        v-model="password_confirm"
        :placeholder="messages.pages.registerPage.passwordConfirmPlaceholder"
        validation="required|length:8,255|confirm"
        :validation-messages="{
          required: messages.pages.registerPage.validationMessages.confirmPasswordRequired,
          length:
          messages.pages.registerPage.validationMessages.confirmPasswordLength,
          confirm: messages.pages.registerPage.validationMessages.confirmPasswordConfirm,
        }"
        :classes="{
          input: {
            'm-auto': true,
            'mt-2': true,
            'form-control': true,
          },
          messages: {
            'list-style-none': true,
          },
          message: {
            'text-danger': true,
            'p-0': true,
          },
          outer: {
            'px-5': true,
          }
        }"
      />
      <div class="d-flex gap-2 justify-content-center mt-3 mb-2">
        <FormKit
          type="submit"
          :classes="{
            input: {
              btn: true,
              'formButton' : true,
              'px-3': true,
            },
          }"
        >
          {{ messages.pages.registerPage.submitButton }}
        </FormKit>
        <RouterLink :to="{ name: 'login' }" class="btn formLink h-100">
          {{ messages.pages.registerPage.accountText }}
        </RouterLink>
      </div>
    </FormKit>
  </div>
</template>

<script>
import { http } from "@utils/http";
import Message from "primevue/message";
import { RouterLink } from "vue-router";
import { themeStore } from "@stores/ThemeStore.mjs";
import { languageStore } from "@stores/LanguageStore.mjs";
import { mapState, mapActions } from "pinia";

export default {
  components: {
    Message,
    RouterLink,
  },
  data() {
    return {
      message: [],
      count: 0,
      name: "",
      email: "",
      password: "",
      password_confirm: "",
    };
  },
  computed: {
    ...mapState(languageStore, ["messages","language"]),
    ...mapState(themeStore, ["isDarkMode"]),
  },
  methods: {
    ...mapActions(languageStore, ["switchLanguage"]),

    toggleTheme() {
      themeStore().toggleTheme();
    },

    async register(data) {
      try {
        let formData = { ...data };
        formData.password_confirmation = formData.password_confirm;
        await http.post("/users/register", formData);
        this.message = [
          {
            color: "success",
            icon: "check",
            content: this.messages.pages.registerPage.registerSucceeded,
            id: this.count++,
          },
        ];

        setTimeout(() => {
          this.$router.push("/login");
        }, 2500);
      } catch (error) {
        this.messages = [
          {
            color: "danger",
            icon: "triangle-exclamation",
            content: this.messages.pages.registerPage.registerFailed,
            id: this.count++,
          },
        ];
      }
    },
  },
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
