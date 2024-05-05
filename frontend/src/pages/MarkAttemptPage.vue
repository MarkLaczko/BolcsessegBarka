<template>
    <BaseLayout>
        <BaseToast />
        <div v-if="!loading">
            <div class="row mb-3">
                <div class="col-12">
                    <h1>{{ attempt.value.user.name }} {{ languageStore().messages.pages.markQuizPage.attemptOf }}</h1>
                </div>
                <div class="col-12">
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><RouterLink :to="{name: 'course', params: {id: attempt.value.quiz.topic.course.id}}">{{ attempt.value.quiz.topic.course.name }}</RouterLink></li>
                        <li class="breadcrumb-item"><RouterLink :to="{name: 'course', params: {id: attempt.value.quiz.topic.course.id}}">{{ attempt.value.quiz.topic.name }}</RouterLink></li>
                        <li class="breadcrumb-item"><a href="#" @click="navigateToQuizPage">{{ attempt.value.quiz.name }}</a></li>
                        <li class="breadcrumb-item active" aria-current="page">{{ attempt.value.user.name }} {{ languageStore().messages.pages.markQuizPage.attemptOf }}</li>
                    </ol>
                </div>
                <div class="col-12">
                    <table class="table table-striped table-responsive text-center">
                        <tbody>
                            <tr>
                                <td>Kezdte</td>
                                <td>{{ toDate(attempt.value.start) }}</td>
                            </tr>
                            <tr>
                                <td>Végzett</td>
                                <td>{{ toDate(attempt.value.end) }} <span v-if="attempt.value.quiz.time != null && (attempt.value.start + (attempt.value.quiz.time * 60) < attempt.value.end)" class="badge  text-bg-danger text-white">{{ languageStore().messages.pages.markQuizPage.late }}</span></td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class="col-12">
                    <div v-for="(task, index) of tasks.value" :key="index" class="border px-2 rounded my-2">
                        <div class="my-3">
                            <h3>{{ index + 1 }}. {{ task.header }}</h3>
                            <h4>{{ task.text }}</h4>
                            <div v-for="subtask of task.subtasks" :key="subtask.id" class="my-2 pt-2 border px-2 rounded">
                                <div v-html="subtask.question"></div>
                                <div v-if="subtask.answer != undefined">
                                    <input type="text" v-if="subtask.type == 'short_answer'" :value="subtask.answer.answer" class="form-control form-control-dark" disabled >
                                    <div class="form-check" v-if="subtask.type == 'multiple_choice'" v-for="(option, index) of subtask.answer.subtask.options" :key="index">
                                        <input class="form-check-input" type="radio" :checked="subtask.answer.answer == option" :disabled="subtask.answer.answer != option">
                                        <label class="form-check-label" :for="`option${subtask.id}number${index}`">
                                            {{ index + 1 }}. {{ option }}
                                        </label>
                                    </div>
                                    <textarea v-if="subtask.type == 'essay'" class="form-control w-100" cols="30" rows="20" :value="subtask.answer.answer"></textarea>
                                    <div class="row">
                                        <div class="col-12 col-md-6 my-3">
                                            <FormKit
                                                type="number"
                                                :label="languageStore().messages.pages.markQuizPage.maxMarks"
                                                :value="subtask.maxMarks"
                                                disabled
                                                :id="`maxMarks${subtask.answer.id}`"
                                                :classes="{
                                                    label: 'form-label',
                                                    input: 'form-control form-control-dark'
                                                }"
                                            />
                                        </div>
                                        <div class="col-12 col-md-6 my-3">
                                            <FormKit
                                                type="form"
                                                :actions="false"
                                                @submit="submitMark(subtask.answer.id, subtask.correct, subtask.marks)"
                                            >
                                                <FormKit
                                                    type="number"
                                                    v-model="subtask.marks"
                                                    :label="languageStore().messages.pages.markQuizPage.marks"
                                                    :id="`marks${subtask.answer.id}`"
                                                    step="0.5"
                                                    :disabled="subtask.correct"
                                                    :validation="`required|min:0|max:${subtask.maxMarks}`"
                                                    :validation-messages="{
                                                        required: languageStore().messages.pages.editTaskPage.marksRequired,
                                                        min: languageStore().messages.pages.editTaskPage.marksMin,
                                                    }"
                                                    :classes="{
                                                        label: 'form-label',
                                                        input: 'form-control form-control-dark'
                                                    }"
                                                />

                                                <FormKit
                                                    type="checkbox"
                                                    v-model="subtask.correct"
                                                    :id="`correct${subtask.answer.id}`"
                                                    :label="languageStore().messages.pages.markQuizPage.correctAnswer"
                                                    :classes="{
                                                        label: 'form-check-label ms-2',
                                                        input: 'form-check-input'
                                                    }"
                                                    @click="subtask.marks = subtask.correct == false ? subtask.maxMarks : 0"
                                                />

                                                <FormKit
                                                    type="submit"
                                                    :label="languageStore().messages.pages.markQuizPage.save"
                                                    :id="`submit${subtask.answer.id}`"
                                                    :classes="{
                                                        input: 'btn btn-success text-white mt-2'
                                                    }"
                                                />
                                            </FormKit>
                                        </div>
                                    </div>
                                </div>
                                <div v-else>
                                    <p><i>{{ languageStore().messages.pages.markQuizPage.noAnswer }}</i></p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-12 mt-4">
                    <h2>Értékelés</h2>
                    <div class="row">
                        <div class="col-12 col-md-6">
                            <FormKit
                                type="text"
                                :label="languageStore().messages.pages.markQuizPage.overallMarks"
                                v-model="overallText"
                                disabled
                                id="overallMarks"
                                :classes="{
                                    label: 'form-label',
                                    input: 'form-control form-control-dark'
                                }"
                            />
                        </div>
                        <div class="col-12 col-md-6">
                            <FormKit
                                type="form"
                                :actions="false"
                                @submit="submitGrade"
                            >
                                <FormKit
                                    type="text"
                                    v-model="attempt.value.grade"
                                    :label="languageStore().messages.pages.markQuizPage.grade"
                                    id="grade"
                                    validation="required|max:10"
                                    :validation-messages="{
                                        required: languageStore().messages.pages.editTaskPage.marksRequired,
                                        max: languageStore().messages.pages.editTaskPage.marksMin,
                                    }"
                                    :classes="{
                                        label: 'form-label',
                                        input: 'form-control form-control-dark'
                                    }"
                                />

                                <div class="d-flex justify-content-end align-items-center gap-1">
                                    <button type="button" class="btn btn-secondary mt-2" @click="navigateToQuizPage">
                                        {{ languageStore().messages.pages.markQuizPage.finish }}
                                    </button>

                                    <FormKit
                                        type="submit"
                                        :label="languageStore().messages.pages.markQuizPage.save"
                                        id="submitGrade"
                                        :classes="{
                                            input: 'btn btn-success text-white mt-2'
                                        }"
                                    />
                                </div>
                            </FormKit>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </BaseLayout>
