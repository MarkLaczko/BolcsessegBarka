import { defineStore } from "pinia";

export const timeStore = defineStore("timeStore", {
  state(){
    return {
      interval: null,
      now: Math.floor(Date.now() / 1000)
    }
  },
  actions: {
    interval() {
        setInterval(() => {
            this.now = Math.floor(Date.now() / 1000);
        }, 1000);
    }
  }
});
