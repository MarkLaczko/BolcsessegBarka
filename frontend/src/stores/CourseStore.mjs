import { defineStore } from "pinia";
import { http } from "@utils/http";
import { userStore } from "@stores/UserStore";

export const courseStore = defineStore("courseStore", {
  state() {
    return {
      courses: [],
    };
  },
  actions: {
    async getCourses() {
      const user = userStore();
      const response = await http.get("/courses", {
        headers: {
          Authorization: `Bearer ${user.token}`,
        },
      });
      this.courses = response.data.data;
    },
    async getCourse(id) {
      const user = userStore();
      const response = await http.get(`/courses/${id}`, {
        headers: {
          Authorization: `Bearer ${user.token}`,
        },
      });
      return response.data.data;
    },
    async postCourse(data) {
      const user = userStore();
      const response = await http.post(`/courses`, data, {
        headers: {
          Authorization: `Bearer ${user.token}`,
        },
      });
      this.courses.push(response.data.data);
    },
    async putCourse(id, data) {
      const user = userStore();
      const response = await http.put(`/courses/${id}`, data, {
        headers: {
          Authorization: `Bearer ${user.token}`,
        },
      });
      const ids = this.courses.findIndex((x) => x.id == id);
      this.courses.splice(ids, 1, response.data.data);
    },
    async destroyCourse(id) {
      const user = userStore();
      const response = await http.delete(`/courses/${id}`, {
        headers: {
          Authorization: `Bearer ${user.token}`,
        },
      });
      const ids = this.courses.findIndex((x) => x.id == id);
      this.courses.splice(ids, 1);
    },
  },
});
