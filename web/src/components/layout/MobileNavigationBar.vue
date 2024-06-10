<script setup lang="ts">
import {faDiceD20, faFeather, faSignsPost} from "@fortawesome/free-solid-svg-icons";
import MobileNavigationBarButton from "@/components/layout/MobileNavigationBarButton.vue";

</script>

<template>
  <div class="mobile-navigation-bar__container">
    <MobileNavigationBarButton :icon=faSignsPost name="Explore"/>
    <MobileNavigationBarButton :icon="faDiceD20" name="Play"/>
    <MobileNavigationBarButton :icon="faFeather" name="Create"/>
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