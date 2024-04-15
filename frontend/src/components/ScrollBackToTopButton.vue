<template>
    <transition name="fade">
        <a href="#" id="toTopBtn" class="cd-top text-replace"
            v-show="showToTopButton"
            @click.prevent="scrollToTop"></a>
    </transition>
</template>

<script>
export default {
    data() {
        return {
            showToTopButton: false
        };
    },
    methods: {
        scrollToTop() {
            window.scrollTo({
                top: 0,
                behavior: "smooth"
            });
        },
        handleScroll() {
            this.showToTopButton = window.pageYOffset > 100;
        }
    },
    mounted() {
        window.addEventListener("scroll", this.handleScroll);
    },
    beforeDestroy() {
        window.removeEventListener("scroll", this.handleScroll);
    }
};
</script>


<style scoped>
    

    #toTopBtn {
        position: fixed;
        bottom: 26px;
        right: 39px;
        z-index: 98;
        padding: 21px;
        background-color: #3faeec;
        border-radius: 50%;
    }

    .cd-top {
        position: fixed;
        bottom: 20px;
        right: 20px;
        display: inline-block;
        height: 40px;
        width: 40px;
        box-shadow: 0 0 10px rgba(0,0,0,.05) !important;
        background: url(https://res.cloudinary.com/dxfq3iotg/image/upload/v1571057658/cd-top-arrow.svg) no-repeat center 50%;
    }
    
    .fade-enter-active, .fade-leave-active {
        transition: opacity 0.5s;
    }
    .fade-enter-from, .fade-leave-to {
        opacity: 0;
    } 
</style>