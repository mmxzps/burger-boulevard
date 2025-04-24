<script>
import { useCartStore } from '@/stores/cart'

export default {
  data() {
    return {
      cartStore: useCartStore(),
      cartVisible: false
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
  <h2 class="cart-icon" @click="cartVisible = !cartVisible">🛒 {{ cartStore.cart.length }}</h2>

  <div class="cart-container" v-if="cartVisible">
    <ul>
      <li v-for="item in groupedCartItems" :key="item.id" class="cart-li">
        <span>{{ item.name }} - {{ item.price.current }} kr</span>
        <div class="quantity-buttons-container">
          <div class="quantity-control">
            <button class="quantity-button" @click="decreaseQuantity(item)">➖</button>
            <span id="quantity">{{ item.quantity }}</span>
            <button class="quantity-button" @click="increaseQuantity(item)">➕</button>
          </div>
          <button class="cart-button" @click="removeFromCart(item)">Ta bort</button>
        </div>
      </li>
    </ul>
    <button class="cart-button" @click="makeOrder">Beställ</button>
    <button class="cart-button" @click="cartVisible = false">Stäng</button>
  </div>
</template>

<style>
.cart-icon:hover {
  cursor: pointer;
}

.cart-icon-container {
  width: 50px;
  height: 50px;
}

.cart-container ul {
  padding: 15px;
}

.cart-container {
  border: 1px solid wheat;
  position: fixed;
  right: 10px;
  top: 45px;
  background-color: #242323;
  color: rgb(196, 190, 190);
  padding: 10px;
  z-index: 1000;
  width: 23rem;
  border-radius: 1%;
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
  font: 1em sans-serif;
  background-color: #4f4492;
  width: 4.5rem;
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

#quantity {
  margin: 4px;
}

.quantity-buttons-container {
  display: flex;
  align-items: center;
}
</style>
