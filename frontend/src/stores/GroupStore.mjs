import { defineStore } from "pinia";
import { http } from "@utils/http";

export const groupStore = defineStore("groupStore", {
    state() {
        return {
            groups: [],
        };
    },
    actions: {
        async getGroups() {
            const response = await http.get("/groups", {
                headers: {
                    Authorization: `Bearer ${this.token}`,
                },
            });
            this.groups = response.data.data;
        },
        async getGroup(id) {
            const response = await http.get(`/groups/${id}`, {
                headers: {
                    Authorization: `Bearer ${this.token}`,
                },
            });
            return response.data.data;
        },
        async postGroup(data) {
            const response = await http.get(`/groups`, data, {
                headers: {
                    Authorization: `Bearer ${this.token}`,
                },
            });
            this.groups.push(response.data.data);
        },
        async updateGroup(id, data) {
            const response = await http.get(`/groups/${id}`, data, {
                headers: {
                    Authorization: `Bearer ${this.token}`,
                },
            });
            const idx = this.groups.findIndex(x => x.id == id);
            this.groups.splice(idx, 1, response.data.data);
        },
        async deleteGroup(id) {
            const response = await http.get(`/groups/${id}`, {
                headers: {
                    Authorization: `Bearer ${this.token}`,
                },
            });
            const idx = this.groups.findIndex(x => x.id == id);
            this.groups.splice(idx, 1);
        },
    }
});
