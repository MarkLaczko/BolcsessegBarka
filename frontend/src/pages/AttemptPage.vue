<template>
    <BaseLayout>
        <div v-if="!loading" class="mb-5">
            <h1>{{ attemptDetails.value.quiz.name }}</h1>
            <div class="my-3" v-if="attemptDetails.value.quiz.time != null">
                <span class="p-2 bg-primary text-white fw-bold rounded-pill">
                    {{ convertSecondstoTime((attemptDetails.value.start + attemptDetails.value.quiz.time * 60) - timeStore().now) }}
                </span>
            </div>
            <div v-for="(task, index) of attemptDetails.value.quiz.tasks" :key="task.id" v-if="!loading" class="border px-2 rounded my-2">
                <div class="mt-2 mb-4">
                    <h3>{{ index + 1 }}. {{ task.header }}</h3>
                    <h4>{{ task.text }}</h4>
                </div>
                <div class="my-4" v-for="(subtask, index2) of task.subtasks" :key="subtask.id">
                    <div v-html="subtask.question"></div>
                    <div>
                        <input type="text" class="form-control form-control-dark" v-if="subtask.type == 'short_answer'" @input="event => attemptStore().addAnswer(route.params.id, subtask.id, event.target.value)">
                        <div v-if="subtask.type == 'multiple_choice'">
                            <div class="form-check" v-for="(option, index) of subtask.options" :key="index">
                                <input class="form-check-input" type="radio" :value="option" :name="`option${subtask.id}`" :id="`option${subtask.id}number${index}`" @click="event => attemptStore().addAnswer(route.params.id, subtask.id, event.target.value)">
                                <label class="form-check-label" :for="`option${subtask.id}number${index}`">
                                    {{ index + 1 }}. {{ option }}
                                </label>
                            </div>
                            <div class="form-check" v-if="subtaskAnswer(subtask.id) != undefined && subtaskAnswer(subtask.id).answer != ''">
                                <input class="form-check-input" type="radio" :value="option" :name="`option${subtask.id}`" :id="`option${subtask.id}remove`" @click="event => attemptStore().addAnswer(route.params.id, subtask.id, '')">
                                <label class="form-check-label" :for="`option${subtask.id}remove`">
                                    {{ messages.pages.attemptPage.removeOption }}
                                </label>
                            </div>
                        </div>
                        <textarea v-if="subtask.type == 'essay'" class="form-control w-100" cols="30" rows="20" @input="event => attemptStore().addAnswer(route.params.id, subtask.id, event.target.value)"></textarea>
                    </div>
                </div>

            </div>
            <button type="button" class="btn btn-success text-white" @click="submitAttempt">Elküldés</button>
        </div>
    </BaseLayout>
</template>
<script setup>
import BaseLayout from '@layouts/BaseLayout.vue';
import { attemptStore } from "@stores/AttemptStore.mjs";
import { timeStore } from "@stores/TimeStore.mjs";
import { languageStore } from '@stores/LanguageStore.mjs';
import { reactive, computed, onMounted } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { useToast } from 'primevue/usetoast';

const route = useRoute();
const router = useRouter();
const toast = useToast();

const messages = reactive(languageStore().messages);

const loading = computed(() => {
    return attemptDetails.value == undefined
});

const attemptDetails = reactive({});

const subtaskAnswer = (id) => {
    return attemptStore().userAttempts.find(x => x.id == attemptDetails.value.id).answers.find(x => x.subtask_id == id);
}

const submitAttempt = async () => {
    try {
        await attemptStore().finishAttempt(route.params.id)
        window.location = `/quiz/${attemptDetails.value.quiz.id}`;
    } catch (error) {
        let toastToAdd = {
            severity: "error",
            detail: messages.pages.attemptPage.toastMessages.failedToSubmit,
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
};

const convertSecondstoTime = (sec) => {
    let dateObj = new Date(sec * 1000);
    let hours = dateObj.getUTCHours();
    let minutes = dateObj.getUTCMinutes();
    let seconds = dateObj.getSeconds();

    return hours.toString().padStart(2, '0') + ':' + minutes.toString().padStart(2, '0') + ':' + seconds.toString().padStart(2, '0');
}

onMounted(async () => {
    await attemptStore().getUsersAttempts();
    attemptDetails.value = attemptStore().attempts.find(x => x.id == route.params.id)
});

</script>

<style>
    img {
        max-width: 100%;
    }
</style>