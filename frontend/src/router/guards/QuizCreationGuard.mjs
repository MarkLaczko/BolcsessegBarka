import { userStore } from "@stores/UserStore.mjs";
import { courseStore } from "@stores/CourseStore.mjs";
import { quizStore } from "@stores/QuizStore.mjs";

async function getCourse() {
    const path = location.pathname.split('/');
    if((path[1] != "quiz" && path[3] != "edit") && (path[5] != 'create-quiz')){
        return false;
    }
    
    let permissions = [];

    if(path[1] == 'quiz'){
        let quizId = path[2];
        const response = await quizStore().getQuiz(quizId);
        for (const group of response.topic.course.groups) {
            const user = group.users.find(x => x.id == userStore().currentUserData.id);
            if(user != undefined){
                permissions.push(user.member.permission);
            }
        }
        return permissions.includes("Tanár");
    }
    
    let courseId;
    if(path[5] == "create-quiz") {
        courseId = path[2];
        const response2 = await courseStore().getCourse(courseId);
        for (const group of response2.groups) {
            const user = group.users.find(x => x.id == userStore().currentUserData.id);
            if(user != undefined){
                permissions.push(user.member.permission);
            }
        }
        return permissions.includes("Tanár");
    }
}

export async function QuizCreationGuard(to, from, next) {
    if (userStore().isAuthenticated && await getCourse()) {
        next()
    } 
    else {
        if (to.meta.quizCreationGuard) {
            next({ name: 'home' })   
        }
        else {
            next()
        }
    }
}