import { defineStore } from 'pinia'

const keys = {
  takeAway: 'takeAway',
  cart: 'cart'
}

export const useCartStore = defineStore('cart', {
  state: () => ({
    // true = ta med, false = ät här.
    takeAway: JSON.parse(localStorage.getItem(keys.takeAway)),
    cart: JSON.parse(localStorage.getItem(keys.cart)) || []
  }),

  actions: {
    save() {
      if (this.takeAway === null)
        localStorage.removeItem(keys.takeAway)
      else
        localStorage.setItem(keys.takeAway, this.takeAway)

      if (this.cart.length === 0)
        localStorage.removeItem(keys.cart)
      else
        localStorage.setItem(keys.cart, JSON.stringify(this.cart))
    }
  },

  getters: {
    totalPrice() {
      // TODO: Calculate total cart price
      return 0
    }
  }
})
