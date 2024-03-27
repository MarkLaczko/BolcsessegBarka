<template>
    <BaseLayout>
        <h1 class="text-center my-3">Üdvözöllek {{ currentUserData.name }}!</h1>
        
        <div class="bg-info rounded-3 pt-3">
            <h2 class="text-center mb-3"><b>Elkészítendő feladatok</b></h2>
            
            <div class="row px-5 pb-5 g-4">
                <div class="col-12 col-lg-4 col-md-6">
                    <BaseAssignmentCard :title="'Feladat 1'" :image="'asd.jpeg'" :deadline="'2024.09.09'"/>
                </div>
                <div class="col-12 col-lg-4 col-md-6">
                    <BaseAssignmentCard :title="'Feladat 2'" :image="'asd.jpeg'" :deadline="'2024.09.13'"/>
                </div>
                <div class="col-12 col-lg-4 col-md-6">
                    <BaseAssignmentCard :title="'Feladat 3'" :image="'asd.jpeg'" :deadline="'2024.09.15'"/>
                </div>
            </div>
            <ul class="pagination justify-content-center pb-2">
                <li class="page-item">
                    <a class="page-link" href="#" aria-label="Previous">
                        <span aria-hidden="true">&laquo;</span>
                    </a>
                </li>
                <li class="page-item"><a class="page-link" href="#">1</a></li>
                <li class="page-item"><a class="page-link" href="#">2</a></li>
                <li class="page-item"><a class="page-link" href="#">3</a></li>
                <li class="page-item">
                    <a class="page-link" href="#" aria-label="Next">
                        <span aria-hidden="true">&raquo;</span>
                    </a>
                </li>
            </ul>
        </div>

        <div class="bg-info rounded-3 pt-3 pb-2 mb-2">
            <h2 class="text-center mb-3"><b>Legutóbbi tananyagok</b></h2>

            <BaseLearningMaterialCard :course="'Szoftverfejlesztő'" :learningMaterial="'Tesztelés'" :image="'asd.jpeg'" :releaseData="'2024.06.05'"/>

            <BaseLearningMaterialCard :course="'Frontend'" :learningMaterial="'Vue'" :image="'asd.jpeg'" :releaseData="'2024.06.10'"/>

            <BaseLearningMaterialCard :course="'Backend'" :learningMaterial="'Rest API'" :image="'asd.jpeg'" :releaseData="'2024.06.12'"/>
        </div>
    </BaseLayout>
</template>

<script>
import BaseLayout from "@layouts/BaseLayout.vue"
import BaseAssignmentCard from "@components/BaseAssignmentCard.vue";
import BaseLearningMaterialCard from "@components/BaseLearningMaterialCard.vue"
import { userStore } from "@stores/UserStore.mjs"
import { mapActions, mapState } from "pinia";

export default {
    components: {
        BaseLayout,
        BaseAssignmentCard,
        BaseLearningMaterialCard
    },
    methods: {
        ...mapActions(userStore, ["getUser"]),
    },
    computed: {
        ...mapState(userStore, ["currentUserData"]),
    },
    async mounted() {
        await this.getUser();
    }
}
</script>

<style>
</style>