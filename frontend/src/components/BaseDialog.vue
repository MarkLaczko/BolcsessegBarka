<template>
    <Dialog
        v-model:visible="props.visible"
        modal
        :closable="false"
        :header="props.header"
        :style="{ width: `${props.width == null ? '50rem' : props.width}` }"
        :pt="{
          root: {
            class: 'modal-dialog p-3 rounded shadow border',
          },
          header: {
            class: 'd-flex justify-content-between align-items-center pb-2',
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
          transition: {
            name: 'slide-fade',
          },
        }"
      >
        <slot />
    </Dialog>
</template>

<script setup>
import Dialog from 'primevue/dialog';

const props = defineProps({
    visible: Boolean,
    header: String,
    width: String
})
</script>

<style scoped>
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