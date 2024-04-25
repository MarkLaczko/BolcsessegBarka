<template>
    <Toast
        :pt="{
            root: {
              class: 'w-25',
            },
            detail: {
              class: 'text-center fw-bold',
            },
            icon: {
              class: 'mt-1 ms-1',
            },
            text: {
              class: 'w-75 mx-auto',
            },
            container: {
              class: ' rounded w-75',
            },
            buttonContainer: {
              class: 'w-25 d-flex justify-content-center ms-auto',
            },
            button: {
              class: 'btn mb-2',
            },
            content: {
              class: 'd-flex justify-content-evenly align-items-center p-2'
            },
            transition: {
              name: 'slide-fade',
            },
        }"
    />
</template>

<script setup>
import Toast from 'primevue/toast';
</script>

<style>
.slide-fade-enter-active {
  transition: all 0.3s ease-out;
}

.slide-fade-leave-active {
  transition: all 0.8s cubic-bezier(1, 0.5, 0.8, 1);
}

.slide-fade-enter-from,
.slide-fade-leave-to {
  transform: translateY(-20px);
  opacity: 0;
}
</style>