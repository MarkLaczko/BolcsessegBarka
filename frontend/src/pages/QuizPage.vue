<template>
    <BaseLayout>
        <BaseSpinner v-if="loading" />
        <div v-if="!loading">

            <BaseDialog
                :visible="beginAttemptDialogVisible"
                :header="languageStore().messages.pages.quizPage.beginAttemptDialog.begin"
            >
                <div>
                    <p class="mb-1">{{ languageStore().messages.pages.quizPage.beginAttemptDialog.confirm }}</p>
                    <p class="mb-1" v-if="quiz.value.time != null">{{ languageStore().messages.pages.quizPage.beginAttemptDialog.time }}: {{ quiz.value.time }} {{ languageStore().messages.pages.quizPage.beginAttemptDialog.minutes }}.</p>
                    <p class="mb-1" v-if="quiz.value.max_attempts == 1">{{ languageStore().messages.pages.quizPage.beginAttemptDialog.attemptOnce }}</p>
                    <p class="mb-1" v-else>{{ languageStore().messages.pages.quizPage.beginAttemptDialog.attemptMultipleStart }} {{ quiz.value.max_attempts == null ? languageStore().messages.pages.quizPage.beginAttemptDialog.attemptMultipleMiddle : quiz.value.max_attempts }} {{ languageStore().messages.pages.quizPage.beginAttemptDialog.attemptMultipleEnd }}.</p>
                </div>
                <div class="d-flex justify-content-end align-items-center gap-2">
                    <button type="button" class="btn btn-outline-danger" @click="beginAttemptDialogVisible = false">{{ languageStore().messages.pages.quizPage.beginAttemptDialog.cancel }}</button>
                    <button type="button" class="btn btn-success text-white" @click="startAttempt">{{ languageStore().messages.pages.quizPage.beginAttemptDialog.start }}</button>
                </div>
            </BaseDialog>

            <div class="row">
                <div class="col-12">
                    <h1>{{ quiz.value.name }}</h1>
                </div>
                <div class="col-12">
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><RouterLink :to="{name: 'course', params: {id: quiz.value.topic.course.id}}">{{ quiz.value.topic.course.name }}</RouterLink></li>
                        <li class="breadcrumb-item"><RouterLink :to="{name: 'course', params: {id: quiz.value.topic.course.id}}">{{ quiz.value.topic.name }}</RouterLink></li>
                        <li class="breadcrumb-item active" aria-current="page">{{ quiz.value.name }}</li>
                    </ol>
                </div>
                <div class="col-12">
                    <table class="mt-2 mb-0 table table-striped text-center">
                        <tbody>
                            <tr>
                                <td class="w-50">
                                    {{ languageStore().messages.pages.quizPage.opens }}
                                </td>
                                <td class="w-50" v-if="quiz.value.opens != null">
                                    {{ toDate(quiz.value.opens) }}
                                </td>
                                <td class="w-50" v-else>-</td>
                            </tr>
                            <tr>
                                <td class="w-50">
                                    {{ languageStore().messages.pages.quizPage.closes }}
                                </td>
                                <td class="w-50" v-if="quiz.value.closes != null">
                                    {{ toDate(quiz.value.closes) }}
                                </td>
                                <td class="w-50" v-else>-</td>
                            </tr>
                            <tr>
                                <td class="w-50">
                                    {{ languageStore().messages.pages.quizPage.time }}
                                </td>
                                <td class="w-50" v-if="quiz.value.time != null">
                                    {{ quiz.value.time }}
                                    {{ languageStore().messages.pages.quizPage.minutes }}
                                </td>
                                <td class="w-50" v-else>-</td>
                            </tr>
                            <tr>
                                <td class="w-50">
                                    {{ languageStore().messages.pages.quizPage.attempts }}
                                </td>
                                <td class="w-50" v-if="quiz.value.max_attempts != null">
                                    {{ quiz.value.max_attempts }}
                                </td>
                                <td class="w-50" v-else>-</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            
            <div class="row" v-if="isTeacher && attempts.value != []">
                <div class="col-12 mt-2">
                    <h2>{{ languageStore().messages.pages.quizPage.userAttempts }}</h2>
                    <table class="table table-responsive table-striped align-middle">
                        <thead>
                            <tr>
                                <th>{{ languageStore().messages.pages.quizPage.user }}</th>
                                <th>{{ languageStore().messages.pages.quizPage.begin }}</th>
                                <th>{{ languageStore().messages.pages.quizPage.finished }}</th>
                                <th>{{ languageStore().messages.pages.quizPage.marks }}</th>
                                <th>{{ languageStore().messages.pages.quizPage.grade }}</th>
                                <th>{{ languageStore().messages.pages.quizPage.evaluate }}</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="attempt of attempts.value" :key="attempt.id">
                                <td>{{ attempt.user.name }}</td>
                                <td>{{ toDate(attempt.start) }}</td>
                                <td v-if="attempt.end != null">{{ toDate(attempt.end) }}</td>
                                <td v-else>-</td>
                                <td>{{ attempt.marks }}</td>
                                <td>{{ attempt.grade }}</td>
                                <td>
                                    <button type="button" class="btn btn-primary" @click="navigateToMarkPage(attempt.id)">{{ languageStore().messages.pages.quizPage.evaluate }}</button>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            <div class="row">
                <div class="col-12 mt-2 d-flex justify-content-center aling-items-center" v-if="!hasAttemptOnThisQuiz && (quiz.value.opens == null || quiz.value.opens <= Math.floor(Date.now() / 1000)) && (quiz.value.closes == null || quiz.value.closes >= Math.floor(Date.now() / 1000))">
                    <button type="button" class="btn btn-primary" @click="beginAttemptDialogVisible = true">{{ languageStore().messages.pages.quizPage.startAttempt }}</button>
                </div>
                <div class="col-12 mt-2" v-if="userAttempts.length > 0">
                    <h2>{{ languageStore().messages.pages.quizPage.ownAttempts }}</h2>
                    <table class="table table-striped table-responsive">
                        <thead>
                            <tr>
                                <th>{{ languageStore().messages.pages.quizPage.begin }}</th>
                                <th>{{ languageStore().messages.pages.quizPage.finished }}</th>
                                <th>{{ languageStore().messages.pages.quizPage.marks }}</th>
                                <th>{{ languageStore().messages.pages.quizPage.grade }}</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="attempt of attemptStore().attempts" :key="attempt.id">
                                <td>{{ toDate(attempt.start) }}</td>
                                <td v-if="attempt.end != null">{{ toDate(attempt.end) }}</td>
                                <td v-else>-</td>
                                <td>{{ attempt.marks }}</td>
                                <td>{{ attempt.grade }}</td>
                                <td v-if="attempt.end == null"><a href="#" @click="navigateToAttempt(attempt.id)">Folytatás</a></td>
                                <td v-else></td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </BaseLayout>
