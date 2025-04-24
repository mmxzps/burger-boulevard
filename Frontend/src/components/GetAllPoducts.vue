<script>
import * as api from '@/api.js'
import ProductCard from './ProductCard.vue'

export default {
  data() {
    return {
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
    ProductCard
  }
}
</script>

<template>
  <h1>{{ browsingCategory?.name }}</h1>
  <div class="products-container">
    <ProductCard v-for="product in categoryProducts" :key="product.id" :component="product" />
  </div>
</template>

<style scoped>
.products-container {
  display: flex;
  flex-wrap: wrap;
  gap: 2rem;

  padding: 2rem;
  margin: 0 auto;
}

h1 {
  font-weight: bold;
  text-align: center;
}
</style>
