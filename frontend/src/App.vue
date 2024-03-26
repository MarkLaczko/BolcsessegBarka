  <script>
  import { RouterView } from 'vue-router'
  export default{
    components:{
      RouterView
    },
    computed: {
      isSignedOut() {
        return this.$route.path === "/register" || this.$route.path === "/login";
      }
    }
  }
  </script>

  <template>
    <div :class="{ backgroundColor: isSignedOut }">
      <RouterView />
    </div>
  </template>

  <style>
      .backgroundColor {
          background: linear-gradient(#FFFFFF, #3FAEEC);
          height: 100%;
      }
  </style>
