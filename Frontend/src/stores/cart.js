import { defineStore } from "pinia";

export const useCartStore = defineStore('cart',{
  state: ()=> ({ 
    cart: JSON.parse(localStorage.getItem('cart')) || [],
    takeAway: JSON.parse(localStorage.getItem('takeAway')) ?? true,
   }),
  actions: {
    addToCart(item){
      this.cart.push(item)
      localStorage.setItem('cart', JSON.stringify(this.cart))
    },
    removeFromCart(index){
      this.cart.splice(index, 1)
      localStorage.setItem('cart',JSON.stringify(this.cart))
    },
    setTakeAway(value){
      this.takeAway = value;
      localStorage.setItem('takeAway', JSON.stringify(value));
    },
    clearCart() {
      this.cart = [];
      localStorage.removeItem('cart');
    },
    removeAllOfItem(productId) {
      this.cart = this.cart.filter(p => p.id !== productId);
    }
  }
})