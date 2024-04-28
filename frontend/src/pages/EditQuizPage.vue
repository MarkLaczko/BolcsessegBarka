<template>
    <BaseLayout>
        <BaseToast />
        <BaseConfirmDialog />
        <BaseSpinner :loading="loading" />
        <div v-if="!loading">
            <h1 v-if="language == 'HU'">{{ quiz.value.name }} {{ messages.pages.editQuizPage.title }}</h1>
            <h1 v-if="language == 'EN'">{{ messages.pages.editQuizPage.title }} {{ quiz.value.name }}</h1>
            <div class="row">
                <div class="col-12">
                    <FormKit
                        type="form"
                        :actions="false"
                        :classes="{
                            message: 'text-end'
                        }"
                        :incomplete-message="messages.pages.editQuizPage.validationMessages.matchAllValidationMessage"
                        @submit="putForm"
                    >
                        <div class="row">
                            <div class="col-12 my-1">
                                <FormKit
                                    type="text"
                                    :label="messages.pages.editQuizPage.name"
                                    name="name"
                                    id="name"
                                    v-model="quiz.value.name"
                                    :classes="{
                                        input: 'form-control',
                                        label: 'form-label',
                                    }"
                                    validation="required|length:0,100"
                                    :validation-messages="{
                                        required: messages.pages.editQuizPage.validationMessages.nameRequired,
                                        length: messages.pages.editQuizPage.validationMessages.nameLength,
                                    }"
                                />
                            </div>
                            <div class="col-12 col-md-6 col-lg-3 my-1">
                                <FormKit
                                    type="number"
                                    :label="messages.pages.editQuizPage.attempts"
                                    name="max_attempts"
                                    id="max_attempts"
                                    v-model="quiz.value.max_attempts"
                                    :classes="{
                                        input: 'form-control',
                                        label: 'form-label',
                                    }"
                                    validation="min:0"
                                    :validation-messages="{
                                        min: messages.pages.editQuizPage.validationMessages.attemptsMin,
                                    }"
                                />
                            </div>
                            <div class="col-12 col-md-6 col-lg-3 my-1">
                                <FormKit
                                    type="datetime-local"
                                    :label="messages.pages.editQuizPage.opens"
                                    name="opens"
                                    id="opens"
                                    v-model="quiz.value.opens"
                                    :classes="{
                                        input: 'form-control',
                                        label: 'form-label',
                                    }"
                                />
                            </div>
                            <div class="col-12 col-md-6 col-lg-3 my-1">
                                <FormKit
                                    type="datetime-local"
                                    :label="messages.pages.editQuizPage.closes"
                                    name="closes"
                                    id="closes"
                                    v-model="quiz.value.closes"
                                    :classes="{
                                        input: 'form-control',
                                        label: 'form-label',
                                    }"
                                />
                            </div>
                            <div class="col-12 col-md-6 col-lg-3 my-1">
                                <FormKit
                                    type="number"
                                    :label="messages.pages.editQuizPage.time"
                                    name="time"
                                    id="time"
                                    v-model="quiz.value.time"
                                    :classes="{
                                        input: 'form-control',
                                        label: 'form-label',
                                    }"
                                    validation="min:0"
                                    :validation-messages="{
                                        min: messages.pages.editQuizPage.validationMessages.time,
                                    }"
                                />
                            </div>
                            <div class="col-12 my-1 d-flex justify-content-end">
                                <FormKit
                                    type="submit"
                                    id="submit"
                                    :label="messages.pages.editQuizPage.submit"
                                    :classes="{
                                        input: 'btn btn-success',
                                    }"
                                />
                            </div>
                        </div>
                    </FormKit>
                </div>
                <div class="col-12">
                    <h2>{{ messages.pages.editQuizPage.tasks }}</h2>
                    <div class="card w-100 my-2 p-1 flex-row align-items-center gap-2 flex-wrap" v-for="(task, index) of tasks.value" :key="index">
                        <span class="badge text-bg-info text-white fs-5">{{index + 1}}.</span> <span class="flex-grow-1 fw-bold">{{ task.header }}</span>
                        <div>
                            <button class="btn btn-secondary text-white mx-1" style="width: 50px" @click="moveItem(index, index - 1)" v-if="index != 0"><i class="fa-solid fa-arrow-up"></i></button>
                            <button class="btn btn-secondary text-white mx-1" style="width: 50px" @click="moveItem(index, index + 1)" v-if="index < tasks.value.length - 1"><i class="fa-solid fa-arrow-down"></i></button>
                            <button class="btn btn-secondary text-white mx-1" style="width: 50px" @click="moveItem(index, 0)" v-else><i class="fa-solid fa-arrows-up-to-line"></i></button>
                            <button class="btn btn-secondary text-white mx-1" style="width: 50px" @click="navigateToEditTaskPage(task.id)"><i class="fa-solid fa-pen"></i></button>
                            <button class="btn btn-danger text-white mx-1" style="width: 50px" @click="confirmDeleteTask(task.id, index)"><i class="fa-solid fa-x"></i></button>
                        </div>
                    </div>
                    <div class="card w-100 my-2 p-1">
                        <RouterLink class="btn btn-info text-white" :to="{ name: 'createTask', params: { id: quiz.value.id } }"><i class="fa-solid fa-plus"></i></RouterLink>
                    </div>
                </div>
            </div>
        </div>
    </BaseLayout>
