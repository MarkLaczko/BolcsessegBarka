<template>
  <nav class="navbar navbar-expand-lg" data-bs-theme="dark">
    <div class="container-fluid">
      <RouterLink class="navbar-brand" :to="{ name: 'home' }"
        ><img
          class="img-fluid"
          src="../../assets/images/logo.png"
          alt="BölcsességBárka"
          style="max-width: 50px"
      /></RouterLink>
      <button
        class="navbar-toggler"
        type="button"
        data-bs-toggle="collapse"
        data-bs-target="#navbarSupportedContent"
        aria-controls="navbarSupportedContent"
        aria-expanded="false"
        aria-label="Toggle navigation"
      >
        <span class="navbar-toggler-icon"></span>
      </button>
      <div class="collapse navbar-collapse" id="navbarSupportedContent">
        <ul class="navbar-nav me-auto mb-2 mb-lg-0 w-100">
          <li class="nav-item">
            <RouterLink class="nav-link" :to="{ name: 'home' }">{{
              messages.layout.header.home
            }}</RouterLink>
          </li>
          <li class="nav-item">
            <RouterLink class="nav-link" :to="{ name: 'courses' }">{{
              messages.layout.header.courses
            }}</RouterLink>
          </li>
          <li
            class="nav-item dropdown"
            v-if="
              currentUserData != undefined && currentUserData.is_admin === 1
            "
          >
            <a
              class="nav-link dropdown-toggle"
              href="#"
              role="button"
              data-bs-toggle="dropdown"
              aria-expanded="false"
            >
              {{ messages.layout.header.admin }}
            </a>
            <ul class="dropdown-menu">
              <li>
                <RouterLink
                  class="dropdown-item"
                  :to="{ name: 'userAdministration' }"
                  >{{ messages.layout.header.userAdministration }}</RouterLink
                >
              </li>
              <li>
                <RouterLink
                  class="dropdown-item"
                  :to="{ name: 'groupAdministration' }"
                  >{{ messages.layout.header.groupAdministration }}</RouterLink
                >
              </li>
              <li>
                <RouterLink
                  class="dropdown-item"
                  :to="{ name: 'courseAdministration' }"
                  >{{ messages.layout.header.courseAdministration }}</RouterLink
                >
              </li>
            </ul>
          </li>
        </ul>
        <div class="navbar-collapse" id="navbarNavDropdown">
          <ul class="navbar-nav ms-auto">
            <li class="nav-item dropdown">
              <a
                class="nav-link dropdown-toggle"
                href="#"
                id="navbarDropdownMenuLink"
                role="button"
                data-bs-toggle="dropdown"
                aria-expanded="false"
              >
                <div
                  class="user-icon"
                  style="
                    width: 30px;
                    height: 30px;
                    color: white;
                    display: inline-flex;
                    align-items: center;
                    justify-content: center;
                    border-radius: 50%;
                  "
                >
                  {{ avatarText }}
                </div>
              </a>
              <ul
                class="dropdown-menu dropdown-menu-end"
                aria-labelledby="navbarDropdownMenuLink"
              >
                <div class="">
                  <li class="ms-3 mb-md-3 mt-md-3">
                    <i
                      :class="`fa-regular ${
                        !isDarkMode ? 'fa-moon' : 'fa-sun'
                      } fa-xl d-none d-lg-block my-2`"
                      id="icon"
                      @click="toggleTheme"
                    ></i>
                    <button
                      class="nav-link d-block d-lg-none d-xl-none"
                      @click="toggleTheme"
                    >
                      <span v-if="!isDarkMode">Dark Mode</span>
                      <span v-else>Light Mode</span>
                    </button>
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
                          class="form-check-label"
                          for="languageSwitch"
                          id="switchLabel"
                          >{{ language }}</label
                        >
                      </div></a
                    >
                  </li>
                  <li>
                    <RouterLink
                      class="dropdown-item"
                      :to="{ name: 'profile' }"
                      >{{ messages.layout.header.profile }}</RouterLink
                    >
                  </li>
                  <li>
                    <a class="dropdown-item" @click="logoutUser">{{
                      messages.layout.header.logout
                    }}</a>
                  </li>
                </div>
              </ul>
            </li>
          </ul>
        </div>
      </div>
    </div>
  </nav>
</template>

<script>
import { userStore } from "@stores/UserStore.mjs";
import { mapActions, mapState } from "pinia";
import { themeStore } from "@stores/ThemeStore.mjs";
import { languageStore } from "@stores/LanguageStore.mjs";

export default {
  methods: {
    ...mapActions(userStore, ["getUser", "logout"]),
    ...mapActions(languageStore, ["switchLanguage"]),
    logoutUser() {
      this.logout();
      this.$router.push("/login");
    },

    toggleTheme() {
      themeStore().toggleTheme();
    },
  },
  computed: {
    ...mapState(userStore, ["currentUserData", "avatarText"]),
    ...mapState(themeStore, ["isDarkMode"]),
    ...mapState(languageStore, ["language", "messages"]),
  },
  async mounted() {
    await this.getUser();
  },
};
</script>
