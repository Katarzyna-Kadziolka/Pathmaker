<script setup lang="ts">
import NavbarButton from "@/components/layout/NavbarButton.vue";
import {onBeforeUnmount, onMounted, ref} from "vue";
import {faBars} from "@fortawesome/free-solid-svg-icons";

const isMobile = ref(false);
const showMobileMenu = ref(false);

onMounted(() => {
  isMobile.value = window.innerWidth < 768;

  window.addEventListener('resize', () => {
    isMobile.value = window.innerWidth < 768;
  });
});

onBeforeUnmount(() => {
  window.removeEventListener('resize', () => {
    isMobile.value = window.innerWidth < 768;
  });
});
</script>

<template>
  <div v-show="!isMobile" class="navbar__container">
    <NavbarButton />
  </div>
  <div v-show="isMobile" class="navbar__container">
    <img src="../../assets/Pathmaker-logo.png" alt="">
    <font-awesome-icon :icon="faBars"></font-awesome-icon>
  </div>
</template>

<style scoped lang="scss">
img {
  max-width: 40%;
}
.navbar {
  &__container {
    display: flex;
    padding: 0.5rem 1rem 0.5rem 0.5rem;
    align-items: center;
    justify-content: space-between;
    height: 4rem;
    background: $color-navbar;
  }
}

</style>