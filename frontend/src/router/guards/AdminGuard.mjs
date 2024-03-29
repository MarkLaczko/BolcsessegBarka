import { userStore } from "@stores/UserStore.mjs";

export function AdminGuard(to, from, next) {
    if (userStore().isAdmin) {
        next()
    } 
    else {
        if (to.meta.requiresAdmin) {
            next({ name: 'home' })   
        }
        else {
            next()
        }
    }
}