import { defineStore } from "pinia";
import { http } from "@utils/http";
import { userStore } from "@stores/UserStore";

export const noteStore = defineStore("noteStore", {
  state() {
    return {
      notes: [],
    };
  },
  actions: {
    async getNotes() {
      const user = userStore();
      const response = await http.get("/notes", {
        headers: {
          Authorization: `Bearer ${user.token}`,
        },
      });
      this.notes = response.data.data;
    },
    async getNote(id) {
      const user = userStore();
      const response = await http.get(`/notes/${id}`, {
        headers: {
          Authorization: `Bearer ${user.token}`,
        },
      });
      return response.data.data;
    },
    async postNote(data) {
      const user = userStore();
      const response = await http.post(`/notes`, data, {
        headers: {
          Authorization: `Bearer ${user.token}`,
        },
      });
      this.notes.push(response.data.data);
      return response.data.data;
    },
    async putNote(id, data) {
      const user = userStore();
      const response = await http.put(`/notes/${id}`, data, {
        headers: {
          Authorization: `Bearer ${user.token}`,
        },
      });

      const idx = this.notes.findIndex((x) => x.id == id);
      this.notes.splice(idx, 1, response.data.data);
      return response.data.data;
    },
    async destroyNote(id) {
      const user = userStore();
      const response = await http.delete(`/notes/${id}`, {
        headers: {
          Authorization: `Bearer ${user.token}`,
        },
      });

      const idx = this.notes.findIndex((x) => x.id == id);
      this.notes.splice(idx, 1);
    },
    async getCurrentNotes() {
      const user = userStore();
      const response = await http.get("/getCurrentNotes", {
        headers: {
          Authorization: `Bearer ${user.token}`,
        },
      });

      return response.data.data;
    },
    async getTeacherNotes() {
      const user = userStore();
      const response = await http.get("/getTeacherNotes", {
        headers: {
          Authorization: `Bearer ${user.token}`,
        },
      });

      return response.data.data;
    }
  },
});
