<script>
import { useCartStore } from '@/stores/cart';

export default {
  data() {
    return {
      productList: [],
      cartVisible: true,
    };
  },
  mounted() {
    const cartStore = useCartStore();
    this.loadCart(cartStore);
  },
  methods: {
    loadCart(store) {
      this.productList = store.cart;
    },
    removeFromCart(index) {
      const cartStore = useCartStore();
      cartStore.removeFromCart(index);
      this.loadCart(cartStore); 
    },
    showCart(){
      this.cartVisible = !this.cartVisible
    },
    makeOrder(){
      alert('Beställ knapp fungerar ej. än.');
    }
  }
};

</script>



<template>
  <div class="cart-icon-container">
    <h2 class="cart-icon" @click="showCart()">🛒 {{ productList.length }}</h2>
  </div>
  <div class="cart-container" :class="{visa: cartVisible}">
    <ul>
      <li v-for="(item, index) in productList" :key="index" class="cart-li">
        <span>{{ item.name }} - {{ item.price.current }} kr</span>
        <button class="cart-button" @click="removeFromCart(index)">Ta bort</button>
      </li>
    </ul>
    <button class="cart-button" @click="makeOrder()">Besäll</button>
    <button class="cart-button" @click="showCart()">Stäng</button>

  </div>
</template>


<style>
.cart-icon-container:hover{cursor: pointer;}
.cart-icon-container{
  width: 100px;
  height: 50px;
}

.cart-container ul{ 
  padding: 15px;
}
.visa{
  display: none;
}
.cart-container {
  margin-bottom: 30px;
  border: 1px solid wheat;
  position: fixed;
  right: 10px;
  top: 35px;
  background-color: #242323;
  color:rgb(196, 190, 190);
  padding: 10px;
  z-index: 1000;
  width: 25rem;
  border-radius: 1%;
}
.cart-li span{
  margin-right: 15px;
}
.cart-li{
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-top: 5px;
}

.cart-button{
  margin: 5px;
  border:1px solid black;
  padding: 5px;
  border-radius: 3px;
  text-shadow: 1px 1px rgb(54, 52, 52);
  color:white;
  font: 1em sans-serif;
  background-color: #4f4492;
  width: 4.5rem;
}
</style>