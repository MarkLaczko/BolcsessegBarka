import { userStore } from "@stores/UserStore.mjs";
import { attemptStore } from "@stores/AttemptStore.mjs";

async function getPerms() {
    const path = location.pathname.split('/');
    if(path[1] != "attempt" || (path[1] == "attempt" && path[3] == "evaluate")){
        return false;
    }
    let attemptId = path[2];
    attemptStore().getUsersAttempts();
    const idx = attemptStore().attempts.findIndex(x => x.id == attemptId);
    return idx != -1;
}

export async function AttemptAccessGuard(to, from, next) {
    if (userStore().isAuthenticated && await getPerms()) {
        next()
    } 
    else {
        if (to.meta.attemptAccessGuard) {
            next({ name: 'home' })   
        }
        else {
            next()
        }
    }
}