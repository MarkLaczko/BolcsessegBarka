<template>
  <header>
    <BaseNavbar />
  </header>
  <main class="container">
    <slot />
  </main>
</template>

<script setup>
import BaseNavbar from "@components/layout/BaseNavbar.vue";
import { timeStore } from '@stores/TimeStore';
import { attemptStore } from '@stores/AttemptStore';
import { onMounted, onBeforeUnmount } from "vue";

const interval = timeStore().interval();

const checkAttemptsForExpiry = attemptStore().checkAttemptsForExpiry();

onMounted(() => {
  interval;
  checkAttemptsForExpiry;
});

onBeforeUnmount(() => {
  const interval_id = window.setInterval(function(){}, Number.MAX_SAFE_INTEGER);
  for (let i = 1; i < interval_id; i++) {
    window.clearInterval(i);
  }
});

</script>
