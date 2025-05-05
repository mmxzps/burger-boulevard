<script>
import * as api from '@/api.js'
import { useApiCacheStore } from '@/stores/apiCache'
import { useCartStore } from '@/stores/cart'

import ProductCard from '@/components/ProductCard.vue'
import CategoryNavigation from '@/components/CategoryNavigation.vue'
import TakeAwayChoice from '@/components/TakeAwayChoice.vue'
import Cart from '@/components/Cart.vue'

export default {
  data() {
    return {
      cart: useCartStore(),
      components: [],
      categories: []
    }
  },

  computed: {
    browsingCategoryId() {
      return this.$route.params.category
    },

    browsingCategory() {
      return this.categories.find(c => c.id == this.browsingCategoryId)
    },

    categoryComponents() {
      return this.components.filter(p => p.categories.some(c => c.id == this.browsingCategoryId))
    }
  },

  async mounted() {
    this.categories = await useApiCacheStore().categories
    this.components = await useApiCacheStore().components
  },

  components: {
    TakeAwayChoice,
    CategoryNavigation,
    ProductCard,
    Cart
  }
}
</script>

<template>
  <TakeAwayChoice v-if="cart.takeAway == null" @choose="choice => { cart.takeAway = choice; cart.save() }" />

  <article v-else>
    <div>
      <CategoryNavigation />
    </div>

    <div class="content-container">
      <h1>{{ browsingCategory?.name }}</h1>
      <div class="products-container">
        <ProductCard v-for="component in categoryComponents" :key="component.id" :component="component" />

        <div v-if="browsingCategory == null">
          <p>hej</p>
        </div>
      </div>
    </div>

    <div class="cart-container">
      <Cart />
    </div>
  </article>
</template>

<style scoped>
article {
  display: flex;
  height: 100vh;
}

.content-container {
  padding: 2em;
  max-height: 100%;
  overflow: scroll;
}

.products-container {
  display: flex;
  flex-wrap: wrap;
  gap: 2rem;
  margin: 0 auto;
  margin-bottom: 10vh;
}

.cart-container {
  position: fixed;
  bottom: 0;
}

h1 {
  font-weight: bold;
  margin: 2rem 0;
}
</style>
