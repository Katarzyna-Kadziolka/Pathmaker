<script setup lang="ts">

import {FontAwesomeIcon} from "@fortawesome/vue-fontawesome";
import type {IconDefinition} from "@fortawesome/free-brands-svg-icons";

defineProps<{
  icon: IconDefinition,
  name: string
  isActive: boolean
}>()
</script>

<template>
  <div class="navbar-button__container" :class="[isActive ? 'navbar-button__active' : '']">
    <font-awesome-icon :icon="icon"/>
    <span>{{ name }}</span>
  </div>
</template>

<style scoped lang="scss">
.navbar-button {
  &__container {
    display: flex;
    align-items: center;
    column-gap: 0.5rem;
    padding: 1rem;
    cursor: pointer;
    background: $color-navbar;
    opacity: 60%;
    transition: opacity 0.3s ease-in-out;
    &:hover {
      opacity: 1;
    }
  }

  &__active {
    opacity: 1;
  }
}
</style>