<template>
    <ConfirmDialog
      :pt="{
        root: {
          class: 'modal-dialog p-3 rounded bg-white shadow border',
        },
        header: {
          class: 'd-flex justify-content-end align-items-center pb-2',
        },
        title: {
          class: 'modal-title fw-bold',
        },
        closeButton: {
          class: 'btn btn-outline-dark btn-sm',
        },
        closeButtonIcon: {
          class: 'fa-solid fa-x',
        },
        footer: {
          class: 'd-flex justify-content-end align-items-center gap-1 mt-2'
        },
        transition: {
          name: 'slide-fade',
        },
      }"
    ></ConfirmDialog>
</template>

<script setup>
import ConfirmDialog from 'primevue/confirmdialog';
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