<script>
import * as api from '@/api.js'
import { useApiCacheStore } from '@/stores/apiCache'
import { useCartStore } from '@/stores/cart'

import ProductCard from '@/components/ProductCard.vue'
import CategoryNavigation from '@/components/CategoryNavigation.vue'
import TakeAwayChoice from '@/components/TakeAwayChoice.vue'
import Cart from '@/components/Cart.vue'
import HomeHeader from '@/components/HomeHeader.vue'

export default {
  data: () => ({
    cart: useCartStore(),
    baseUrl: api.baseUrl,
    components: [],
    categories: []
  }),

  computed: {
    browsingCategoryId() {
      return this.$route.params.category || null
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
    Cart,
    HomeHeader
  }
}
</script>

<template>
  <div><HomeHeader/></div>
  <TakeAwayChoice v-if="cart.takeAway == null" @choose="choice => { cart.takeAway = choice; cart.save() }" />
    
  <article v-else>
    <Transition mode="out-in">
      <div v-if="browsingCategory != null">
        <CategoryNavigation />
      </div>
    </Transition>

    <div class="content-container">
      <h1>{{ browsingCategory?.name }}</h1>
      <div class="products-container">
        <ProductCard v-for="component in categoryComponents" :key="component.id" :component="component" />

        <div v-if="browsingCategory == null" class="category-container">
          <template v-for="category in categories">
            <router-link :to="'/' + category.id">
              <img :src="category.imageUrl ? baseUrl + category.imageUrl : ''" class="category-div"
                :alt="category.name">
            </router-link>
          </template>
        </div>
      </div>
    </div>

    <div class="cart-container">
      <Cart />
    </div>
  </article>
</template>

<style scoped>
.v-enter-active {
  transition: transform .2s ease;
}

.v-enter-from {
  transform: translateX(-100%);
}

.category-container {
  display: flex;
  gap: 1rem;
  width: 100%;
  height: auto;
  flex-wrap: wrap;
  max-width: 50rem;
  margin-top: 4rem;
  margin-bottom: 8rem;
  justify-content: center
}

.category-div {
  width: 18rem;
  border-radius: 10%;
}

article {
  display: flex;
  height: 91vh;
  overflow: scroll;
}

.content-container {
  padding: 2em;
  height: fit-content;
  padding-bottom: 9rem;
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
