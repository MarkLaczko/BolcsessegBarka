import { createRouter, createWebHistory } from 'vue-router'


export const router = createRouter({
  history: createWebHistory(),
  routes: [
    {
      path: '/register',
      name: 'register',
      component: () => import('@pages/RegisterPage.vue'),
      meta: {
        requiresAuth: false,
        title: 'Regisztráció'
      }
    },
    {
      path: '/login',
      name: 'login',
      component: () => import('@pages/LoginPage.vue'),
      meta: {
        requiresAuth: false,
        title: 'Bejelentkezés'
      }
    },
    {
      path: '/home',
      name: 'home',
      component: () => import('@pages/HomePage.vue'),
      meta: {
        requiresAuth: true,
        title: 'Főoldal'
      }
    }
  ]
})

