<template>
    <BaseLayout>
        <h1 class="text-center my-3">{{ this.$route.meta.title }}</h1>
    </BaseLayout>
</template>

<script>
import BaseLayout from "@layouts/BaseLayout.vue"

export default {
    components: {
        BaseLayout
    }
}
</script>