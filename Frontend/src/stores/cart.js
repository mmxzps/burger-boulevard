import { defineStore } from 'pinia'

export const useCartStore = defineStore('cart', {
  state: () => ({
    // true = ta med, false = ät här.
    orderType: JSON.parse(localStorage.getItem('orderType')),
    cart: JSON.parse(localStorage.getItem('cart')) || []
  }),

  actions: {
    save() {
      localStorage.setItem('orderType', this.orderType)

      if (this.cart.length === 0)
        localStorage.removeItem('cart')
      else
        localStorage.setItem('cart', JSON.stringify(this.cart))
    }
  }
})
