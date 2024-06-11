<script setup lang="ts">
import {faDiceD20, faFeather, faSignsPost} from "@fortawesome/free-solid-svg-icons";
import MobileNavigationBarButton from "@/components/layout/MobileNavigationBarButton.vue";
import {useRoute} from "vue-router";
import {computed} from "vue";
import router from "@/router";
import {useI18n} from "vue-i18n";
const {t} = useI18n();

const route = useRoute();

const onItemClick = async (name: string) => {
  await router.push({name: name});
}

const activeItemName = computed(() => route.name);

</script>

<template>
  <div class="mobile-navigation-bar__container">
    <MobileNavigationBarButton :icon=faSignsPost :name="t('layout.explore')" :is-active="activeItemName == 'explore'" @click="() => onItemClick('explore')"/>
    <MobileNavigationBarButton :icon="faDiceD20" :name="t('layout.play')" :is-active="activeItemName == 'play'" @click="() => onItemClick('play')"/>
    <MobileNavigationBarButton :icon="faFeather" :name="t('layout.create')" :is-active="activeItemName == 'create'" @click="() => onItemClick('create')"/>
  </div>
</template>

<style scoped lang="scss">
.mobile-navigation-bar {
  &__container {
    display: flex;
    position: fixed;
    bottom: 0;
    justify-content: space-evenly;
    width: 100%;
    border-top: 0.1rem solid $color-border;
    background: $color-navbar;
  }
}
</style>