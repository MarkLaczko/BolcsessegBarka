import axios from 'axios'

export const http = axios.create({
    baseURL: "http://localhost:8881/api",
    headers:{
        "Accept": "application/json",
        "Content-Type": "application/json" 
    }
})
