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
            <RouterLink class="nav-link" :to="{ name: 'home' }"
              >Főoldal</RouterLink
            >
          </li>
          <li class="nav-item">
            <RouterLink class="nav-link" :to="{ name: 'courses' }"
              >Kurzusaim</RouterLink
            >
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
              Adminisztráció
            </a>
            <ul class="dropdown-menu">
              <li>
                <RouterLink
                  class="dropdown-item"
                  :to="{ name: 'userAdministration' }"
                  >Felhasználó kezelése</RouterLink
                >
              </li>
              <li>
                <RouterLink
                  class="dropdown-item"
                  :to="{ name: 'groupAdministration' }"
                  >Csoportok kezelése</RouterLink
                >
              </li>
              <li>
                <RouterLink
                  class="dropdown-item"
                  :to="{ name: 'courseAdministration' }"
                  >Kurzusok kezelése</RouterLink
                >
              </li>
            </ul>
          </li>
          <li class="nav-item ms-lg-auto d-flex align-items-center me-2">
            <i
              :class="`fa-regular ${isDarkMode ? 'fa-sun' : 'fa-moon'} fa-xl d-none d-lg-block my-2`"
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
          <li class="nav-item d-flex">
            <button class="nav-link" @click="logoutUser">Kijelentkezés</button>
          </li>
        </ul>
      </div>
    </div>
  </nav>
</template>

<script>
import { userStore } from "@stores/UserStore.mjs";
import { mapActions, mapState } from "pinia";
import { themeStore } from "@stores/ThemeStore.mjs";

export default {
  methods: {
    ...mapActions(userStore, ["getUser", "logout"]),
    logoutUser() {
      this.logout();
      this.$router.push("/login");
    },

    toggleTheme() {
      themeStore().toggleTheme();
    },
  },
  computed: {
    ...mapState(userStore, ["currentUserData"]),
    ...mapState(themeStore, ["isDarkMode"]),
  },
  async mounted() {
    await this.getUser();
  }
};
</script>
