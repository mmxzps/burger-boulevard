<script setup>
const emit = defineEmits(['choose'])
</script>

<template>
  <div class="container">
    <h1>Var vill du äta?</h1>
    <button @click="$emit('choose', false)" class="button">Ät här</button>
    <button @click="$emit('choose', true)" class="button">Ta med</button>
  </div>
</template>

<style scoped>
.container {
  display: flex;
  justify-content: center;
  align-items: center;
  flex-direction: column;
  gap: 2.5rem;

  width: 50%;
  margin: 0 auto;
  padding-top: 10%;
}
</style>
