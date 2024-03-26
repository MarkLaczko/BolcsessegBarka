<template>
    <div class="d-flex justify-content-center align-items-center vh-100">
        <FormKit
            type="form"
            :actions="false"
            @submit="attemptLogin"
            :classes="{
                form: {
                    'container' : true,
                    'form-control-width' : true,
                    'bg-info' : true,
                    'rounded-4' : true,
                    'px-5' : true,
                    'pb-2' : true
                }
            }"
        >
        <Message
            v-for="msg of messages" :key="msg.id"
            :icon="`fa-solid fa-${msg.icon}`"
            :pt="{
                wrapper: `alert alert-${msg.color} d-flex flex-row w-100 justify-content-between align-items-center p-2 mt-4 mb-0`,
                button: `btn btn-outline-${msg.color} rounded-circle`,
            }">
            {{ msg.content }}
            <template #closeicon>
                <i class="fa-solid fa-xmark"></i>
            </template>
        </Message>
        <h1 class="text-center py-4">Bejelentkezés</h1>
        
            <FormKit
                type="email"
                placeholder="E-mail"
                name="email"
                :classes="{
                    input: {
                        'm-auto' : true,
                        'my-2' : true,
                        'form-control' : true
                    }
                }"
            />
            <FormKit
                type="password"
                placeholder="Jelszó"
                name="password"
                :classes="{
                    input: {
                        'm-auto' : true,
                        'form-control' : true
                    }
                }"
            />

            <div class="d-flex gap-2 justify-content-center">
                <FormKit
                    type="submit"
                    :classes="{
                        input: {
                            'text-white' : true,
                            'bg-primary' : true,
                            'px-3' : true
                        },
                        outer: {
                            'pb-3' : true
                        }
                    }"
                >
                Bejelentkezés
                </FormKit>

                <RouterLink :to="{ name: 'register' }" class="btn btn-secondary h-100 mt-3">
                    Még nincs fiókja?
                </RouterLink>
            </div>
        </FormKit>
    </div>
</template>

<script>
import Message from 'primevue/message';
import { RouterLink } from 'vue-router'
import { mapState, mapActions } from 'pinia';
import { userStore } from '@stores/UserStore';

export default {
    components: {
        Message,
        RouterLink
    },
    data() {
        return {
            messages: [],
            count: 0
        };
    },
    methods: {
        ...mapActions(userStore, ['login']),
        async attemptLogin(data){
            console.log(data)
            try {
                await this.login(data);
                this.$router.push({name: 'home'});
            } catch (error) {
                this.messages = [
                    { color: 'danger', icon:'triangle-exclamation', content: 'Sikertelen belépés!', id: this.count++ },
                ];
            }
        }
    }
}
</script>

<style scoped>
.form-control-width {
    width: 50%;
} 

@media screen and (min-width: 1200px) {
    .form-control-width {
        width: 40%;
    } 
}
</style>