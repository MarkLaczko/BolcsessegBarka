import axios from "axios";
import { userStore } from "@stores/UserStore.mjs";
import { router } from "@/router/index.js";

const http = axios.create({
  baseURL: "/api",
  headers: {
    Accept: "application/json",
    "Content-Type": "application/json",
  },
});

http.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response.status === 401) {
      const store = userStore();
      store.logout();
      router.push("/login");
    }
  }
);

export { http };
