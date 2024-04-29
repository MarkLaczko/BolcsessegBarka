<template>
    <BaseLayout>
        <div v-if="!loading">
            <div v-for="(task, index) of attemptDetails.value.quiz.tasks" :key="task.id" v-if="!loading">
                <div class="mt-2 mb-4">
                    <h3>{{ index + 1 }}. {{ task.header }}</h3>
                    <h4>{{ task.text }}</h4>
                </div>
                <div class="my-4" v-for="(subtask, index2) of task.subtasks" :key="subtask.id">
                    <div v-html="subtask.question"></div>
                    <div>
                        <input type="text" class="form-control" v-if="subtask.type == 'short_answer'" @input="event => attemptStore().addAnswer(attemptDetails.id, subtask.id, event.target.value)">
                        <div>

                        </div>
                    </div>
                </div>

            </div>
            <button type="button" class="btn btn-success" @click="submitAttempt">Elküldés</button>
        </div>
    </BaseLayout>
</template>
<script setup>
import BaseLayout from '@layouts/BaseLayout.vue';
import { attemptStore } from "@stores/AttemptStore.mjs";
import { reactive, computed, onMounted } from 'vue';
import { useRoute, useRouter } from 'vue-router';

const route = useRoute();
const router = useRouter();

const loading = computed(() => {
    return attemptDetails.value == undefined
});

const attemptDetails = reactive({});

const submitAttempt = async () => {
    try {
        await attemptStore().finishAttempt(route.params.id)
        window.location = `/quiz/${attemptDetails.value.quiz.id}`;
    } catch (error) {
        console.error('valami nem jó')
    }
};

onMounted(async () => {
    await attemptStore().getUsersAttempts();
    attemptDetails.value = attemptStore().attempts.find(x => x.id == route.params.id)
});

</script>