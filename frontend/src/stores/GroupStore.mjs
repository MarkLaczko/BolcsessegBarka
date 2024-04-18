import { defineStore } from "pinia";
import { http } from "@utils/http";
import { userStore } from '@stores/UserStore'

export const groupStore = defineStore("groupStore", {
    state() {
        return {
            groups: [],
        };
    },
    actions: {
        async getGroups() {
            const user = userStore();
            const response = await http.get("/groups", {
                headers: {
                    Authorization: `Bearer ${user.token}`,
                },
            });
            this.groups = response.data.data;
        },
        async getGroup(id) {
            const user = userStore();
            const response = await http.get(`/groups/${id}`, {
                headers: {
                    Authorization: `Bearer ${user.token}`,
                },
            });
            return response.data.data;
        },
        async postGroup(data) {
            const user = userStore();
            const response = await http.post(`/groups`, data, {
                headers: {
                    Authorization: `Bearer ${user.token}`,
                },
            });
            this.groups.push(response.data.data);
        },
        async updateGroup(data) {
            const user = userStore();
            const response = await http.put(`/groups/${data.id}`, data, {
                headers: {
                    Authorization: `Bearer ${user.token}`,
                },
            });
            const idx = this.groups.findIndex(x => x.id == data.id);
            this.groups.splice(idx, 1, response.data.data);
        },
        async deleteGroup(id) {
            const user = userStore();
            const response = await http.delete(`/groups/${id}`, {
                headers: {
                    Authorization: `Bearer ${user.token}`,
                },
            });
            const idx = this.groups.findIndex(x => x.id == id);
            this.groups.splice(idx, 1);
        },
        async bulkDeleteGroups(data) {
            const toDelete = data.map((x) => x.id);
            const user = userStore();
            const response = await http.delete(`/groups`, {
                headers: {
                    Authorization: `Bearer ${user.token}`,
                },
                data: {
                    bulk: toDelete
                }
            });
            this.groups = this.groups.filter((x) => !toDelete.includes(x.id));
        },
    }
});
