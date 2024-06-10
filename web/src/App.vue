<script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router'
import {useI18n} from 'vue-i18n'
import Navbar from "@/components/layout/AppNavbar.vue";
import {onBeforeUnmount, onMounted, ref} from "vue";
import MobileNavigationBar from "@/components/layout/MobileNavigationBar.vue";

const {t} = useI18n();

const isMobile = ref(false);

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
  <header>
    <Navbar />
  </header>
  <main>
    <RouterView />
  </main>
  <footer>
    <MobileNavigationBar v-show="isMobile" />
  </footer>
</template>

<style scoped lang="scss">
main {
  padding-top: $nav-height * 0.8;
}

</style>
