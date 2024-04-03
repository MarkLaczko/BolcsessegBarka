<template>
  <BaseLayout>
    <div class="rounded-3 my-5 pt-2">
      <h1 class="text-center mb-4">{{ this.$route.meta.title }}</h1>

      <div class="row px-5 pb-5 g-4">
        <div class="col-12 col-lg-4 col-md-6">
          <BaseCourseCard :title="'Kurzus 1'" :image="'asd.jpeg'" />
        </div>
        <div class="col-12 col-lg-4 col-md-6">
          <BaseCourseCard :title="'Kurzus 2'" :image="'asd.jpeg'" />
        </div>
        <div class="col-12 col-lg-4 col-md-6">
          <BaseCourseCard :title="'Kurzus 3'" :image="'asd.jpeg'" />
        </div>
        <div class="col-12 col-lg-4 col-md-6">
          <BaseCourseCard :title="'Kurzus 4'" :image="'asd.jpeg'" />
        </div>
        <div class="col-12 col-lg-4 col-md-6">
          <BaseCourseCard :title="'Kurzus 5'" :image="'asd.jpeg'" />
        </div>
        <div class="col-12 col-lg-4 col-md-6">
          <BaseCourseCard :title="'Kurzus 6'" :image="'asd.jpeg'" />
        </div>
      </div>
    </div>
  </BaseLayout>
</template>

<script>
import BaseLayout from "@layouts/BaseLayout.vue";
import BaseCourseCard from "@components/BaseCourseCard.vue";

export default {
  components: {
    BaseLayout,
    BaseCourseCard,
  },
};
</script>
