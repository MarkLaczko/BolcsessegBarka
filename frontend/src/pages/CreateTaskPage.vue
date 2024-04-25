<template>
    <BaseLayout>
        <BaseToast />
        <BaseConfirmDialog />
        <BaseSpinner :loading="loading" />
        <div v-if="!loading">
            <h1>Feladat hozzáadása</h1>
            <div class="row">
                <div class="col-12">
                    <RouterLink :to="{ name: 'editQuiz', params: { id: route.params.id } }" class="btn btn-outline-secondary"><i class="fa-solid fa-arrow-left-long"></i> {{ quiz.value.name }}</RouterLink>
                </div>
                <div class="col-12">
                    <div class="row">
                        <div class="col-12 my-1">
                            <FormKit 
                                type="text"
                                label="Fejléc"
                                v-model="form.header"
                                :classes="{
                                    input: 'form-control',
                                    label: 'form-label',
                                }"
                                validation="required|length:0,100"
                                :validation-messages="{
                                    required:
                                        'Ez a mező kötelező.',
                                    length:
                                        'A fejléc maximum 100 karakter hosszú lehet.',
                                }"
                            />
                        </div>
                        <div class="col-12 my-1">
                            <FormKit 
                                type="text"
                                label="Rövid szöveg"
                                v-model="form.text"
                                :classes="{
                                    input: 'form-control',
                                    label: 'form-label',
                                }"
                                validation="required|length:0,255"
                                :validation-messages="{
                                    required:
                                        'Ez a mező kötelező.',
                                    length:
                                        'A fejléc maximum 255 karakter hosszú lehet.',
                                }"
                            />
                        </div>
                        <div class="col-12 my-1" v-if="form.subtasks">
                            <div class="card w-100 my-2 p-1 flex-row align-items-center gap-2 flex-wrap" v-for="(subtask, index) of form.subtasks" :key="index">
                                <div class="w-100">
                                    <div class="my-2">
                                        <span class="badge text-bg-info fs-5">{{ index + 1}}</span>
                                    </div>
                                    <div class="my-2">
                                        <label for="question" class="form-label">Alfeladat szövege</label>
                                        <Editor v-model="subtask.question" id="question" editorStyle="height: 320px;"
                                            :pt="{
                                                root: {
                                                    class: 'w-100 rounded'
                                                }
                                            }"/>
                                    </div>
                                    <div class="my-2">
                                        <label for="type" class="form-label">Feladat típusa</label>
                                        <select name="type" id="type" class="form-select" v-model="subtask.type">
                                            <option value="short_answer">Rövid válasz</option>
                                            <option value="multiple_choice">Több válasz</option>
                                            <option value="essay">Esszé</option>
                                        </select>
                                    </div>
                                    <div class="my-2" v-if="subtask.type == 'multiple_choice'">
                                        <label for="options" class="form-label mb-0">Válaszlehetőségek</label>
                                        <p><i>Kezdje el gépelni a válaszlehetőségeket! Az Enter billentyűvel tudja hozzáadni a válaszokat.</i></p>
                                        <Chips v-model="subtask.options" id="options"
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
                                    <div class="my-2" v-if="subtask.type == 'short_answer' || subtask.type == 'multiple_choice'">
                                        <label for="solution" class="form-label mb-0">Helyes válaszok</label>
                                        <p><i>Kezdje el gépelni a helyes válaszokat! Az Enter billentyűvel tudja hozzáadni a helyes válaszokat.</i></p>
                                        <Chips v-model="subtask.solution" id="solution"
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
                                        label="Feladat pontszáma"
                                        v-model="subtask.marks"
                                        :classes="{
                                            input: 'form-control',
                                            label: 'form-label',
                                        }"
                                        validation="required|min:0"
                                        :validation-messages="{
                                            required:
                                                'A pontszám megadása kötelező.',
                                            min:
                                                'A pontszám nem lehet kevesebb, mitn 0.',
                                        }"
                                        />
                                    </div>
                                    <div class="my-2">

                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-12 my-1">
                            <div class="card w-100 my-2 p-1">
                                <button class="btn btn-info" @click="addEmptySubtask"><i class="fa-solid fa-plus"></i></button>
                            </div>
                        </div>
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

const route = useRoute();
const router = useRouter();
const toast = useToast();
const confirm = useConfirm();

const quiz = reactive({});
const form = reactive({});

const loading = computed(() => {
    return quiz.value == undefined;
});

const getQuiz = async () => {
    const response = await http.get(`quizzes/${route.params.id}`, {
        headers: {
            Authorization: `Bearer ${userStore().token}`,
        },
    });
    quiz.value = response.data.data;

    let opensDate = new Date(quiz.value.opens * 1000);
    quiz.value.opens = quiz.value.opens == null ? null : `${opensDate.getFullYear()}-${(opensDate.getMonth() + 1) < 10 ? '0' : ''}${opensDate.getMonth() + 1}-${opensDate.getDate() < 10 ? '0' : ''}${opensDate.getDate()}T${opensDate.getHours() < 10 ? '0' : ''}${opensDate.getHours()}:${opensDate.getMinutes() < 10 ? '0' : ''}${opensDate.getMinutes()}:00`;
    let closesDate = new Date(quiz.value.closes * 1000);
    quiz.value.closes = quiz.value.closes == null ? null : `${closesDate.getFullYear()}-${(closesDate.getMonth() + 1) < 10 ? '0' : ''}${closesDate.getMonth() + 1}-${closesDate.getDate() < 10 ? '0' : ''}${closesDate.getDate()}T${closesDate.getHours() < 10 ? '0' : ''}${closesDate.getHours()}:${closesDate.getMinutes() < 10 ? '0' : ''}${closesDate.getMinutes()}:00`;
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
          detail: "Váratlan hiba a feladatok sorrendjének módosításakor!",
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
        message: 'Biztos ki akarja törölni ezt a feladatot és az összes hozzá tartozó alfeladatot?',
        icon: 'pi pi-exclamation-triangle',
        rejectClass: 'btn btn-danger',
        acceptClass: 'btn btn-success ',
        rejectLabel: 'Mégse',
        acceptLabel: 'Törlés',
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
                    detail: "Sikeres törlés!",
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
                    detail: "Váratlan hiba a törlésnél!",
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

const addEmptySubtask = () => {
    if(form.subtasks == null){
        form.subtasks = [];
    }
    form.subtasks.push({
        order: form.subtasks.length,
        question: "",
        options: [],
        solution: [],
        type: "short_answer",
        marks: 0
    });
}

onMounted(() => {
    getQuiz()
})

</script>