import { defineStore } from "pinia";
import { http } from "@utils/http";

export const courseStore = defineStore("courseStore", {
    state() {
        return {
            courses: [],
        };
    },
    actions: {
        async getCourses() {
            const response = await http.get("/courses", {
                headers: {
                    Authorization: `Bearer ${this.token}`,
                },
            });
            this.courses = response.data.data;
        },
        async getCourse(id) {
            const response = await http.get(`/courses/${id}`, {
                headers: {
                    Authorization: `Bearer ${this.token}`,
                },
            });
            return response.data.data;
        },
        async postCourse(data) {
            const response = await http.get(`/courses`, data, {
                headers: {
                    Authorization: `Bearer ${this.token}`,
                },
            });
            this.courses.push(response.data.data);
        },
        async putCourse(id, data) {
            const response = await http.get(`/courses/${id}`, data, {
                headers: {
                    Authorization: `Bearer ${this.token}`,
                },
            });
            const ids = this.courses.findIndex(x => x.id == id);
            this.courses.splice(ids, 1, response.data.data);
        },
        async destroyCourse(id) {
            const response = await http.get(`/courses/${id}`, {
                headers: {
                    Authorization: `Bearer ${this.token}`,
                },
            });
            const ids = this.courses.findIndex(x => x.id == id);
            this.courses.splice(ids, 1);
        },
    }
});
