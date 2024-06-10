<script setup lang="ts">
import type {IconDefinition} from "@fortawesome/free-brands-svg-icons";
import {FontAwesomeIcon} from "@fortawesome/vue-fontawesome";

defineProps<{
  icon: IconDefinition,
  name: string,
  isActive: boolean
}>()
</script>

<template>
<div class="mobile-navigation-bar-button__container" :class="[isActive ? 'mobile-navigation-bar-button__active' : '']">
  <div class="mobile-navigation-bar-button__icon">
    <font-awesome-icon :icon="icon" />
  </div>
  <span>{{ name }}</span>
</div>
</template>

<style scoped lang="scss">
.mobile-navigation-bar-button {
  &__container {
    display: flex;
    flex-direction: column;
    align-items: center;
    padding: 0.5rem;
    opacity: 60%;
  }
  &__icon {
    font-size: 1.5rem;
  }
  &__active {
    opacity: 80%;
  }
}
</style>