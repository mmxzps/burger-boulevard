<script setup>
import WelcomeItem from './WelcomeItem.vue'
import SupportIcon from './icons/IconSupport.vue'

const openReadmeInEditor = () => fetch('/__open-in-editor?file=README.md')
</script>

<template>

  <WelcomeItem>
    <template #icon>
      <SupportIcon />
    </template>
    <template #heading>Featured Burgers</template>

  </WelcomeItem>

</template>
