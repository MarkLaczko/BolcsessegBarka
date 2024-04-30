import { defineStore } from "pinia";
import { http } from "@utils/http";
import { userStore } from "@stores/UserStore";
import { timeStore } from '@stores/TimeStore';

export const attemptStore = defineStore("attemptStore", {
  state(){
    return {
      userAttempts: [],
      attempts: [],
    }
  },
  actions: {
    async getQuizAttempts(id) {
      const response = await http.get(`quizzes/${id}/attempts`, {
          headers: {
              Authorization: `Bearer ${userStore().token}`,
          },
      });
      return response.data.data;
    },
    async getUsersAttempts() {
      const response = await http.get(`user/attempts`, {
          headers: {
              Authorization: `Bearer ${userStore().token}`,
          },
      });
      this.attempts = response.data.data;
    },
    async postAttempt(id){
      const quiz_id = id;
      const response = await http.post(`attempts`, { quiz_id: quiz_id }, {
        headers: {
          Authorization: `Bearer ${userStore().token}`,
        },
      });
      let attempt = response.data.data;
      attempt.finish_by = attempt.start + (attempt.quiz.time * 60);
      attempt.answers = [];
      this.userAttempts.push(attempt);
      return response;
    },
    async finishAttemptSend(id){
      const response = await http.put(`/attempts/${id}/finish`, {}, {
        headers: {
          Authorization: `Bearer ${userStore().token}`,
        },
      })
    },
    async finishSendAnswers(id){
      let attempt = this.userAttempts.find(x => x.id == id);
      if(attempt.answers.length > 0){
        let data = {
          attempt_id: attempt.id,
          bulk: attempt.answers
        };
        const bulkResponse = await http.post('/answers/bulk', data,{
          headers: {
            Authorization: `Bearer ${userStore().token}`,
          },
        })
      }
    },
    async finishAttempt(id) {
      let idx = this.userAttempts.findIndex(x => x.id == id);
      await this.finishSendAnswers(id);
      await this.finishAttemptSend(id);
      this.userAttempts.splice(idx, 1);
    },
    async addAnswer(attemptId, subtaskId, answer) {
      let idx = this.userAttempts.findIndex(x => x.id == attemptId);
      let idx2 = this.userAttempts[idx].answers.findIndex(x => x.subtask_id == subtaskId)
      if(idx2 == -1){
        this.userAttempts[idx].answers.push({
          attempt_id: attemptId,
          subtask_id: subtaskId,
          answer: answer
        });
      }
      else {
        this.userAttempts[idx].answers[idx2] = {
          attempt_id: attemptId,
          subtask_id: subtaskId,
          answer: answer
        };
      }
    },
    checkAttemptsForExpiry(){
      setInterval(async () => {
        for (const attempt of this.userAttempts) {
          if(attempt.finish_by <= timeStore().now){
            let idx = this.userAttempts.findIndex(x => x.id == attempt.id);
            await this.finishSendAnswers(attempt.id);
            await this.finishAttemptSend(attempt.id);
            this.userAttempts.splice(idx, 1);
            const path = location.pathname.split('/');
            if(path[1] == 'attempt' && path[2] == attempt.id){
              window.location = `/quiz/${attempt.quiz.id}`;
            }
          }
        }
      }, 1000);
    }
  },
  persist: true
});
