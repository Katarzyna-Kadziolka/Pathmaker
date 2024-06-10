<script setup lang="ts">
import NavbarButton from "@/components/layout/AppNavbarButton.vue";
import {computed} from "vue";
import {faSignsPost, faDiceD20, faFeather, faArrowRightToBracket} from "@fortawesome/free-solid-svg-icons";
import {useRoute} from "vue-router";
import router from "@/router";

const route = useRoute();

const onItemClick = async (name: string) => {
  await router.push({name: name});
}

const activeItemName = computed(() => route.name);
</script>

<template>
  <div class="navbar__container">
    <div class="navbar__web">
      <img src="@/assets/Pathmaker-logo.png" alt="">
      <div class="navbar__navigation">
        <NavbarButton :icon=faSignsPost name="Explore" :is-active="activeItemName == 'explore'" @click="() => onItemClick('explore')"/>
        <NavbarButton :icon="faDiceD20" name="Play" :is-active="activeItemName == 'play'" @click="() => onItemClick('play')"/>
        <NavbarButton :icon="faFeather" name="Create" :is-active="activeItemName == 'create'" @click="() => onItemClick('create')"/>
      </div>

      <NavbarButton class="navbar__navigation-account" :icon="faArrowRightToBracket" name="Log In" :is-active="activeItemName == 'logIn'" @click="() => onItemClick('logIn')"/>
    </div>
    <div class="navbar__mobile">
      <img src="@/assets/Pathmaker-logo.png" alt="">
      <div class="navbar__account" :class="[activeItemName === 'logIn' ? 'navbar__active' : '']"
           @click="() => onItemClick('logIn')">
        <font-awesome-icon :icon="faArrowRightToBracket"></font-awesome-icon>
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss">
img {
  padding-left: 0.5rem;
  height: $nav-height *0.8;

  @media(min-width: 768px) {
    max-width: 30%;
  }
}

.navbar {
  &__container {
    padding: 0.5rem 1rem 0.5rem 0;
    background: $color-navbar;
    position: fixed;
    z-index: 1;
    width: 100%;
    border-bottom: 0.1rem solid $color-border;
  }
  &__web {
    height: $nav-height;
    display: flex;
    align-items: center;
    width: 100%;
    @media(max-width: 769px) {
      display: none;
    }
  }
  &__mobile {
    height: $nav-height;
    display: flex;
    align-items: center;
    justify-content: space-between;
    @media(min-width: 768px) {
      display: none;
    }
  }

  &__account {
    display: flex;
    padding: 1rem;
    cursor: pointer;
    font-size: 1.5rem;
    opacity: 50%;
  }

  &__active {
    opacity: 80%;
  }
  &__navigation {
    display: flex;
    padding: 1rem;
    column-gap: 1rem;
  }
  &__navigation-account {
    margin-left: auto;
    margin-right: 1rem;
  }
}

</style>