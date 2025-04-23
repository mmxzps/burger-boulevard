<script setup>
import NavBar from './components/NavBar.vue'
import ChooseOrderType from './components/ChooseOrderType.vue'
import { useCartStore } from '@/stores/cart'
import { onMounted } from 'vue'

const cart = useCartStore()

function setOrderType(type) {
  cart.setOrderType(type)
}

onMounted(() => {
  
  const savedType = localStorage.getItem('orderType')
  if (savedType) {
  
    cart.setOrderType(savedType)
  } else {
    
    cart.setOrderType(null)
}
})
</script>

<template>
  <div id="app">
    <NavBar />
    <ChooseOrderType
      v-if="!cart.orderType"
      @choose="setOrderType"
    />
    <main v-else>
      <router-view />
    </main>
  </div>
</template>

<style>
body {
  margin: 0;
  padding: 0;
  font-family: Arial, sans-serif;
}

#app {
  min-height: 100vh;
}

main {
  padding-top: 60px;
}
</style>