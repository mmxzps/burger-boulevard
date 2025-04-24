<script>
import { useCartStore } from '@/stores/cart'
import { baseUrl } from '@/api.js'

export default {
  data() {
    return {
      baseUrl
    }
  },

  props: {
    component: Object
  },

  methods: {
    addToCart(component) {
      const cartStore = useCartStore()
      cartStore.cart.push(component)
      cartStore.save()
    }
  }
};
</script>

<template>
  <div class="product">
    <img :src="baseUrl + component.imageUrl" alt="" class="product-image" />
    <span class="product-name">{{ component.name }}</span>
    <p class="product-description">{{ component.description }}</p>
    <span class="product-price">{{ component.price.current }}kr</span>
    <button class="button" @click="addToCart(component)">Lägg till</button>
  </div>
</template>

<style scoped>
.product {
  width: 20rem;
}

.product-image {
  display: block;
  margin: 0 auto;
  height: 14rem;
  width: auto;
  border-radius: 5px;
}

.product-name {
  font-size: 1.6rem;
  font-weight: bold;
}

.product-description {
  font-size: 1.1rem;
  color: #333;
}

.product-price {
  font-size: 1.5rem;
}
</style>