</template>

<script setup>
import BaseLayout from '@layouts/BaseLayout.vue';
import BaseToast from '@components/BaseToast.vue';
import { attemptStore } from '@stores/AttemptStore.mjs';
import { answerStore } from '@stores/AnswerStore.mjs';
import { languageStore } from '@stores/LanguageStore.mjs';
import { themeStore } from '@stores/ThemeStore.mjs';
import { ref, reactive, computed, onMounted } from 'vue';
import { useRoute } from 'vue-router';
import { useToast } from 'primevue/usetoast';

const route = useRoute();
const toast = useToast();

const attempt = reactive({});

const tasks = reactive({});

const loading = computed(() => {
    return attempt.value == undefined && tasks.value == undefined
});

const maxMarks = ref(0);

const overallMarks = ref(0);

const overallText = ref("");

const calculateMarks = () => {
    let sum = 0;
    for (const task of tasks.value) {
        for (const subtask of task.subtasks) {
            sum += Number(subtask.maxMarks);
        }
    }
    maxMarks.value = sum;

    sum = 0;
    for (const task of tasks.value) {
        for (const subtask of task.subtasks) {
            sum += Number(subtask.marks);
        }
    }
    overallMarks.value = sum;

    let percentage = ((Math.round((overallMarks.value / maxMarks.value * 100)) * 100) / 100).toFixed(0);
    overallText.value = `${overallMarks.value}/${maxMarks.value} (${percentage}%)`
}

const fillTasks = () => {
    let quizTasks = [];
    for (const task of attempt.value.quiz.tasks) {
        let subtasks = [];
        for (const subtask of task.subtasks) {
            let answer = attempt.value.answers.find(x => x.subtask.id == subtask.id);
            let subtaskDetails = {
                question: subtask.question,
                type: subtask.type,
                answer: answer,
                maxMarks: subtask.marks,
                marks: answer != undefined && answer.marks != null ? answer.marks : 0,
                correct: answer != undefined && answer.correct == 1 ? true : false,
                id: subtask.id
            }
            subtasks.push(subtaskDetails);
        }
        let taskDetails = {
            header: task.header,
            text: task.text,
            subtasks: subtasks
        }
        quizTasks.push(taskDetails);
    }
    tasks.value = quizTasks;
};

const submitMark = (id, correct, marks) => {
    try {
        answerStore().updateAnswer(id, correct, marks)
        let toastToAdd = {
            severity: "success",
            detail: languageStore().messages.pages.markQuizPage.toastMessages.updatedMark,
            life: 3000,
        };
        if (!themeStore().isDarkMode) {
            toastToAdd.styleClass = "bg-success text-white";
        }
        else {
            toastToAdd.styleClass = "toast-success text-white";
        }
        toast.add(toastToAdd);
        calculateMarks();
    } catch (error) {
        let toastToAdd = {
            severity: "error",
            detail: languageStore().messages.pages.markQuizPage.toastMessages.failedUpdateMark,
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

const submitGrade = async () => {
    try {
        await attemptStore().grade(attempt.value.id, overallMarks.value, attempt.value.grade);
        let toastToAdd = {
            severity: "success",
            detail: languageStore().messages.pages.markQuizPage.toastMessages.graded,
            life: 3000,
        };
        if (!themeStore().isDarkMode) {
            toastToAdd.styleClass = "bg-success text-white";
        }
        else {
            toastToAdd.styleClass = "toast-success text-white";
        }
        toast.add(toastToAdd);
    } catch (error) {
        let toastToAdd = {
            severity: "error",
            detail: languageStore().messages.pages.markQuizPage.toastMessages.failedToGrade,
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

const toDate = (date) => {
    return new Date(date * 1000).toLocaleString();
};

const navigateToQuizPage = () => {
    window.location = `/quiz/${attempt.value.quiz.id}`;
};

onMounted(async () => {
    attempt.value = await attemptStore().getAttempt(route.params.id);
    fillTasks();
    calculateMarks();
});
</script>

<style>
    img {
        max-width: 100%;
    }
</style>