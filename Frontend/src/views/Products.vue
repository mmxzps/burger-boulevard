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

        <div v-if="browsingCategory == null" class="category-container">
          <router-link to="/menus">
            <div class="category-div catemeny"></div>
          </router-link>

          <router-link to="/burgers">
            <div class="category-div cateburg"></div>
          </router-link>

          <router-link to="/sides">
            <div class="category-div cateside"></div>
          </router-link>

          <router-link to="/drinks">
            <div class="category-div catedrink"></div>
          </router-link>
        </div>
      </div>
    </div>

    <div class="cart-container">
      <Cart />
    </div>
  </article>
</template>

<style scoped>
.category-container {
  width: 100%;
  height: auto;
  display: flex;
  flex-wrap: wrap;
  max-width: 50rem;
  margin-top: 4rem;
  margin-bottom: 8rem;
  justify-content: center
}

.category-div {
  max-height: 25rem;
  min-height: 16rem;
  flex: 1 1 20rem;
  max-width: 18rem;
  min-width: 16rem;
  margin: 1rem;
  box-shadow: 1px 1px 2px rgb(175, 173, 173);
  border-radius: 2%;
}

.catemeny {
  background-image: url('../categoryImg/menyer.png');
  background-size: cover;
  background-position: center;
  cursor: pointer;
}

.cateburg {
  background-image: url('../categoryImg/burgare.png');
  background-size: cover;
  background-position: center;
}

.catemeny:hover,
.cateside:hover,
.catedrink:hover,
.cateburg:hover {
  transform: scale(1.01);
}

.cateside {
  background-image: url('../categoryImg/tillbehor.png');
  background-size: cover;
  background-position: center;
}

.catedrink {
  background-image: url('../categoryImg/dryck.png');
  background-size: cover;
  background-position: center;
}

article {
  display: flex;
  height: 100vh;
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
