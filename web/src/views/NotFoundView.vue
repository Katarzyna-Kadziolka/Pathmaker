<script setup lang="ts">
import BaseButton from "@/components/common/BaseButton.vue";
import {useI18n} from "vue-i18n";
import router from "@/router";

const {t} = useI18n();

</script>

<template>
  <div class="not-found-view__container">
    <span class="not-found-view__code">{{t('not-found.code')}}</span>
    <span class="not-found-view__message">{{t('not-found.message')}}</span>
    <BaseButton :text="t('not-found.button')" class="not-found-view__button" @click="async () => await router.push({name: 'explore'})"/>
  </div>
</template>

<style scoped lang="scss">
.not-found-view {
  &__container {
    display: flex;
    height: 80vh;
    flex-direction: column;
    align-items: center;
    justify-content: center;
  }
  &__code {
    font-size: 4rem;
  }
  &__message {
    font-size: 2rem;
  }
  &__button {
    margin-top: 2rem;
  }
}
</style>