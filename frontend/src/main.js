import { createApp } from "vue";
import { createPinia } from "pinia";
import { router } from "@/router/index.js";
import piniaPluginPersistedstate from "pinia-plugin-persistedstate";
import { plugin, defaultConfig } from "@formkit/vue";
import config from "../formkit.config.js";
import PrimeVue from "primevue/config";
import ToastService from "primevue/toastservice";
import ConfirmationService from 'primevue/confirmationservice';
import App from "@/App.vue";

import "bootstrap";
import "@assets/app.scss";

const app = createApp(App);

const pinia = createPinia();
pinia.use(piniaPluginPersistedstate);

app.use(router);
app.use(plugin, defaultConfig(config));
app.use(pinia);

app.use(PrimeVue, {
  unstyled: true,
});
app.use(ToastService);
app.use(ConfirmationService);

app.mount("#app");
