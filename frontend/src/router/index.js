import { createRouter, createWebHistory } from "vue-router";
import { setTitle } from "@/router/guards/SetTitleGuard.mjs";
import { AuthGuard } from "@/router/guards/AuthGuard.mjs";
import { AdminGuard } from "@/router/guards/AdminGuard.mjs";

export const router = createRouter({
  history: createWebHistory(),
  routes: [
    {
      path: "/register",
      name: "register",
      component: () => import("@pages/RegisterPage.vue"),
      meta: {
        requiresAuth: false,
        title: "Regisztráció",
      },
    },
    {
      path: "/login",
      name: "login",
      component: () => import("@pages/LoginPage.vue"),
      meta: {
        requiresAuth: false,
        title: "Bejelentkezés",
      },
    },
    {
      path: "/",
      name: "home",
      component: () => import("@pages/HomePage.vue"),
      meta: {
        requiresAuth: true,
        title: "Főoldal",
      },
    },
    {
      path: "/profile",
      name: "profile",
      component: () => import("@pages/EditProfile.vue"),
      meta: {
        requiresAuth: true,
        title: "Profil szerkesztése",
      },
    },
    {
      path: "/user-administration",
      name: "userAdministration",
      component: () => import("@pages/UserManagementPage.vue"),
      meta: {
        requiresAuth: true,
        title: "Felhasználók kezelése",
        requiresAdmin: true,
      },
    },
    {
      path: "/course-administration",
      name: "courseAdministration",
      component: () => import("@pages/CourseManagementPage.vue"),
      meta: {
        requiresAuth: true,
        title: "Kurzusok kezelése",
        requiresAdmin: true,
      },
    },
    {
      path: "/group-administration",
      name: "groupAdministration",
      component: () => import("@pages/GroupManagementPage.vue"),
      meta: {
        requiresAuth: true,
        title: "Csoportok kezelése",
        requiresAdmin: true,
      },
    },
    {
      path: "/courses",
      name: "courses",
      component: () => import("@pages/CoursesPage.vue"),
      meta: {
        requiresAuth: true,
        title: "Kurzusok",
      },
    },
    {
      path: "/assignment/:id",
      name: "assignment",
      component: () => import("@pages/AssignmentPage.vue"),
      meta: {
        requiresAuth: true,
        title: "Feladat beadó felület",
      },
    },
    {
      path: "/course/:id",
      name: "course",
      component: () => import("@pages/CoursePage.vue"),
      meta: {
        requiresAuth: true,
      },
    },
  ],
});

router.beforeEach(setTitle);
router.beforeEach(AuthGuard);
router.beforeEach(AdminGuard);
