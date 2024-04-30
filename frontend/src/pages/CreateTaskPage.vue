<template>
    <BaseLayout>
        <BaseToast />
        <BaseConfirmDialog />
        <BaseSpinner :loading="loading" />
        <div v-if="!loading">
            <h1>{{ messages.pages.createTaskPage.title }}</h1>
            <div class="row">
                <div class="col-12">
                    <RouterLink :to="{ name: 'editQuiz', params: { id: route.params.id } }" class="btn btn-outline-secondary"><i class="fa-solid fa-arrow-left-long"></i> {{ quiz.value.name }}</RouterLink>
                </div>
                <div class="col-12">
                    <div class="row">
                        <FormKit
                            type="form"
                            id="form"
                            :actions="false"
                            @submit="postTask"
                            :incomplete-message="messages.pages.createTaskPage.validationMessages.matchAllValidationMessage"
                        >
                            <div class="col-12 my-1">
                                <FormKit
                                    type="text"
                                    :label="messages.pages.createTaskPage.header"
                                    name="header"
                                    id="header"
                                    v-model="form.header"
                                    :classes="{
                                        input: 'form-control',
                                        label: 'form-label',
                                    }"
                                    autocomplete="off"
                                    validation="required|length:0,100"
                                    :validation-messages="{
                                        required: messages.pages.createTaskPage.validationMessages.headerLength,
                                        length: messages.pages.createTaskPage.validationMessages.headerRequired,
                                    }"
                                />
                            </div>
                            <div class="col-12 my-1">
                                <FormKit
                                    type="text"
                                    :label="messages.pages.createTaskPage.text"
                                    name="text"
                                    id="text"
                                    v-model="form.text"
                                    :classes="{
                                        input: 'form-control',
                                        label: 'form-label',
                                    }"
                                    autocomplete="off"
                                    validation="required|length:0,255"
                                    :validation-messages="{
                                        required: messages.pages.createTaskPage.validationMessages.textLength,
                                        length: messages.pages.createTaskPage.validationMessages.textRequired,
                                    }"
                                />
                            </div>
                            <div class="col-12 my-1" v-if="form.subtasks">
                                <div class="card w-100 my-2 p-1 flex-row align-items-center gap-2 flex-wrap" v-for="(subtask, index) of form.subtasks" :key="index">
                                    <div class="w-100">
                                        <div class="my-2">
                                            <span class="badge text-bg-info fs-5 text-white">{{ index + 1}}.</span>
                                        </div>
                                        <div class="my-2">
                                            <label :for="'question' + index" class="form-label">{{ messages.pages.createTaskPage.subtaskQuestion }}</label>
                                            <Editor v-model="subtask.question" :id="'question' + index" editorStyle="height: 320px;"
                                                :pt="{
                                                    root: {
                                                        class: 'w-100 rounded'
                                                    }
                                                }"/>
                                        </div>
                                        <div class="my-2">
                                            <label :for="'type' + index" class="form-label">{{ messages.pages.createTaskPage.subtaskType }}</label>
                                            <select name="type" :id="'type' + index" class="form-select" v-model="subtask.type">
                                                <option value="short_answer">{{ messages.pages.createTaskPage.typeOptions.shortAnswer }}</option>
                                                <option value="multiple_choice">{{ messages.pages.createTaskPage.typeOptions.multipleChoice }}</option>
                                                <option value="essay">{{ messages.pages.createTaskPage.typeOptions.essay }}</option>
                                            </select>
                                        </div>
                                        <div class="my-2" v-if="subtask.type == 'multiple_choice'">
                                            <label :for="'options' + index" class="form-label mb-0">{{ messages.pages.createTaskPage.subtaskOptions }}</label>
                                            <p><i>{{ messages.pages.createTaskPage.subtaskOptionsInstructions }}</i></p>
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
                                            :label="messages.pages.createTaskPage.subtaskMarks"
                                            :id="'marks' + index"
                                            number
                                            step="0.5"
                                            v-model="subtask.marks"
                                            :classes="{
                                                input: 'form-control',
                                                label: 'form-label',
                                            }"
                                            validation="required|min:0"
                                            :validation-messages="{
                                                required: messages.pages.createTaskPage.validationMessages.marksRequired,
                                                length: messages.pages.createTaskPage.validationMessages.marksMin,
                                            }"
                                            />
                                        </div>
                                        <div class="my-2">
                                            <div class="w-100 d-flex justify-content-end aling-items-center gap-1">
                                                <button type="button" class="btn btn-secondary text-white mx-1" style="width: 50px" @click="moveItem(index, index - 1)" v-if="index != 0"><i class="fa-solid fa-arrow-up"></i></button>
                                                <button type="button" class="btn btn-secondary text-white mx-1" style="width: 50px" @click="moveItem(index, index + 1)" v-if="index < form.subtasks.length - 1"><i class="fa-solid fa-arrow-down"></i></button>
                                                <button type="button" class="btn btn-secondary text-white mx-1" style="width: 50px" @click="moveItem(index, 0)" v-else><i class="fa-solid fa-arrows-up-to-line"></i></button>
                                                <button type="button" class="btn btn-danger text-white mx-1" style="width: 50px" @click="confirmDeleteSubtask(subtask.id, index)"><i class="fa-solid fa-x"></i></button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 my-1">
                                <div class="card w-100 my-2 p-1">
                                    <button type="button" class="btn btn-info" id="addSubtask" @click="addEmptySubtask"><i class="fa-solid fa-plus"></i></button>
                                </div>
                            </div>
                            <div class="col-12 my-1">
                                <div class="d-flex w-100 flex-row justify-content-end my-2 p-1">
                                    <RouterLink :to="{ name: 'editQuiz', params: { id: route.params.id } }" class="btn btn-secondary ms-1">{{ messages.pages.createTaskPage.cancel }}</RouterLink>
                                    <FormKit
                                        type="submit"
                                        :label="messages.pages.createTaskPage.submit"
                                        id="submit"
                                        :classes="{
                                            input: 'btn btn-success ms-1'
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
import { themeStore } from '@stores/ThemeStore';
import { quizStore } from "@stores/QuizStore";
import { languageStore } from '@stores/LanguageStore';