</template>

<script setup>
import BaseLayout from '@layouts/BaseLayout.vue'
import BaseToast from '@components/BaseToast.vue';
import BaseConfirmDialog from "@components/BaseConfirmDialog.vue";
import BaseSpinner from "@components/BaseSpinner.vue";
import { reactive, computed, onMounted } from 'vue';
import { useRoute, useRouter } from 'vue-router'
import { http } from '@utils/http';
import { useToast } from 'primevue/usetoast';
import { useConfirm } from "primevue/useconfirm";
import { userStore } from "@stores/UserStore";
import { quizStore } from "@stores/QuizStore";
import { themeStore } from '@stores/ThemeStore';
import { languageStore } from '@stores/LanguageStore';

const route = useRoute();
const router = useRouter();
const toast = useToast();
const confirm = useConfirm();

const quiz = reactive({});
const tasks = reactive([]);

const loading = computed(() => {
    return quiz.value == undefined;
});

const messages = languageStore().messages;

const language = languageStore().language;

const getQuiz = async () => {
    quiz.value = await quizStore().getQuiz(route.params.id);

    let opensDate = new Date(quiz.value.opens * 1000);
    quiz.value.opens = quiz.value.opens == null ? null : `${opensDate.getFullYear()}-${(opensDate.getMonth() + 1) < 10 ? '0' : ''}${opensDate.getMonth() + 1}-${opensDate.getDate() < 10 ? '0' : ''}${opensDate.getDate()}T${opensDate.getHours() < 10 ? '0' : ''}${opensDate.getHours()}:${opensDate.getMinutes() < 10 ? '0' : ''}${opensDate.getMinutes()}:00`;
    let closesDate = new Date(quiz.value.closes * 1000);
    quiz.value.closes = quiz.value.closes == null ? null : `${closesDate.getFullYear()}-${(closesDate.getMonth() + 1) < 10 ? '0' : ''}${closesDate.getMonth() + 1}-${closesDate.getDate() < 10 ? '0' : ''}${closesDate.getDate()}T${closesDate.getHours() < 10 ? '0' : ''}${closesDate.getHours()}:${closesDate.getMinutes() < 10 ? '0' : ''}${closesDate.getMinutes()}:00`;

    const response2 = await http.get(`quizzes/${route.params.id}/tasks`, {
        headers: {
            Authorization: `Bearer ${userStore().token}`,
        },
    });
    tasks.value = response2.data.data;
};

