import { defineStore } from 'pinia';
import { http } from '@utils/http';

export const userStore = defineStore('userStore', {
    state(){
        return{
            token: "",
            currentUserData: []
        }
    },
    actions:{
        async getUser(){
            const response = await http.get("/user", {
                headers: {
                    Authorization: `Bearer ${this.token}`
                }
            });
            this.currentUserData = response.data;
        },
        async login(credentials){
            const response = await http.post('/users/login', credentials)
            this.token = response.data.data.token;
        },
        logout(){
            this.token = '';
            this.currentUserData = [];
        }
    },
    getters:{
        isAuthenticated(){
            return this.token !== ""
        },
        isAdmin(){
            return this.currentUserData.is_admin === 1;
        }
    },
    persist: true,
})