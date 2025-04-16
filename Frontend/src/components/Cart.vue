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
    },
    increaseQuantity(item){
      const cartStore = useCartStore();
      cartStore.addToCart(item);
      this.loadCart(cartStore);
    },
    decreaseQuantity(item){
      const cartStore = useCartStore();
      const index = this.productList.findIndex(p=>p.id == item.id);

      if(index !== -1){
        cartStore.removeFromCart(index);
        this.loadCart(cartStore);
      }
    }
  },
  computed:{
    groupedCartItems(){
      const grouped = {};

      this.productList.forEach(item =>{
        const key = item.id;

        if(!grouped[key]){
          grouped[key]={
            ...item,
            quantity:1
          };
        }else{
          grouped[key].quantity +=1;
        }
      });

      return Object.values(grouped);
    }
  }

};

</script>



<template>

    <h2 class="cart-icon" @click="showCart()">🛒 {{ productList.length }}</h2>

  <div class="cart-container" :class="{visa: cartVisible}">
    <ul>

      <li v-for="(item, index) in groupedCartItems" :key="index" class="cart-li">
          <span>{{ item.name }} - {{ item.price.current }} kr</span>
            <div class="quantity-buttons-container">

              <div class="quantity-control">
                  <button class="quantity-button" @click="decreaseQuantity(item)">➖</button>
                  <span id="quantity">{{ item.quantity }}</span>
                  <button class="quantity-button" @click="increaseQuantity(item)">➕</button>
              </div>
              <button class="cart-button" @click="removeFromCart(item)">Ta bort</button>

            </div>
      </li>

    </ul>
    <button class="cart-button" @click="makeOrder()">Beställ</button>
    <button class="cart-button" @click="showCart()">Stäng</button>

  </div>
</template>


<style>
.cart-icon:hover{
  cursor: pointer;
}
.cart-icon-container{
  width: 50px;
  height: 50px;
}

.cart-container ul{ 
  padding: 15px;
}
.visa{
  display: none;
}
.cart-container {
  border: 1px solid wheat;
  position: fixed;
  right: 10px;
  top: 45px;
  background-color: #242323;
  color:rgb(196, 190, 190);
  padding: 10px;
  z-index: 1000;
  width: 23rem;
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
.quantity-control{
  width: 77px;
  justify-content: end;
  display: flex;
  margin-right: 5px;
}
.quantity-button{
  background: none;
border: none;
cursor: pointer;
}
#quantity{
  margin: 4px;
}
.quantity-buttons-container{
  display: flex;
  align-items: center;
}
</style>