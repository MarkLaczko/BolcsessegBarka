<template>
    <BaseLayout>
        <BaseSpinner v-if="loading" />
        <div v-if="!loading">
            <h1>{{ quiz.value.name }}</h1>
            <table class="mt-2 mb-0 table table-striped text-center">
                <tbody>
                    <tr>
                        <td class="w-50">
                            {{ messages.pages.quizPage.opens }}
                        </td>
                        <td class="w-50" v-if="quiz.value.opens != null">
                            {{ toDate(quiz.value.opens) }}
                        </td>
                        <td class="w-50" v-else>-</td>
                    </tr>
                    <tr>
                        <td class="w-50">
                            {{ messages.pages.quizPage.closes }}
                        </td>
                        <td class="w-50" v-if="quiz.value.closes != null">
                            {{ toDate(quiz.value.closes) }}
                        </td>
                        <td class="w-50" v-else>-</td>
                    </tr>
                    <tr>
                        <td class="w-50">
                            {{ messages.pages.quizPage.time }}
                        </td>
                        <td class="w-50" v-if="quiz.value.time != null">
                            {{ quiz.value.time }}
                            {{ messages.pages.quizPage.quiz.minutes }}
                        </td>
                        <td class="w-50" v-else>-</td>
                    </tr>
                    <tr>
                        <td class="w-50">
                            {{ messages.pages.quizPage.attempts }}
                        </td>
                        <td class="w-50" v-if="quiz.value.max_attempts != null">
                            {{ quiz.value.max_attempts }}
                        </td>
                        <td class="w-50" v-else>-</td>
                    </tr>
                </tbody>
            </table>
            <div class="row" v-if="!isTeacher && attempts.value != []">
                <div class="col-12 mt-2">
                    <h2>{{ messages.pages.quizPage.userAttempts }}</h2>
                    <table class="table table-responsive table-striped align-middle">
                        <thead>
                            <tr>
                                <th>{{ messages.pages.quizPage.user }}</th>
                                <th>{{ messages.pages.quizPage.begin }}</th>
                                <th>{{ messages.pages.quizPage.finished }}</th>
                                <th>{{ messages.pages.quizPage.marks }}</th>
                                <th>{{ messages.pages.quizPage.grade }}</th>
                                <th>{{ messages.pages.quizPage.evaluate }}</th>
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
                                    <button type="button" class="btn btn-primary">Értékelés</button>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            <div class="row">
                <div class="col-12" v-if="quiz.value.closes != null || (quiz.value.closes < Math.floor(Date.now() / 1000))">
                    <button type="button" class="btn btn-primary">Asd</button>
                </div>
            </div>
        </div>
    </BaseLayout>
</template>

<script setup>
import BaseLayout from '@layouts/BaseLayout.vue';
import BaseSpinner from '@components/BaseSpinner.vue';
import { quizStore } from '@stores/QuizStore';
import { languageStore } from '@stores/LanguageStore';
import { userStore } from '@stores/UserStore';
import { attemptStore } from '@stores/AttemptStore';
import { reactive, computed, onMounted } from 'vue';
import { useRoute } from 'vue-router';

const route = useRoute();

const messages = languageStore().messages;

const quiz = reactive([]);

const attempts = reactive([]);

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
    if(isTeacher){
        attempts.value = await attemptStore().getQuizAttempts(route.params.id)
    }
}

onMounted(async () => {
    quiz.value = await quizStore().getQuiz(route.params.id)
    await getAttempts()
    console.log(Math.floor(Date.now() / 1000))
})

</script>