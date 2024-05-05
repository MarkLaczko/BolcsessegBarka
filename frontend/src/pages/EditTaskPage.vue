<template>
    <BaseLayout>
        <BaseToast />
        <BaseConfirmDialog />
        <BaseSpinner :loading="loading" />
        <div v-if="!loading">
            <h1>{{ languageStore().messages.pages.editTaskPage.title }}</h1>
            <div class="row">
                <div class="col-12">
                    <RouterLink :to="{ name: 'editQuiz', params: { id: route.params.quizId } }" class="btn btn-outline-secondary"><i class="fa-solid fa-arrow-left-long"></i> {{ quiz.value.name }}</RouterLink>
                </div>
                <div class="col-12">
                    <div class="row">
                        <FormKit
                            type="form"
                            id="form"
                            :actions="false"
                            :incomplete-message="languageStore().messages.pages.editTaskPage.validationMessages.matchAllValidationMessage"
                            @submit="putTask"
                        >
                            <div class="col-12 my-1">
                                <FormKit
                                    type="text"
                                    :label="languageStore().messages.pages.editTaskPage.header"
                                    name="header"
                                    id="header"
                                    v-model="form.value.header"
                                    :classes="{
                                        input: 'form-control form-control-dark',
                                        label: 'form-label',
                                    }"
                                    autocomplete="off"
                                    validation="required|length:0,100"
                                    :validation-messages="{
                                        required: languageStore().messages.pages.editTaskPage.headerRequired,
                                        length: languageStore().messages.pages.editTaskPage.headerLength,
                                    }"
                                />
                            </div>
                            <div class="col-12 my-1">
                                <FormKit
                                    type="text"
                                    :label="languageStore().messages.pages.editTaskPage.text"
                                    name="text"
                                    id="text"
                                    v-model="form.value.text"
                                    :classes="{
                                        input: 'form-control form-control-dark',
                                        label: 'form-label',
                                    }"
                                    autocomplete="off"
                                    validation="required|length:0,255"
                                    :validation-messages="{
                                        required: languageStore().messages.pages.editTaskPage.textRequired,
                                        length: languageStore().messages.pages.editTaskPage.textLength,
                                    }"
                                />
                            </div>
                            <div class="col-12 my-1">
                                <div class="card w-100 my-2 p-1 flex-row align-items-center gap-2 flex-wrap" v-for="(subtask, index) of form.value.subtasks" :key="index">
                                    <div class="w-100">
                                        <div class="my-2">
                                            <span class="badge text-bg-info fs-5 text-white">{{ index + 1}}.</span>
                                        </div>
                                        <div class="my-2">
                                            <label :for="'question' + index" class="form-label">{{ languageStore().messages.pages.editTaskPage.subtaskQuestion }}</label>
                                            <Editor v-model="subtask.question" :id="'question' + index" editorStyle="height: 320px;"
                                                :pt="{
                                                    root: {
                                                        class: 'w-100 rounded'
                                                    }
                                                }"/>
                                        </div>
                                        <div class="my-2">
                                            <label :for="'type' + index" class="form-label">{{ languageStore().messages.pages.editTaskPage.subtaskType }}</label>
                                            <select name="type" :id="'type' + index" class="form-select form-control-dark" v-model="subtask.type">
                                                <option value="short_answer">{{ languageStore().messages.pages.editTaskPage.typeOptions.shortAnswer }}</option>
                                                <option value="multiple_choice">{{ languageStore().messages.pages.editTaskPage.typeOptions.multipleChoice }}</option>
                                                <option value="essay">{{ languageStore().messages.pages.editTaskPage.typeOptions.essay }}</option>
                                            </select>
                                        </div>
                                        <div class="my-2" v-if="subtask.type == 'multiple_choice'">
                                            <label :for="'options' + index" class="form-label mb-0">{{ languageStore().messages.pages.editTaskPage.subtaskOptions }}</label>
                                            <p><i>{{ languageStore().messages.pages.editTaskPage.subtaskOptionsInstructions }}</i></p>
                                            <Chips v-model="subtask.options" :id="'options' + index"
                                                :pt="{
                                                    container: {
                                                        class: 'p-0'
                                                    },
                                                    token: {
                                                        class: 'list-style-none badge text-bg-dark p-2 mb-2 mx-1'
                                                    },
                                                    removeTokenIcon: {
                                                        class: 'ms-2'
                                                    },
                                                    inputToken: {
                                                        class: 'list-style-none'
                                                    },
                                                    input: {
                                                        class: 'form-control'
                                                    }
                                                }"
                                            />
                                        </div>
                                        <div class="my-2">
                                            <FormKit
                                            type="number"
                                            :label="languageStore().messages.pages.editTaskPage.subtaskMarks"
                                            :id="'marks' + index"
                                            number
                                            step="0.5"
                                            v-model="subtask.marks"
                                            :classes="{
                                                input: 'form-control form-control-dark',
                                                label: 'form-label',
                                            }"
                                            validation="required|min:0"
                                            :validation-messages="{
                                                required: languageStore().messages.pages.editTaskPage.marksRequired,
                                                min: languageStore().messages.pages.editTaskPage.marksMin,
                                            }"
                                            />
                                        </div>
                                        <div class="my-2">
                                            <div class="w-100 d-flex justify-content-end aling-items-center gap-1">
                                                <button type="button" class="btn btn-secondary text-white mx-1" style="width: 50px" @click="moveItem(index, index - 1)" v-if="index != 0"><i class="fa-solid fa-arrow-up"></i></button>
                                                <button type="button" class="btn btn-secondary text-white mx-1" style="width: 50px" @click="moveItem(index, index + 1)" v-if="index < form.value.subtasks.length - 1"><i class="fa-solid fa-arrow-down"></i></button>
                                                <button type="button" class="btn btn-secondary text-white mx-1" style="width: 50px" @click="moveItem(index, 0)" v-else><i class="fa-solid fa-arrows-up-to-line"></i></button>
                                                <button type="button" class="btn btn-danger text-white mx-1" style="width: 50px" @click="confirmDeleteSubtask(subtask.id, index)"><i class="fa-solid fa-x"></i></button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 my-1">
                                <div class="card w-100 my-2 p-1">
                                    <button type="button" class="btn btn-info text-white" id="addSubtask" @click="addEmptySubtask"><i class="fa-solid fa-plus"></i></button>
                                </div>
                            </div>
                            <div class="col-12 my-1">
                                <div class="d-flex w-100 flex-row justify-content-end my-2 p-1">
                                    <RouterLink :to="{ name: 'editQuiz', params: { id: route.params.quizId } }" class="btn btn-secondary text-white ms-1">{{ languageStore().messages.pages.editTaskPage.cancel }}</RouterLink>
                                    <FormKit
                                        type="submit"
                                        id="submit"
                                        :label="languageStore().messages.pages.editTaskPage.submit"
                                        :classes="{
                                            input: 'btn btn-success text-white ms-1'
                                        }"
                                    />
                                </div>
                            </div>
                        </FormKit>
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
import Editor from 'primevue/editor';
import Chips from 'primevue/chips';
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
const form = reactive({});

