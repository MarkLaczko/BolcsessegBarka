<template>
    <BaseLayout>
        <BaseToast />
        <FormKit
            type="form"
            :actions="false"
            :classes="{
                form: {
                    'mt-2' : true
                }
            }"
            @submit="updateProfile"
        >
            <h1 class="text-center">{{ messages.pages.profilePage.title }}</h1>

            <div class="row">
                <div class="col-12 col-md-6">
                    <label class="form-label">{{ messages.pages.profilePage.nameLabel }}</label>
                    <input type="text" class="form-control form-control-dark" disabled :value="currentUserData.name">
                </div>
                <div class="col-12 col-md-6">
                    <label class="form-label">{{ messages.pages.profilePage.emailLabel }}</label>
                    <input type="text" class="form-control form-control-dark" disabled :value="currentUserData.email">
                </div>

                <div class="col-12 mt-2">
                    <h2>{{ messages.pages.profilePage.changePassword }}</h2>
                </div>                
                
                <div class="col-12 col-md-4">
                    <label class="form-label">{{ messages.pages.profilePage.currentPasswordLabel }}</label>
                    <FormKit
                        type="password"
                        name="old_password"
                        id="old_password"
                        validation="required|length:8,255"
                        :validation-messages="{
                            required: messages.pages.profilePage.validationMessages.passwordRequired,
                            length: messages.pages.profilePage.validationMessages.passwordLength,
                        }"
                        :classes="{
                        input: {
                            'form-control': true,
                            'w-100': true,
                            'form-control-dark': true,
                        },
                        messages: {
                            'list-style-none': true,
                        },
                        message: {
                            'text-danger': true,
                            'p-0': true,
                        },
                        }"
                    />
                </div>
                <div class="col-12 col-md-4">
                    <label class="form-label">{{ messages.pages.profilePage.passwordLabel }}</label>
                    <FormKit
                        type="password"
                        name="password"
                        id="password"
                        validation="required|length:8,255"
                        :validation-messages="{
                            required: messages.pages.profilePage.validationMessages.passwordRequired,
                            length: messages.pages.profilePage.validationMessages.passwordLength,
                        }"
                        :classes="{
                        input: {
                            'form-control': true,
                            'w-100': true,
                            'form-control-dark': true,
                        },
                        messages: {
                            'list-style-none': true,
                        },
                        message: {
                            'text-danger': true,
                            'p-0': true,
                        },
                        }"
                    />
                </div>
                <div class="col-12 col-md-4">
                    <label class="form-label">{{ messages.pages.profilePage.confirmPasswordLabel }}</label>
                    <FormKit
                        type="password"
                        name="password_confirm"
                        id="password_confirm"
                        validation="required|length:8,255|confirm"
                        :validation-messages="{
                            required: messages.pages.profilePage.validationMessages.confirmPasswordRequired,
                            length:
                            messages.pages.profilePage.validationMessages.confirmPasswordLength,
                            confirm: messages.pages.profilePage.validationMessages.confirmPasswordConfirm,
                        }"
                        :classes="{
                        input: {
                            'form-control': true,
                            'w-100': true,
                            'form-control-dark': true,
                        },
                        messages: {
                            'list-style-none': true,
                        },
                        message: {
                            'text-danger': true,
                            'p-0': true,
                        },
                        }"
                    />
                </div>
                
                <div class="col-12">
                    <div class="d-flex justify-content-end align-items-center">
                        <FormKit
                            type="submit"
                            :label="messages.pages.profilePage.submitButton"
                            :classes="{
                                outer: {
                                    'mt-2': true
                                },
                                input: {
                                    'px-3': true,
                                    'formButton' : true
                                },
                            }"
                        />
                    </div>
                </div>

            </div>
        </FormKit>
    </BaseLayout>
</template>

<script>
import BaseLayout from '@layouts/BaseLayout.vue';
import BaseToast from '@components/BaseToast.vue';
import { userStore } from '@stores/UserStore';
import { languageStore } from '@stores/LanguageStore';
import { mapState } from 'pinia';
import { http } from '@utils/http';

export default {
    components: {
        BaseLayout,
        BaseToast
    },
    computed: {
        ...mapState(userStore, ['currentUserData', 'token']),
        ...mapState(languageStore, ['messages'])
    },
    methods: {
        updateProfile(data){
            try {
                data.password_confirmation = data.password_confirm;
                const response = http.put('/profile', data, {
                    headers: {
                        Authorization: `Bearer ${this.token}`,
                    },
                });
                if (this.isDarkMode) {
                    this.$toast.add({
                        severity: "success",
                        detail:
                        this.messages.pages.courseManagementPage.toastMessages
                            .successfullyDeletedCourse,
                        life: 3000,
                    });
                } 
                else {
                    this.$toast.add({
                        severity: "success",
                        detail:
                        this.messages.pages.courseManagementPage.toastMessages
                            .successfullyDeletedCourse,
                        styleClass: "bg-success text-white",
                        life: 3000,
                    });
                }
            } catch (error) {
                
            }
        }
    }
}
</script>