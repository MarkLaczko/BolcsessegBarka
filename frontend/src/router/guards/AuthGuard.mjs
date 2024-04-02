import { userStore } from "@stores/UserStore.mjs";
export function AuthGuard(to, from, next) {
  if (userStore().isAuthenticated) {
    if (to.path === "/login" || to.path === "/register") {
      next("/home");
    }
    next();
  } else {
    if (to.meta.requiresAuth) {
      next({ name: "login" });
    } else {
      next();
    }
  }
}
