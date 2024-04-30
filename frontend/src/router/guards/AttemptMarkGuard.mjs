import { userStore } from "@stores/UserStore.mjs";
import { attemptStore } from "@stores/AttemptStore.mjs";

async function getPerms() {
    const path = location.pathname.split('/');
    if(path[1] != "attempt" && path[3] != "mark"){
        return false;
    }
    let attemptId = path[2];
    let permissions = [];
    const response = await attemptStore().getAttemptGroups(attemptId);
    for (const group of response.groups) {
        const user = group.users.find(x => x.id == userStore().currentUserData.id);
        if(user != undefined){
            permissions.push(user.member.permission);
        }
    }
    
    return permissions.includes("Tanár");
}

export async function AttemptMarkGuard(to, from, next) {
    if (userStore().isAuthenticated && await getPerms()) {

        next()
    } 
    else {
        if (to.meta.attemptMarkGuard) {
            next({ name: 'home' })   
        }
        else {
            next()
        }
    }
}