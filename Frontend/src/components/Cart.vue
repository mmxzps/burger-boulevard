<script>
import { useCartStore } from '@/stores/cart'

export default {
  data() {
    return {
      cartStore: useCartStore()
    }
  }
}
</script>

<template>
  <Transition mode="out-in">
    <div class="cart-container" v-show="cartStore.cart.length > 0">
      <router-link to="/checkout" class="button">
        Fortsätt &ndash; {{ cartStore.totalPrice }} kr ({{ cartStore.cart.length }}st)
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
