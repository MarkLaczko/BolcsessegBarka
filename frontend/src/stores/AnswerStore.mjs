import { defineStore } from "pinia";
import { http } from "@utils/http";
import { userStore } from "@stores/UserStore";

export const answerStore = defineStore("answerStore", {
  actions: {
    async updateAnswer(id, correct, marks) {
        let data = {
            correct: correct,
            marks: marks,
        }
        const response = await http.put(`answers/${id}`, data, {
            headers: {
                Authorization: `Bearer ${userStore().token}`,
            },
        });
        return response.data.data;
    },
  }
});
