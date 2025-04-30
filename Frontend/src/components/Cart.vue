<script>
import { ref, computed, onMounted } from 'vue'
import { useCartStore } from '@/stores/cart'

export default {
  data() {
    return {
      cartStore: useCartStore(),
      cartVisible: true
    }
  },

  methods: {
    removeFromCart(item) {
      this.cartStore.cart = this.cartStore.cart.filter(i => i.id != item.id)
      this.cartStore.save()
    },

    makeOrder() {
      alert('Beställknapp fungerar ej än.')
    },

    increaseQuantity(item) {
      this.cartStore.cart.push(item)
      this.cartStore.save()
    },

    decreaseQuantity(item) {
      this.cartStore.cart.splice(this.cartStore.cart.findIndex(i => i.id == item.id), 1)
      this.cartStore.save()
    }
  },

  computed: {
    groupedCartItems() {
      const grouped = {}

      this.cartStore.cart.forEach(item => {
        const key = item.id

        if (!grouped[key])
          grouped[key] = { ...item, quantity: 1 }
        else
          grouped[key].quantity += 1
      })

      return Object.values(grouped)
    }
  }
}
</script>

<template>
  <div class="cart-container" v-if="cartVisible">
    {{ groupedCartItems.length }} produkter

    <ul>
      <li v-for="item in groupedCartItems" :key="item.id" class="cart-li">
        <span>{{ item.name }} - {{ item.price.current }} kr</span>
        <div class="quantity-buttons-container">
          <div class="quantity-control">
            <button class="quantity-button" @click="decreaseQuantity(item)">➖</button>
            <span>{{ item.quantity }}</span>
            <button class="quantity-button" @click="increaseQuantity(item)">➕</button>
          </div>
          <button class="cart-button" @click="removeFromCart(item)">Ta bort</button>
        </div>
      </li>
    </ul>
    <button class="button-secondary" @click="cartStore.takeAway = null; cartStore.save()">
      {{ cartStore.takeAway ? 'Tar med' : 'Äter här' }}
    </button>
    <button class="button" @click="makeOrder">Beställ</button>
    <button class="button" @click="cartVisible = false">Stäng</button>

    <button class="button">
      <router-link to="/checkout" @click="cartVisible = false">Till Kassa</router-link>
    </button>
  </div>
</template>

<style scoped>
.button,
.button-secondary {
  display: inline;
  width: max-content;
}

.cart-container {
  width: 100%;
  padding: 1rem;
  background-color: #fff2be;
  border-radius: 30px 30px 0 0;
}

.cart-container ul {
  padding: 15px;
}

.cart-li span {
  margin-right: 15px;
}

.cart-li {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-top: 5px;
}

.cart-button {
  margin: 5px;
  border: 1px solid black;
  padding: 5px;
  border-radius: 3px;
  text-shadow: 1px 1px rgb(54, 52, 52);
  color: white;
  background-color: #4f4492;
  min-width: 4.5rem;
  cursor: pointer;
}

.quantity-control {
  width: 77px;
  justify-content: end;
  display: flex;
  margin-right: 5px;
}

.quantity-button {
  background: none;
  border: none;
  cursor: pointer;
}

.quantity-buttons-container {
  display: flex;
  align-items: center;
}
</style>
