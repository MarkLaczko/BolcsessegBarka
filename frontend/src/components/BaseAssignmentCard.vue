<template>
  <div class="card">
    <div class="card-header">
      <h3 class="text-center">{{ title }}</h3>
    </div>
    <div class="card-body m-auto text-center">
      <img
        :src="'http://localhost:5173/src/assets/images/' + image"
        :alt="image"
        class="img-fluid rounded"
      />
      <a href="#" class="btn text-white mt-3 buttons">Feladat leadása</a>
    </div>
    <div class="card-footer">
      <h6 class="text-center m-0">
        <b>Leadási határidő: {{ deadline }}</b>
      </h6>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    title: String,
    image: String,
    deadline: String,
  },
};
</script>
