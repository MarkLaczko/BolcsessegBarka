import { createRouter, createWebHistory } from 'vue-router'


export const router = createRouter({
  history: createWebHistory(),
  routes: [
    {
      path: '/',
      name: 'register',
      component: () => import('@pages/RegisterPage.vue'),
    },
    {
      path: '/login',
      name: 'login',
      component: () => import('@pages/LoginPage.vue'),
    },
    {
      path: '/home',
      name: 'home',
      component: () => import('@pages/HomePage.vue'),
    }
  ]
})

