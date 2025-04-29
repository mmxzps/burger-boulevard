import { defineStore } from 'pinia'

export const useCartStore = defineStore('cart', {
  state: () => ({
    // true = ta med, false = ät här.
    takeAway: JSON.parse(localStorage.getItem('takeAway')),
    cart: JSON.parse(localStorage.getItem('cart')) || []
  }),

  actions: {
    save() {
      localStorage.setItem('takeAway', this.takeAway)

      if (this.cart.length === 0)
        localStorage.removeItem('cart')
      else
        localStorage.setItem('cart', JSON.stringify(this.cart))
    }
  }
})
