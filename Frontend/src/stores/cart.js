import { defineStore } from "pinia";

export const useCartStore = defineStore('cart',{
  state: ()=> ({ cart: JSON.parse(localStorage.getItem('cart')) || [] }),
  actions: {
    addToCart(item){
      this.cart.push(item)
      localStorage.setItem('cart', JSON.stringify(this.cart))
    },
    removeFromCart(index){
      this.cart.splice(index, 1)
      localStorage.setItem('cart',JSON.stringify(this.cart))
    }
  }
})