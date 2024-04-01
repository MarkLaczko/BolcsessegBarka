<template>
    <BaseLayout>
        <h1 class="text-center my-3">{{ this.$route.meta.title }}</h1>

        <div>
            <div class="card">
                <Toolbar
                    :pt="{
                        start: {
                            class: 'col-sm-12 col-md-5 d-flex justify-content-md-start align-items-center justify-content-center'
                        },
                        center: {
                            class: 'col-sm-12 col-md-2'
                        },
                        end: {
                            class: 'col-sm-12 col-md-5 d-flex justify-content-md-end align-items-center justify-content-center'
                        },
                        root: {
                            class: 'row mb-2'
                        }
                    }"
                >
                    <template #start>
                        <Button label=" Új felhasználó" icon="pi pi-plus" class="mr-2 btn btn-success text-white me-1 mt-2 ms-2"/>
                        <Button label=" Törlés" icon="pi pi-trash" class="btn btn-danger text-white mt-2"/>
                    </template>
                    <template #end>
                        <FileUpload 
                            :pt="{
                                input: {
                                    class: 'd-none'
                                }
                            }"
                            mode="basic" accept="image/*" :maxFileSize="1000000" label="Importálás" chooseLabel=" Importálás" class="mr-2 btn btn-success text-white inline-block me-1 mt-2" />
                        <Button label=" Exportálás" icon="pi pi-upload" class="btn btn-warning text-white mt-2 me-2"/>
                    </template>
                </Toolbar>
            </div>
        </div>
    </BaseLayout>
</template>

<script>
import BaseLayout from "@layouts/BaseLayout.vue"
import Toolbar from "primevue/toolbar"
import FileUpload from "primevue/fileupload"
import Button from "primevue/button"

export default {
    components: {
        BaseLayout,
        Toolbar,
        FileUpload,
        Button
    }
}
</script>

<style>

</style>