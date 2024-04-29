import { defineStore } from "pinia";
import { http } from "@utils/http";
import { userStore } from "@stores/UserStore";

export const attemptStore = defineStore("attemptStore", {
  actions: {
    async getQuizAttempts(id) {
        const user = userStore();
        const response = await http.get(`quizzes/${id}/attempts`, {
            headers: {
                Authorization: `Bearer ${user.token}`,
            },
        });
        return response.data.data;
    },
  },
});
