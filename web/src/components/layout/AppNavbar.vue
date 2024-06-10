<script setup lang="ts">
import NavbarButton from "@/components/layout/NavbarButton.vue";
import {onBeforeUnmount, onMounted, ref} from "vue";
import {faBars, faSignsPost, faDiceD20, faFeather} from "@fortawesome/free-solid-svg-icons";

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

const openMenu = () => {
  showMobileMenu.value = !showMobileMenu.value;
}
</script>

<template>
  <div v-show="!isMobile" class="navbar__container">
    <NavbarButton :icon=faSignsPost name="Explore" />
    <NavbarButton :icon="faDiceD20" name="Play" />
    <NavbarButton :icon="faFeather" name="Create" />
  </div>
  <div v-show="isMobile" class="navbar__container">
    <img src="../../assets/Pathmaker-logo.png" alt="">
    <div class="navbar__hamburger-menu" @onclick="openMenu">
      <font-awesome-icon :icon="faBars"></font-awesome-icon>
    </div>
    <div v-show="showMobileMenu" class="navbar__menu">
      <NavbarButton :icon=faSignsPost name="Explore" />
      <NavbarButton :icon="faDiceD20" name="Play" />
      <NavbarButton :icon="faFeather" name="Create" />
    </div>
  </div>
</template>

<style scoped lang="scss">
img {
  max-width: 40%;
  padding-left: 0.5rem;
}
.navbar {
  &__container {
    display: flex;
    padding: 0.5rem 1rem 0.5rem 0;
    align-items: center;
    justify-content: space-between;
    height: $nav-height;
    background: $color-navbar;
    position: fixed;
    z-index: 1;
    width: 100%;
    border-bottom: 0.1rem solid $color-border;
  }
  &__hamburger-menu {
    display: flex;
    padding: 1rem;
    cursor: pointer;
  }
  &__menu {
    display: flex;
  }
}

</style>