</template>

<script setup>
import BaseLayout from '@layouts/BaseLayout.vue';
import BaseSpinner from '@components/BaseSpinner.vue';
import BaseDialog from '@components/BaseDialog.vue';
import { useToast } from 'primevue/usetoast';
import { quizStore } from '@stores/QuizStore';
import { languageStore } from '@stores/LanguageStore';
import { userStore } from '@stores/UserStore';
import { attemptStore } from '@stores/AttemptStore';
import { ref, reactive, computed, onMounted } from 'vue';
import { useRoute } from 'vue-router';

const route = useRoute();
const toast = useToast();

const quiz = reactive([]);

const attempts = reactive([]);

const beginAttemptDialogVisible = ref(false);

const hasAttemptOnThisQuiz = computed(() => {
    return attemptStore().userAttempts.findIndex(x => x.quiz.id == route.params.id && x.user.id == userStore().currentUserData.id && x.end == null) != -1;
});

const userAttempts = reactive(attemptStore().attempts);

const loading = computed(() => {
    return quiz.value == [] || quiz.value == undefined;
});

const isTeacher = computed(() => {
    for (const group of quiz.value.topic.course.groups) {
        const user = group.users.find(x => x.id == userStore().currentUserData.id)
        if(user.member.permission == "Tanár"){
            return true;
        }
    }
    return false;
})

const toDate = (date) => {
    return new Date(date * 1000).toLocaleString();
};

const getAttempts = async () => {
    if(isTeacher.value){
        attempts.value = await attemptStore().getQuizAttempts(route.params.id)
    }
}

const startAttempt = async () => {
    try {
        const response = await attemptStore().postAttempt(route.params.id);
        window.location = `/attempt/${response.data.data.id}`
    } catch (error) {
        let toastToAdd = {
            severity: "error",
            detail: languageStore().messages.pages.quizPage.toastMessages.unexpectedError,
            life: 3000,
        };
        if (!themeStore().isDarkMode) {
            toastToAdd.styleClass = "bg-danger text-white";
        }
        else {
            toastToAdd.styleClass = "toast-danger text-white";
        }
        toast.add(toastToAdd);
    }
}

const navigateToMarkPage = (id) => {
    window.location = `/attempt/${id}/mark`;
}

const navigateToAttempt = (id) => {
    window.location = `/attempt/${id}`;
}

onMounted(async () => {
    quiz.value = await quizStore().getQuiz(route.params.id)
    await getAttempts();
    await attemptStore().getUsersAttempts();
})

</script>