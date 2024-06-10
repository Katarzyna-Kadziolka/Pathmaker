<script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router'
import {useI18n} from 'vue-i18n'
import Navbar from "@/components/layout/AppNavbar.vue";

const {t} = useI18n();
</script>

<template>
  <header>
    <Navbar />
  </header>
  <main>
    <RouterView />
  </main>
</template>

<style scoped lang="scss">
main {
  padding-top: $nav-height * 0.8;
}

</style>
