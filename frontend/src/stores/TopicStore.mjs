import { defineStore } from "pinia";
import { http } from "@utils/http";
import { userStore } from "@stores/UserStore";

export const topicStore = defineStore("topicStore", {
  actions: {
    async getTopics() {
      const user = userStore();
      const response = await http.get("/topics", {
        headers: {
          Authorization: `Bearer ${user.token}`,
        },
      });
      return response.data.data;
    },
    async getTopic(id) {
      const user = userStore();
      const response = await http.get(`/topics/${id}`, {
        headers: {
          Authorization: `Bearer ${user.token}`,
        },
      });
      return response.data.data;
    },
    async postTopic(data) {
      const user = userStore();
      const response = await http.post(`/topics`, data, {
        headers: {
          Authorization: `Bearer ${user.token}`,
        },
      });

      return response;
    },
    async putTopic(id, data) {
      const user = userStore();
      const response = await http.put(`/topics/${id}`, data, {
        headers: {
          Authorization: `Bearer ${user.token}`,
        },
      });

      return response.data.data;
    },
    async destroyTopic(id) {
      const user = userStore();
      const response = await http.delete(`/topics/${id}`, {
        headers: {
          Authorization: `Bearer ${user.token}`,
        },
      });

      return response.data.data;
    },
  },
});
