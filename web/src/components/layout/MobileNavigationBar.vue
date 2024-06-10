<script setup lang="ts">
import {faDiceD20, faFeather, faSignsPost} from "@fortawesome/free-solid-svg-icons";
import MobileNavigationBarButton from "@/components/layout/MobileNavigationBarButton.vue";
import {useRoute} from "vue-router";
import {ref} from "vue";
import router from "@/router";

const route = useRoute();
let activeItemName = ref(route.name);

const onItemClick = async (name: string) => {
  activeItemName.value = name;
  await router.push({name: name});
}

</script>

<template>
  <div class="mobile-navigation-bar__container">
    <MobileNavigationBarButton :icon=faSignsPost name="Explore" :is-active="activeItemName == 'explore'" @click="() => onItemClick('explore')"/>
    <MobileNavigationBarButton :icon="faDiceD20" name="Play" :is-active="activeItemName == 'play'" @click="() => onItemClick('play')"/>
    <MobileNavigationBarButton :icon="faFeather" name="Create" :is-active="activeItemName == 'create'" @click="() => onItemClick('create')"/>
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