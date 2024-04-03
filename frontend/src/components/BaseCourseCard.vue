<template>
  <div class="card">
    <img
      :src="'http://localhost:5173/src/assets/images/' + image"
      class="card-img-top"
      :alt="image"
    />
    <div class="card-body text-center">
      <h5 class="card-title">{{ title }}</h5>
      <a href="#" class="btn text-white mt-3 buttons">Megtekintés</a>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    title: String,
    image: String,
  },
};
</script>
