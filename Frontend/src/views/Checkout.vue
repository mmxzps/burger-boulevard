<script>
import * as api from '@/api'
import { evaluateCost, diff } from '@/util'
import { useCartStore } from '@/stores/cart'
import { useApiCacheStore } from '@/stores/apiCache'

export default {
  data() {
    return {
      cartStore: useCartStore(),
      components: null,
      orderId: null
    }
  },

  async mounted() {
    this.components = await useApiCacheStore().components
  },

  methods: {
    diff,
    evaluateCost,

    copyComponent(componentTree) {
      this.cartStore.cart.push(componentTree)
      this.cartStore.save()
    },

    removeComponent(componentTree) {
      this.cartStore.cart.splice(
        this.cartStore.cart.findIndex(o => o == componentTree), 1)
      this.cartStore.save()
    },

    placeOrder() {
      api.createOrder(this.cartStore.cart, this.cartStore.takeAway)
        .then(r => this.orderId = r.data.id)

      this.cartStore.cart = []
      this.cartStore.save()
    }
  },

  computed: {
    totalPrice() {
      if (!this.components) return 0
      return this.cartStore.cart
        .reduce((sum, i) => sum + evaluateCost(this.components, i), 0)
    }
  }
}
</script>

<template>
  <router-link to="/" class="back-button">← Tillbaka</router-link>
  <div v-if="orderId != null" class="confirm-container">
    <h2>Tack för din beställning!</h2>
    <p>Din order har skickats till köket.</p>
    <h3>Ordernummer: {{ orderId }}</h3>
  </div>

  <div v-else class="order-container">
    <h1>Varukorg</h1>
    <div v-if="cartStore.cart.length <= 0">
      <p>Din kundvagn är tom!</p>
    </div>
    <div v-else class="order-info-container">
      <ul>
        <li v-if="components" v-for="(componentTree, index) in cartStore.cart" :key="index"
          :set="component = components.find(({ id }) => id == componentTree.componentId)">
          <div class="order-name-price">
            <h2>{{ component.name }}</h2>

            <span class="order-price">{{ evaluateCost(components, componentTree) }} kr</span>

            <button class="button quantity-button" @click="removeComponent(componentTree)">-</button>
            <button class="button quantity-button" @click="copyComponent(componentTree)">+</button>

            <template v-for="[changeType, changedComponents] in Object.entries(diff(components, componentTree))">
              <div v-for="changedComponent in changedComponents" :key="changedComponent.id">
                <div
                  :class="{ 'children-change': true, 'children-change-added': changeType == 'added', 'children-change-removed': changeType == 'removed' }">
                  <span>{{ changedComponent.name }}</span>
                  <span v-if="changeType == 'added'" class="order-price">{{ changedComponent.price.current }} kr</span>
                </div>
              </div>
            </template>
          </div>
        </li>
      </ul>
      <div>
        <button :class="{
          button: !cartStore.takeAway, 'button-secondary': cartStore.takeAway,
          'takeaway-button': true
        }" @click="cartStore.takeAway = false; cartStore.save()">
          Ät här
        </button>

        <button :class="{
          button: cartStore.takeAway, 'button-secondary': !cartStore.takeAway,
          'takeaway-button': true
        }" @click="cartStore.takeAway = true; cartStore.save()">
          Ta med
        </button>

        <br><br>
        <button class="button" @click="placeOrder">Beställ &mdash; {{ totalPrice }}kr</button>
      </div>
    </div>
  </div>
</template>

<style>
.children-change {
  display: inline-block;
}

.children-change-added::before,
.children-change-removed::before {
  vertical-align: middle;
  font-size: 1.5rem;
  font-weight: bold;
  margin: 1rem;
}

.children-change-added {
  color: green;
}

.children-change-added::before {
  content: '+';
}

.children-change-removed {
  color: red;
}

.children-change-removed::before {
  content: '-';
}

.takeaway-button {
  display: inline;
  width: fit-content;
}

.order-container {
  display: flex;
  flex-direction: column;
  align-items: center;
}

.order-name-price {
  justify-content: space-between;
  width: 100%;
  padding: 5px;
}

ul {
  padding: 0;
  list-style: none;
}

.order-price {
  display: inline-block;
  width: 6rem;
  margin: 0 1rem;
  font-weight: bold;
}

.quantity-button {
  display: inline;
  width: min-content;
  padding: 0 .3rem;
  font-size: 1rem;
  font-weight: bold;
}
</style>
