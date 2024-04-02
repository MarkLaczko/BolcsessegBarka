<template>
  <ul class="pagination justify-content-center pb-2">
    <li class="page-item" :class="{ disabled: currentPage === 1 }">
      <a
        class="page-link"
        href="#"
        @click.prevent="goToPage(1)"
        aria-label="First"
      >
        <i class="fa-solid fa-angles-left" aria-hidden="true"></i>
      </a>
    </li>
    <li class="page-item" :class="{ disabled: currentPage === 1 }">
      <a
        class="page-link"
        href="#"
        @click.prevent="goToPage(currentPage - 1)"
        aria-label="Previous"
      >
        <i class="fa-solid fa-arrow-left" aria-hidden="true"></i>
      </a>
    </li>
    <li
      class="page-item"
      v-for="page in visiblePages"
      :key="page"
      :class="{ active: page === currentPage }"
    >
      <a class="page-link" href="#" @click.prevent="goToPage(page)">{{
        page
      }}</a>
    </li>
    <li class="page-item" :class="{ disabled: currentPage === totalPages }">
      <a
        class="page-link"
        href="#"
        @click.prevent="goToPage(currentPage + 1)"
        aria-label="Next"
      >
        <i class="fa-solid fa-arrow-right" aria-hidden="true"></i>
      </a>
    </li>
    <li class="page-item" :class="{ disabled: currentPage === totalPages }">
      <a
        class="page-link"
        href="#"
        @click.prevent="goToPage(totalPages)"
        aria-label="Last"
      >
        <i class="fa-solid fa-angles-right" aria-hidden="true"></i>
      </a>
    </li>
  </ul>
</template>

<script>
export default {
  props: {
    totalPages: Number,
    currentPage: Number,
  },
  computed: {
    visiblePages() {
      let pages = [];
      let start = Math.max(this.currentPage - 2, 1);
      let end = Math.min(start + 4, this.totalPages);

      if (this.totalPages >= 5 && end === this.totalPages) {
        start = this.totalPages - 4;
      }

      for (let i = start; i <= end; i++) {
        pages.push(i);
      }

      return pages;
    },
  },
  methods: {
    goToPage(page) {
      if (page >= 1 && page <= this.totalPages) {
        this.$emit("page-changed", page);
      }
    },
  },
};
</script>

<style scoped>
a {
  font-weight: bold;
}

a:hover {
  background-color: rgba(49, 203, 230, 0.5);
}
</style>
