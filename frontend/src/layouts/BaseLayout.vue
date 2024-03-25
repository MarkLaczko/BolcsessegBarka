<template>
    <header>
        <BaseNavbar />
    </header>
    <main class="container">
        <slot />
    </main>

</template>
<script>
import BaseNavbar from "@components/BaseNavbar.vue"
export default {
    components: {
        BaseNavbar,
    }
}
</script>