const loading = computed(() => {
    return quiz.value == undefined || form.value == undefined;
});

const getQuiz = async () => {
    quiz.value = await quizStore().getQuiz(route.params.quizId);

    const response4 = await http.get(`quizzes/${route.params.quizId}/tasks/ids`, {
        headers: {
            Authorization: `Bearer ${userStore().token}`,
        },
    });
    if(response4.data.data.findIndex(x => x == route.params.taskId) == -1){
        router.back();
        return;
    }

    const response2 = await http.get(`tasks/${route.params.taskId}`, {
        headers: {
            Authorization: `Bearer ${userStore().token}`,
        },
    });
    form.value = response2.data.data;
    form.value.quiz_id = route.params.quizId;
};

const moveItem = async (from, to) => {
    form.value.subtasks[from].order = to;
    form.value.subtasks[to].order = from;
    
    let helper = form.value.subtasks.splice(from, 1)[0];
    form.value.subtasks.splice(to, 0, helper);
    try {
    } catch (error) {
        let toastToAdd = {
          severity: "error",
          detail: languageStore().messages.pages.editTaskPage.toastMessages.changeOrderUnexpectedError,
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

const addEmptySubtask = () => {
    if(form.value.subtasks == null){
        form.value.subtasks = [];
    }
    form.value.subtasks.push({
        order: form.value.subtasks.length,
        question: "",
        options: null,
        type: "short_answer",
        marks: 0
    });
}

const putTask = () => {
    try {
        for (let subtask of form.value.subtasks) {
            if(subtask.options != null && subtask.options.length < 1){
                subtask.options = null;
            }

            if(subtask.question == ""){
                let toastToAdd = {
                    severity: "error",
                    detail: languageStore().messages.pages.editTaskPage.validationMessages.allQuestionRequired,
                    life: 3000,
                };
                if (!themeStore().isDarkMode) {
                    toastToAdd.styleClass = "bg-danger text-white";
                }
                else {
                    toastToAdd.styleClass = "toast-danger text-white";
                }
                toast.add(toastToAdd);
                return;
            }
        }

        const response = http.put(`/tasks/${route.params.taskId}`, form.value, {
            headers: {
                Authorization: `Bearer ${userStore().token}`,
            },
        })

        router.push({ name: 'editQuiz', params: { id: route.params.quizId } });
    } catch (error) {
        let toastToAdd = {
            severity: "error",
            detail: languageStore().messages.pages.editTaskPage.toastMessages.updateUnexpectedError,
            life: 3000,
        };
        if (!themeStore().isDarkMode) {
            toastToAdd.styleClass = "bg-danger text-white";
        }
        else {
            toastToAdd.styleClass = "toast-danger text-white";
        }
        toast.add(toastToAdd);
        return;
    }
}

const confirmDeleteSubtask = async (id, index) => {
    await confirm.require({
        message: languageStore().messages.pages.editTaskPage.deleteTask,
        icon: 'pi pi-exclamation-triangle',
        rejectClass: 'btn btn-danger text-white',
        acceptClass: 'btn btn-success text-white',
        rejectLabel: languageStore().messages.pages.editTaskPage.cancel,
        acceptLabel: languageStore().messages.pages.editTaskPage.confirm,
        accept: async () => {
            try {          
                await http.delete(`/subtasks/${id}`, {
                    headers: {
                        Authorization: `Bearer ${userStore().token}`,
                    },
                });
                
                form.values.subtasks.splice(index, 1);

                let toastToAdd = {
                    severity: "success",
                    detail: languageStore().messages.pages.editTaskPage.toastMessages.deleteSuccess,
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
                    detail: languageStore().messages.pages.editTaskPage.toastMessages.deleteUnexpectedError,
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

const navigateToEditQuizPage = (id) => {
    window.location = `/quiz/${route.params.quizId}/edit/`
}

onMounted(() => {
    getQuiz()
})

</script>