<script>
import * as api from '@/api'
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
    this.components = (await useApiCacheStore().components).data
  },

  methods: {
    diff(componentTree) {
      const policies = this.components.find(({ id }) => id == componentTree.componentId).childPolicies

      return policies.reduce((diffs, policy) => {
        const childrenByPolicy = componentTree.children.filter(o => o.componentId == policy.child.id)
        const added = childrenByPolicy.length - policy.default

        const changes = Array(Math.abs(added))
          .fill(this.components.find(({ id }) => id == policy.child.id))

        if (added > 0)
          diffs.added.push(...changes)
        else if (added < 0)
          diffs.removed.push(...changes)

        return diffs
      }, {
        added: [],
        removed: []
      })
    },

    increaseQuantity(componentTree) {
      this.cartStore.cart.push(componentTree)
      this.cartStore.save()
    },

    decreaseQuantity(componentTree) {
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
      return this.cartStore.cart.reduce((sum, item) =>
        sum + this.components?.find(({ id }) => id == item.componentId).price.current, 0)
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
    <h1>Summering</h1>
    <div v-if="cartStore.cart.length <= 0">
      <p>Din kundvagn är tom!</p>
    </div>
    <div v-else class="order-info-container">
      <ul>
        <li v-if="components" v-for="(componentTree, index) in cartStore.cart" :key="index"
          :set="component = components.find(({ id }) => id == componentTree.componentId)">
          <div class="order-name-price">
            <span>{{ component.name }}</span>

            <div class="quantity-holder">
              <button class="quantity-button" @click="decreaseQuantity(componentTree)">➖</button>
              <button class="quantity-button" @click="increaseQuantity(componentTree)">➕</button>
              <span class="order-price">{{ component.price.current }} kr</span>
            </div>

            <div v-for="[changeType, changedComponents] in Object.entries(diff(componentTree))">
              <br>
              <span v-if="changeType == 'added'">+</span>
              <span v-else-if="changeType == 'removed'">-</span>
              <div v-for="changedComponent in changedComponents" :key="changedComponent.id">
                <span>{{ changedComponent.name }}</span>
                <span v-if="changeType == 'added'" class="order-price">{{ changedComponent.price.current }} kr</span>
              </div>
            </div>
          </div>
        </li>
      </ul>
      <hr class="order-divider" />
      <div>
        <h3>Totalt: {{ totalPrice }} kr</h3>
      </div>
      <div>
        <button class="order-button" @click="placeOrder">Beställ</button>

        <button style="display: inline; width: fit-content;"
          :class="{ button: !cartStore.takeAway, 'button-secondary': cartStore.takeAway }"
          @click="cartStore.takeAway = false; cartStore.save()">
          Ät här
        </button>

        <button style="display: inline; width: fit-content;"
          :class="{ button: cartStore.takeAway, 'button-secondary': !cartStore.takeAway }"
          @click="cartStore.takeAway = true; cartStore.save()">
          Ta med
        </button>
      </div>
    </div>
  </div>
</template>

<style>
.confirm-container {
  display: flex;
  flex-direction: column;
  width: 100%;
  margin: 5rem auto;
  text-align: center;
  text-shadow: -1px 3px 50px #4f4492;
}

.confirm-container h3 {
  margin-top: 1rem;
  font-size: x-large;
  color: darkgoldenrod;
}

.order-container {
  display: flex;
  flex-direction: column;
  align-items: center;
  margin-top: 4rem;
}

.order-info-container {
  border: 1px solid #282828;
  display: flex;
  flex-direction: column;
  align-items: stretch;
  padding: 10px;
  min-width: 22rem;
}

.order-name-price {
  display: flex;
  justify-content: space-between;
  width: 100%;
  padding: 5px;
}

ul {
  width: 100%;
  padding: 0;
  list-style: none;
}

.order-button {
  width: 5rem;
  border: none;
  height: 2rem;
  margin: 6px 3px;
  background-color: #4f4492;
  color: hsla(160, 100%, 37%, 1);
  cursor: pointer;
  border-radius: 3px;
  font: 1em sans-serif;
}

.order-price {
  display: inline-block;
  width: 3.5rem;
  text-align: end;
}

.order-divider {
  margin: 1rem 0;
}
</style>
