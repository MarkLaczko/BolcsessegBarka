import { userStore } from "@stores/UserStore.mjs";
import { quizStore } from "@stores/QuizStore.mjs";

async function getPerms() {
    const path = location.pathname.split('/');
    if((path[1] != "quiz" && path[3] != "edit") || (path[1] == "quiz" && path[3] == "edit")){
        return false;
    }
    let quizId = path[2];
    let permissions = [];
    const response = await quizStore().getQuiz(quizId);
    for (const group of response.topic.course.groups) {
        const user = group.users.find(x => x.id == userStore().currentUserData.id);
        if(user != undefined){
            permissions.push(user.member.permission);
        }
    }

    return permissions.includes("Tanár") || permissions.includes("Tanuló");
}

export async function QuizAccessGuard(to, from, next) {
    if (userStore().isAuthenticated && await getPerms()) {
        next()
    } 
    else {
        if (to.meta.quizAccessGuard) {
            next({ name: 'home' })   
        }
        else {
            next()
        }
    }
}