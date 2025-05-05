<template>
  <div>
    <button @click="emit('click', targetStatus)" class="button">
      <slot>{{ arrow }}</slot>
    </button>
  </div>
</template>

<script setup>
const {  arrow, targetStatus } = defineProps({
  arrow: {
    type: String,
    required: true
  },
  targetStatus: {
    type: String,
    required: true
  }
});

const emit = defineEmits(['click']);
</script>

<style scoped>
button {
  padding: 0.25rem 0.5rem;
  background: linear-gradient(135deg, #4f46e5, #3b82f6);
  color: white;
  border: none;
  border-radius: 0.5rem;
  font-weight: 600;
  font-size: 1.8rem;
  box-shadow: 0 4px 12px rgba(59, 130, 246, 0.3);
  transition: all 0.2s ease-in-out;
  cursor: pointer;
  width: 100%;
}

button:hover {
  background: linear-gradient(135deg, #4338ca, #2563eb);
  transform: translateY(-2px);
  box-shadow: 0 6px 16px rgba(59, 130, 246, 0.4);
}

button:active {
  transform: scale(0.97);
  box-shadow: 0 2px 6px rgba(59, 130, 246, 0.2);
}
</style>

