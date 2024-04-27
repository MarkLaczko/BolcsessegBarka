import { defineStore } from "pinia";
import { http } from "@utils/http";
import { userStore } from "@stores/UserStore";

export const quizStore = defineStore("quizStore", {
  state() {
    return {
      
    };
  },
  actions: {
    async getQuiz(id) {
        const user = userStore();
        const response = await http.get(`quizzes/${id}`, {
            headers: {
                Authorization: `Bearer ${user.token}`,
            },
        });
        return response.data.data;
    },
    async postQuiz(data) {
        const user = userStore();
        const response = await http.post(`quizzes`, data, {
                headers: {
                    Authorization: `Bearer ${user.token}`,
                },
            });
        return response.data.data;
    },
    async putQuiz(id, data) {
        const user = userStore();
        const response = await http.put(`/quizzes/${id}`, data, {
            headers: {
                Authorization: `Bearer ${user.token}`,
            },
        });
        return response.data.data;
    },
    async destroyQuiz(id) {
        const user = userStore();
        const response = await http.delete(`/quizzes/${id}`, {
            headers: {
                Authorization: `Bearer ${user.token}`,
            },
        });
    },
  },
});
