import { defineStore } from 'pinia';
import { http } from '@utils/http';

export const userStore = defineStore('userStore', {
    state(){
        return{
            token: "",
            name: '',
            email: '',
            global_role: '',
        }
    },
    actions:{
        async login(credentials){
            const response = await http.post('/users/login', credentials)
            this.token = response.data.data.token;
        }
    },
    getters:{
        isAuthenticated(){
            return this.token !== ""
        }
    },
    persist: true,
})