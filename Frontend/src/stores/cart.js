import { defineStore } from 'pinia'

export const useCartStore = defineStore('cart', {
  state: () => ({
    orderType: null,
    cart: JSON.parse(localStorage.getItem('cart')) || [],
    takeAway: JSON.parse(localStorage.getItem('takeAway')) ?? true,
  }),

  actions: {
    save() {
      if (this.cart.length === 0)
        localStorage.removeItem('cart')
      else
        localStorage.setItem('cart', JSON.stringify(this.cart))
    },

    setOrderType(type) {
      this.orderType = type
      localStorage.setItem('orderType', type)
    },

    setTakeAway(value) {
      this.takeAway = value;
      localStorage.setItem('takeAway', JSON.stringify(value));
    }
  }
})
