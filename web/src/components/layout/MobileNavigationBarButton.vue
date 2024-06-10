<script setup lang="ts">
import type {IconDefinition} from "@fortawesome/free-brands-svg-icons";
import {FontAwesomeIcon} from "@fortawesome/vue-fontawesome";

defineProps<{
  icon: IconDefinition,
  name: string
}>()
</script>

<template>
<div class="mobile-navigation-bar-button__container">
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
    padding: 1rem;
  }
  &__icon {
    font-size: 2rem;
  }
}
</style>