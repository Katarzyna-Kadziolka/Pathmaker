<script setup lang="ts">

import {FontAwesomeIcon} from "@fortawesome/vue-fontawesome";
import type {IconDefinition} from "@fortawesome/free-brands-svg-icons";

defineProps<{
  icon: IconDefinition,
  name: string
}>()
</script>

<template>
  <div class="navbar-button__container">
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
    border-radius: 0.5rem;

    &:hover {
      animation-name: hover-container;
      animation-duration: 0.3s;
      animation-fill-mode: forwards;
    }
  }
  @keyframes hover-container {
    from {background-color: $color-navbar;}
    to {background-color: $color-secondary;}
  }
}
</style>