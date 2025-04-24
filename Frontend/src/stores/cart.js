import { defineStore } from "pinia"

export const useCartStore = defineStore('cart', {
  state: () => ({
    // TODO: Add take-away state
    cart: JSON.parse(localStorage.getItem('cart')) || []
  }),

  actions: {
    save() {
      localStorage.setItem('cart', JSON.stringify(this.cart))
    }
  }
})
