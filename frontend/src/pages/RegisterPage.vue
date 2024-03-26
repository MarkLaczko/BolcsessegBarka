<template>
        <div class="d-flex justify-content-center align-items-center vh-100">
            <FormKit
                type="form"
                :actions="false"
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
            <h1 class="text-center py-4">Regisztráció</h1>
            
                <FormKit
                    type="text"
                    placeholder="Felhasználónév"
                    :classes="{
                        input: {
                            'm-auto' : true,
                            'form-control' : true
                        }
                    }"
                />
                <FormKit
                    type="email"
                    placeholder="E-mail"
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
                    :classes="{
                        input: {
                            'm-auto' : true,
                            'form-control' : true
                        }
                    }"
                />
                <FormKit
                    type="password"
                    placeholder="Jelszó megerősítés"
                    :classes="{
                        input: {
                            'm-auto' : true,
                            'mt-2' : true,
                            'form-control' : true
                        }
                    }"
                />
                <div class="d-flex justify-content-center">
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
                    Regisztráció
                    </FormKit>
                </div>
            </FormKit>
        </div>
</template>

<script>
export default {

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