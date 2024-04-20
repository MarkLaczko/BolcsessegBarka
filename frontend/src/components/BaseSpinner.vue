<template>
    <div class="d-flex justify-content-center" v-if="loading">
        <img src="../assets/images/logo.png" alt="logo" width="300" class="rotating-pulsating" />
    </div>
</template>

<script>
export default {
    props: {
        loading: {
            type: Boolean,
            required: false
        }
    }
};
</script>
