<script>
import * as api from '@/api.js'
import { useCartStore } from '@/stores/cart'

import ProductCard from '@/components/ProductCard.vue'
import CategoryNavigation from '@/components/CategoryNavigation.vue'
import ChooseOrderType from '@/components/ChooseOrderType.vue'
import Cart from '@/components/Cart.vue'

export default {
  data() {
    return {
      cart: useCartStore(),
      products: [], // All components fetched from the API
      categories: [] // Categories from the API
    }
  },

  computed: {
    browsingCategoryId() {
      return this.$route.params.category
    },

    browsingCategory() {
      return this.categories.find(c => c.id == this.browsingCategoryId)
    },

    categoryProducts() {
      return this.products.filter(p => p.categories.some(c => c.id == this.browsingCategoryId))
    }
  },

  async created() {
    this.categories = (await api.getCategories()).data
    this.products = (await api.getComponents(1)).data
  },

  components: {
    ChooseOrderType,
    CategoryNavigation,
    ProductCard,
    Cart
  }
}
</script>

<template>
  <ChooseOrderType v-if="cart.takeAway == null" @choose="choice => { cart.takeAway = choice; cart.save() }" />

  <article v-else>
    <div>
      <CategoryNavigation />
      
    </div>

    <div class="content-container">
      <h1>{{ browsingCategory?.name }}</h1>
      <div class="products-container">
        <ProductCard v-for="product in categoryProducts" :key="product.id" :component="product" />
        <div v-if="browsingCategory == null"><p>hej</p></div>
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
