import { userStore } from "@stores/UserStore.mjs";
import { courseStore } from "@stores/CourseStore.mjs";
import { http } from '@utils/http';

async function getCourse() {
    const path = location.pathname.split('/');
    if(path[5] != "create-quiz" && (path[1] != "quiz" && path[3] != "edit")){
        return false;
    }
    let courseId;
    if(path[5] == "create-quiz") {
        courseId = path[2];
    }
    else {
        await courseStore().getCourses();
        for (const course of courseStore().courses) {
            for (const topic of course.topics) {
                const quiz = topic.quizzes.find(x => x.id == path[2]);
                if(quiz != undefined) {
                    courseId = course.id;
                }
            }
        }
        if(courseId == null) {
            return false;
        }
    }
    let groups = [];
    const response = await http.get('/groups', {
        headers: {
            Authorization: `Bearer ${userStore().token}`,
        },
    });
    groups = response.data.data;
    let groupContainsCourse = groups.filter(x => x.courses.find(y => y.id == courseId));

    let permissions = [];
    for(const group of groupContainsCourse) {
        let userGroups = group.users.find(x => x.id == userStore().currentUserData.id);
        if(userGroups == undefined){
            return false;
        }
        permissions.push(userGroups.member.permission);
    }

    return permissions.includes("Tanár");
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