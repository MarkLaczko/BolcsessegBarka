<template>
    <BaseLayout>
        <BaseToast />
        <BaseSpinner :loading="loading" />
        <h1>{{ messages.pages.createQuizPage.title }}</h1>
        <div class="row" v-if="!loading">
            <div class="col-12" v-if="topic.value != undefined">
                <div class="row">
                    <div class="col-12">
                        <ol class="breadcrumb">
                            <li class="breadcrumb-item"><RouterLink :to="{name: 'course', params: {id: route.params.courseId}}">{{ course.value.name }}</RouterLink></li>
                            <li class="breadcrumb-item active" aria-current="page">{{ topic.value.name }}</li>
                        </ol>
                    </div>
                </div>
            </div>
            <div class="col-12" v-else>
                
                <div class="row">
                    <div class="col-12">
                        <ol class="breadcrumb">
                            <li class="breadcrumb-item"><a href="#">{{ course.value.name }}</a></li>
                        </ol>
                    </div>
                    <div class="col-12">
                        <label for="topic_id">{{ messages.pages.createQuizPage.topic }}:</label>
                        <select name="topic_id" id="topic_id" v-model="form.topic_id" class="form-select">
                            <option :value="topic.id" v-for="topic of course.value.topics">{{ topic.name }}</option>
                        </select>
                    </div>
                </div>
            </div>
            <div class="col-12">
                <FormKit
                    type="form"
                    :actions="false"
                    :classes="{
                        message: 'text-end'
                    }"
                    :incomplete-message="messages.pages.createQuizPage.validationMessages.matchAllValidationMessage"
                    @submit="submitForm"
                >
                    <div class="row">
                        <div class="col-12 my-1">
                            <FormKit 
                                type="text"
                                :label="messages.pages.createQuizPage.name"
                                name="name"
                                id="name"
                                v-model="form.name"
                                :classes="{
                                    input: 'form-control',
                                    label: 'form-label',
                                }"
                                validation="required|length:0,100"
                                :validation-messages="{
                                    required:
                                        messages.pages.createQuizPage.validationMessages.nameLength, 
                                    length:
                                        messages.pages.createQuizPage.validationMessages.nameRequired,
                                }"
                            />
                        </div>
                        <div class="col-12 col-md-6 col-lg-3 my-1">
                            <FormKit 
                                type="number"
                                :label="messages.pages.createQuizPage.attempts"
                                name="max_attempts"
                                id="max_attempts"
                                v-model="form.max_attempts"
                                :classes="{
                                    input: 'form-control',
                                    label: 'form-label',
                                }"
                                validation="min:0"
                                :validation-messages="{
                                    min:
                                        messages.pages.createQuizPage.validationMessages.attemptsMin,
                                }"
                            />
                        </div>
                        <div class="col-12 col-md-6 col-lg-3 my-1">
                            <FormKit 
                                type="datetime-local"
                                :label="messages.pages.createQuizPage.opens"
                                name="opens"
                                id="opens"
                                v-model="form.opens"
                                :classes="{
                                    input: 'form-control',
                                    label: 'form-label',
                                }"
                            />
                        </div>
                        <div class="col-12 col-md-6 col-lg-3 my-1">
                            <FormKit 
                                type="datetime-local"
                                :label="messages.pages.createQuizPage.closes"
                                name="closes"
                                id="closes"
                                v-model="form.closes"
                                :classes="{
                                    input: 'form-control',
                                    label: 'form-label',
                                }"
                            />
                        </div>
                        <div class="col-12 col-md-6 col-lg-3 my-1">
                            <FormKit 
                                type="number"
                                :label="messages.pages.createQuizPage.time"
                                name="time"
                                id="time"
                                v-model="form.time"
                                :classes="{
                                    input: 'form-control',
                                    label: 'form-label',
                                }"
                                
                                :validation-messages="{
                                    min:
                                        messages.pages.createQuizPage.validationMessages.timeMin,
                                }"
                            />
                        </div>
                        <div class="col-12 my-1 d-flex justify-content-end">
                            <FormKit 
                                type="submit"
                                id="submit"
                                :label="messages.pages.createQuizPage.submit"
                                :classes="{
                                    input: 'btn btn-success text-white',
                                }"
                            />
                        </div>
                    </div>
                </FormKit>
            </div>
        </div>
    </BaseLayout>
</template>

<script setup>
import BaseLayout from '@layouts/BaseLayout.vue';
import BaseToast from '@components/BaseToast.vue';
import BaseSpinner from '@components/BaseSpinner.vue';
import { reactive, computed, onMounted } from 'vue';
import { useRoute, useRouter } from 'vue-router'
import { http } from '@utils/http';
import { useToast } from 'primevue/usetoast';
import { userStore } from "@stores/UserStore";
import { quizStore } from "@stores/QuizStore";
import { themeStore } from '@stores/ThemeStore';
import { languageStore } from '@stores/LanguageStore';

const route = useRoute();
const router = useRouter();
const toast = useToast();

const course = reactive({});
const topic = reactive({});

const form = reactive({});

const messages = languageStore().messages;

const loading = computed(() => {
    return course.value == undefined;
});

const getCourse = async () => {
    const response = await http.get(`courses/${route.params.courseId}`, {
        headers: {
            Authorization: `Bearer ${userStore().token}`,
        },
    });
    course.value = response.data.data;

    let findTopic = course.value.topics.find(x => x.id == route.params.topicId);
    if(findTopic){
        topic.value = findTopic;
        form.topic_id = findTopic.id;
    }
};

const submitForm = async () => {
    try {
        form.opens = form.opens == null ? null : Math.floor(new Date(form.opens).getTime() / 1000);
        form.closes = form.closes == null ? null : Math.floor(new Date(form.closes).getTime() / 1000);
        const response = await quizStore().postQuiz(form);
        if(!response.id){
            throw ('Error')
        }
        router.push({name: 'editQuiz', params: {id: response.id}});
    } catch (error) {
        let toastToAdd = {
          severity: "error",
          detail: messages.pages.createQuizPage.toastMessages.unexpectedError,
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

onMounted(() => {
    getCourse();
})

</script>