const putForm = async () => {
    try {
        quiz.value.opens = quiz.value.opens == null ? null : Math.floor(new Date(quiz.value.opens).getTime() / 1000);
        quiz.value.closes = quiz.value.closes == null ? null : Math.floor(new Date(quiz.value.closes).getTime() / 1000);
        const response = await quizStore().putQuiz(route.params.id, quiz.value);
        if(!response.id){
            throw ('Error')
        }

        let opensDate = new Date(quiz.value.opens * 1000);
        quiz.value.opens = quiz.value.opens == null ? null : `${opensDate.getFullYear()}-${(opensDate.getMonth() + 1) < 10 ? '0' : ''}${opensDate.getMonth() + 1}-${opensDate.getDate() < 10 ? '0' : ''}${opensDate.getDate()}T${opensDate.getHours() < 10 ? '0' : ''}${opensDate.getHours()}:${opensDate.getMinutes() < 10 ? '0' : ''}${opensDate.getMinutes()}:00`;
        let closesDate = new Date(quiz.value.closes * 1000);
        quiz.value.closes = quiz.value.closes == null ? null : `${closesDate.getFullYear()}-${(closesDate.getMonth() + 1) < 10 ? '0' : ''}${closesDate.getMonth() + 1}-${closesDate.getDate() < 10 ? '0' : ''}${closesDate.getDate()}T${closesDate.getHours() < 10 ? '0' : ''}${closesDate.getHours()}:${closesDate.getMinutes() < 10 ? '0' : ''}${closesDate.getMinutes()}:00`;

        let toastToAdd = {
          severity: "success",
          detail: messages.pages.editQuizPage.toastmessages.pages.updateSuccess,
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
          detail: messages.pages.editQuizPage.toastmessages.pages.updateUnexpectedError,
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

const moveItem = async (from, to) => {
    try {
        tasks.value[from].order = to;
        tasks.value[to].order = from;
        tasks.value[from].quiz_id = quiz.value.id;
        tasks.value[to].quiz_id = quiz.value.id;
    
        await http.put(`tasks/${tasks.value[from].id}`, tasks.value[from], {
            headers: {
                Authorization: `Bearer ${userStore().token}`,
            },
        });
        await http.put(`tasks/${tasks.value[to].id}`, tasks.value[to], {
            headers: {
                Authorization: `Bearer ${userStore().token}`,
            },
        });
        
        let helper = tasks.value.splice(from, 1)[0];
        tasks.value.splice(to, 0, helper);
    } catch (error) {
        let toastToAdd = {
          severity: "error",
          detail: messages.pages.editQuizPage.toastmessages.pages.changeOrderUnexpectedError,
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

const confirmDeleteTask = async (id, index) => {
    await confirm.require({
        message: messages.pages.editQuizPage.confirmDialogs.deleteTask,
        icon: 'pi pi-exclamation-triangle',
        rejectClass: 'btn btn-danger',
        acceptClass: 'btn btn-success ',
        rejectLabel: messages.pages.editQuizPage.confirmDialogs.cancel,
        acceptLabel: messages.pages.editQuizPage.confirmDialogs.confirm,
        accept: async () => {
            try {
                await http.delete(`/tasks/${id}`, {
                    headers: {
                        Authorization: `Bearer ${userStore().token}`,
                    },
                });
                
                tasks.value.splice(index, 1);

                let toastToAdd = {
                    severity: "success",
                    detail: messages.pages.editQuizPage.toastmessages.pages.deleteSuccess,
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
                    detail: messages.pages.editQuizPage.toastmessages.pages.deleteUnexpectedError,
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
    });
}

const navigateToEditTaskPage = (id) => {
    window.location = `/quiz/${quiz.value.id}/edit/${id}`
}

onMounted(() => {
    getQuiz()
})

</script>