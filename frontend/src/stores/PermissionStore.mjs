import { defineStore } from "pinia";
import { http } from "@utils/http";
import { userStore } from '@stores/UserStore'

export const permissionStore = defineStore("permissionStore", {
    state() {
        return {
            permissions: [],
        };
    },
    actions: {
        async getPermissions() {
            const user = userStore();
            const response = await http.get("/permissions", {
                headers: {
                    Authorization: `Bearer ${user.token}`,
                },
            });
            this.permissions = response.data.data;
        }
    }
});