<script setup lang="ts">
defineProps<{
  text: string
}>()
</script>

<template>
  <div class="base-button__container">
    {{text}}
  </div>
</template>

<style scoped lang="scss">
.base-button {
  &__container {
    display: flex;
    padding: 0.5rem;
    border-radius: 8px;
    border: 2px solid $color-border;
    opacity: 60%;
    cursor: pointer;
    transition: opacity 0.2s ease-in-out;
    box-shadow: 0px 1px 5px 1px $color-border;
    &:hover {
      opacity: 1;
    }
  }
}

</style>