<template>
  <div class="d-flex justify-content-center align-items-center vh-100">
    <FormKit
      type="form"
      :actions="false"
      :classes="{
        form: {
          container: true,
          'form-control-width': true,
          'bg-info': true,
          'rounded-4': true,
          'px-5': true,
          'pb-2': true,
        },
      }"
      @submit="register"
      incomplete-message="Sajnáljuk, nem minden mezőt töltöttek ki helyesen."
    >
      <Message
        v-for="msg of messages"
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

      <h1 class="text-center my-4">Regisztráció</h1>

      <FormKit
        type="text"
        name="name"
        v-model="name"
        placeholder="Felhasználónév"
        validation="required|length:0,255"
        :validation-messages="{
          required: 'A felhasználónév kitöltése kötelező.',
          length:
            'A felhasználónévnek kevesebbnek kell lennie, mint 255 karakter.',
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
        }"
      />
      <FormKit
        type="email"
        name="email"
        v-model="email"
        placeholder="E-mail"
        validation="required|email"
        :validation-messages="{
          required: 'Az email kitöltése kötelező.',
          email: 'Adjon meg egy érvényes email címet.',
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
        }"
      />
      <FormKit
        type="password"
        name="password"
        v-model="password"
        placeholder="Jelszó"
        validation="required|length:8,255"
        :validation-messages="{
          required: 'A jelszó kitöltése kötelező.',
          length: 'Legalább 8, maximum 255 karakter hosszú lehet a jelszó.',
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
        }"
      />
      <FormKit
        type="password"
        name="password_confirm"
        v-model="password_confirm"
        placeholder="Jelszó megerősítés"
        validation="required|length:8,255|confirm"
        :validation-messages="{
          required: 'A jelszó megerősítés kitöltése kötelező.',
          length:
            'Legalább 8, maximum 255 karakter hosszú lehet a jelszó megerősítés.',
          confirm: 'A jelszavak nem egyeznek.',
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
        }"
      />
      <div class="d-flex gap-2 justify-content-center mt-3 mb-2">
        <FormKit
          type="submit"
          :classes="{
            input: {
              btn: true,
              'btn-primary': true,
              'btn-info': false,
              'form-control': false,
              'px-3': true,
            },
          }"
        >
          Regisztráció
        </FormKit>
        <RouterLink :to="{ name: 'login' }" class="btn btn-secondary h-100">
          Már van fiókja?
        </RouterLink>
      </div>
    </FormKit>
  </div>
</template>

<script>
import { http } from "@utils/http";
import Message from "primevue/message";
import { RouterLink } from "vue-router";

export default {
  components: {
    Message,
    RouterLink,
  },
  data() {
    return {
      messages: [],
      count: 0,
      name: "",
      email: "",
      password: "",
      password_confirm: "",
    };
  },
  methods: {
    async register(data) {
      try {
        let formData = { ...data };
        formData.password_confirmation = formData.password_confirm;
        await http.post("/users/register", formData);
        this.messages = [
          {
            color: "success",
            icon: "check",
            content: "Sikeres regisztráció!",
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
            content: "Sikertelen regisztráció!",
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
</style>
