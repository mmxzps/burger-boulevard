<script>
import { useApiCacheStore } from '@/stores/apiCache'
import { useCartStore } from '@/stores/cart'
import { evaluateCost } from '@/util'

export default {
  data: () => ({
    cartStore: useCartStore(),
    components: null
  }),

  async mounted() {
    this.components = await useApiCacheStore().components
  },

  computed: {
    totalPrice() {
      if (!this.components) return 0
      return this.cartStore.cart
        .reduce((sum, i) =>
          sum + evaluateCost(this.components, i), 0)
    }
  }
}
</script>

<template>
  <Transition mode="out-in">
    <div class="cart-container" v-show="cartStore.cart.length > 0">
      <router-link to="/checkout" class="button">
        Fortsätt &ndash; {{ totalPrice }} kr ({{ cartStore.cart.length }}st)
      </router-link>
    </div>
  </Transition>
</template>

<style scoped>
.v-enter-active,
.v-leave-active {
  transition: transform .3s ease;
}

.v-enter-from,
.v-leave-to {
  transform: translateY(100%);
}

.button,
.button-secondary {
  display: inline;
  width: max-content;
}

.cart-container {
  display: flex;
  justify-content: end;
  width: 100%;
  padding: 1rem;
  background-color: #fff2be;
}

@media screen and (min-width: 600px) {

  .button,
  .button-secondary {
    width: max-content;
  }
}
</style>
