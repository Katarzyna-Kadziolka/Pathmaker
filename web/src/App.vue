<script setup lang="ts">
import { RouterView } from 'vue-router'
import {useI18n} from 'vue-i18n'
import Navbar from "@/components/layout/AppNavbar.vue";
import MobileNavigationBar from "@/components/layout/MobileNavigationBar.vue";

const {t} = useI18n();

</script>

<template>
  <header>
    <Navbar />
  </header>
  <main>
    <RouterView />
  </main>
  <footer>
    <MobileNavigationBar />
  </footer>
</template>

<style scoped lang="scss">
main {
  padding-top: $nav-height * 1.5;
}
footer {
  @media (min-width:768px) {
    display: none;
  }
}
</style>