const route = useRoute();
const router = useRouter();
const toast = useToast();
const confirm = useConfirm();

const quiz = reactive({});
const form = reactive({});

const loading = computed(() => {
    return quiz.value == undefined;
});

const messages = languageStore().messages;

const getQuiz = async () => {
    quiz.value = await quizStore().getQuiz(route.params.id);

    form.quiz_id = quiz.value.id;
    form.order = quiz.value.number_of_tasks;
};

const moveItem = async (from, to) => {
    form.subtasks[from].order = to;
    form.subtasks[to].order = from;
    
    let helper = form.subtasks.splice(from, 1)[0];
    form.subtasks.splice(to, 0, helper);
    try {
    } catch (error) {
        let toastToAdd = {
          severity: "error",
          detail: messages.pages.createTaskPage.toastMessages.changeOrderUnexpectedError,
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
    if(form.subtasks == null){
        form.subtasks = [];
    }
    form.subtasks.push({
        order: form.subtasks.length,
        question: "",
        options: null,
        type: "short_answer",
        marks: 0
    });
}

const postTask = () => {
    try {
        for (let subtask of form.subtasks) {
            if(subtask.options != null && subtask.options.length < 1){
                subtask.options = null;
            }

            if(subtask.question == ""){
                let toastToAdd = {
                    severity: "error",
                    detail: messages.pages.createTaskPage.validationMessages.allQuestionRequired,
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

        const response = http.post('/tasks', form, {
            headers: {
                Authorization: `Bearer ${userStore().token}`,
            },
        })

        router.push({ name: 'editQuiz', params: { id: route.params.id } });
    } catch (error) {
        let toastToAdd = {
            severity: "error",
            detail: messages.pages.createTaskPage.toastMessages.createUnexpectedError,
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
        message: messages.pages.createTaskPage.deleteTask,
        icon: 'pi pi-exclamation-triangle',
        rejectClass: 'btn btn-danger',
        acceptClass: 'btn btn-success ',
        rejectLabel: messages.pages.createTaskPage.cancel,
        acceptLabel: messages.pages.createTaskPage.confirm,
        accept: async () => {
            try {               
                form.subtasks.splice(index, 1);

                let toastToAdd = {
                    severity: "success",
                    detail: messages.pages.createTaskPage.toastMessages.deleteSuccess,
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
                    detail: messages.pages.createTaskPage.toastMessages.deleteUnexpectedError,
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

onMounted(() => {
    getQuiz()
})

</script>