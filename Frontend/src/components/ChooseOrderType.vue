<template>
  <div class="order-type-overlay">
    <div class="order-type-modal">
      <h2 class="order-type-heading">
        Vill du?
      </h2>
      <div class="order-type-buttons">
        <button @click="choose('eatHere')" class="order-button eat-here-button">
          <span>Äta här</span>
        </button>
        <button @click="choose('takeAway')" class="order-button take-away-button">
          <span>Ta med</span>
        </button>
      </div>
    </div>
  </div>
</template>

<script setup>
import { onMounted, onUnmounted } from 'vue'

const emit = defineEmits(['choose'])

// Methods
function choose(type) {
  console.log('ChooseOrderType: Selected', type)
  emit('choose', type)
}

// Lifecycle hooks
onMounted(() => {
  // Prevent background scrolling
  document.documentElement.style.overflow = 'hidden'
  document.body.style.overflow = 'hidden'
})

onUnmounted(() => {
  // Restore scrolling
  document.documentElement.style.overflow = ''
  document.body.style.overflow = ''
})
</script>

<style scoped>
.order-type-overlay {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: #242323;
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 100;
  padding: 1rem;
}

.order-type-modal {
  text-align: center;
  max-width: 28rem;
  width: 100%;
  background-color: #242323;
  border-radius: 12px;
  padding: 2rem;
}

.order-type-heading {
  font-size: 1.8rem;
  font-weight: 600;
  margin-bottom: 2rem;
  color: white;
  text-shadow: 1px 1px 2px rgba(0, 0, 0, 0.5);
}

.order-type-buttons {
  display: flex;
  flex-direction: column;
  gap: 1.5rem;
}

@media (min-width: 640px) {
  .order-type-heading {
    font-size: 2rem;
  }

  .order-type-buttons {
    flex-direction: row;
    justify-content: center;
    gap: 2.5rem;
  }
}

.order-button {
  position: relative;
  width: 100%;
  padding: 1rem 1.5rem;
  border-radius: 8px;
  font-size: 1.2rem;
  font-weight: 500;
  border: none;
  transition: all 0.3s ease;
  color: white;
  text-transform: uppercase;
  letter-spacing: 1px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  overflow: hidden;
}

.order-button::before {
  content: '';
  position: absolute;
  top: 0;
  left: 0;
  width: 0;
  height: 100%;
  background-color: rgba(255, 255, 255, 0.1);
  transition: width 0.3s ease;
  z-index: 1;
}

.order-button:hover::before {
  width: 100%;
}

.order-button span {
  position: relative;
  z-index: 2;
}

@media (min-width: 640px) {
  .order-button {
    width: 180px;
  }
}

.eat-here-button {
  background-color: #4f4492;
}

.eat-here-button:hover {
  background-color: #3b3370;
  transform: translateY(-2px);
}

.take-away-button {
  background-color: #4f4492;
}

.take-away-button:hover {
  background-color: #3b3370;
  transform: translateY(-2px);
}
</style>
