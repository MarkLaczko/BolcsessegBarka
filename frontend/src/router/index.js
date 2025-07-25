import { createRouter, createWebHistory } from "vue-router";
import { setTitle } from "@/router/guards/SetTitleGuard.mjs";
import { AuthGuard } from "@/router/guards/AuthGuard.mjs";
import { AdminGuard } from "@/router/guards/AdminGuard.mjs";
import { QuizCreationGuard } from "@/router/guards/QuizCreationGuard.mjs";
import { QuizAccessGuard } from "@/router/guards/QuizAccessGuard.mjs";
import { AttemptAccessGuard } from "@/router/guards/AttemptAccessGuard.mjs";
import { AttemptMarkGuard } from "@/router/guards/AttemptMarkGuard.mjs";

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
    {
      path: "/course/:courseId/topic/:topicId/create-quiz",
      name: "createQuiz",
      component: () => import("@pages/CreateQuizPage.vue"),
      meta: {
        requiresAuth: true,
        title: "Kvíz létrehozása",
        quizCreationGuard: true,
      },
    },
    {
      path: "/quiz/:id",
      name: "quiz",
      component: () => import("@pages/QuizPage.vue"),
      meta: {
        requiresAuth: true,
        title: "Kvíz",
        quizAccessGuard: true,
      },
    },
    {
      path: "/quiz/:id/edit",
      name: "editQuiz",
      component: () => import("@pages/EditQuizPage.vue"),
      meta: {
        requiresAuth: true,
        title: "Kvíz szerkesztése",
        quizCreationGuard: true,
      },
    },
    {
      path: "/quiz/:id/edit/create-task",
      name: "createTask",
      component: () => import("@pages/CreateTaskPage.vue"),
      meta: {
        requiresAuth: true,
        title: "Feladat hozzáadása",
        quizCreationGuard: true,
      },
    },
    {
      path: "/quiz/:quizId/edit/:taskId",
      name: "editTask",
      component: () => import("@pages/EditTaskPage.vue"),
      meta: {
        requiresAuth: true,
        title: "Feladat szerkesztése",
        quizCreationGuard: true,
      },
    },
    {
      path: "/attempt/:id",
      name: "attempt",
      component: () => import("@pages/AttemptPage.vue"),
      meta: {
        requiresAuth: true,
        title: "Kvíz kitöltése",
        attemptAccessGuard: true,
      },
    },
    {
      path: "/attempt/:id/mark",
      name: "attemptMark",
      component: () => import("@pages/MarkAttemptPage.vue"),
      meta: {
        requiresAuth: true,
        title: "Kvíz kitöltése",
        attemptMarkGuard: true,
      },
    },
  ],
});

router.beforeEach(setTitle);
router.beforeEach(AuthGuard);
router.beforeEach(AdminGuard);
router.beforeEach(QuizCreationGuard);
router.beforeEach(QuizAccessGuard);
router.beforeEach(AttemptAccessGuard);
router.beforeEach(AttemptMarkGuard);
