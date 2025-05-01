<script>
import { useCartStore } from '@/stores/cart';

export default {
  data(){
    return{
      productList:[],
      cartStore: useCartStore(),
      showOrderConfirm: false,
      orderId: null,
    };
  },

  mounted() {
    const cartStore = useCartStore();
    this.loadCart(cartStore);
  },

  methods:{
    loadCart(store){
      this.productList = store.cart;
    },
    increaseQuantity(item){
      this.cartStore.cart.push(item)
      this.cartStore.save()
    },
    decreaseQuantity(item){
      this.cartStore.cart.splice(this.cartStore.cart.findIndex(i => i.id == item.id), 1)
      this.cartStore.save()

      
    },

    placeOrder() {
      const cartStore = useCartStore();
      const cartProducts = this.groupedProducts
      const components = [];

      
      cartProducts.forEach(product => {
        for (let i = 0; i < product.quantity; i++) {
          components.push({
            componentId: product.id,
            children: []
          });
        }
      });
      const payload = {
          components: components,
          takeAway: cartStore.takeAway
        };

      fetch('https://localhost:7115/api/Orders', {
            method: 'POST',
            headers: {
              'Content-Type': 'application/json'
            },
            body: JSON.stringify(payload)

          }).then( async response => 
          {
            if (!response.ok) {
              return response.json().then(err => {
                throw new Error("Posting order failed!");
              });
            }
            const respdata = await response.json();
            this.orderId = respdata.id;
            console.log('order data:' + respdata)
            return respdata;

          }).then(data => 
          {
            console.log("Order sent:", data);
            cartStore.clearCart();
            this.loadCart(this.cartStore); 
            this.showOrderConfirm = true;
          }).catch(error => 
          {
            console.error("Something went wrong:", error);
          });
    }
  },

  computed:{
    totalPrice(){
      return this.productList.reduce((sum, item)=>{
        return sum + item.price.current;
      }, 0);
    },
    groupedProducts(){
      const itemGroup = {};

      this.productList.forEach(item =>{
        const key = item.id;

        if(!itemGroup[key]){
          itemGroup[key] = {...item, quantity: 1};
        }else{
          itemGroup[key].quantity +=1;
        }
      });

      return Object.values(itemGroup);
    },
    takeAway:{
      get(){
        return this.cartStore.takeAway;
      },
      set(value){
        return this.cartStore.setTakeAway(value);
      }
    }
  }
}
</script>

<template>
   <router-link to="/" class="back-button">← Tillbaka</router-link>
  <div v-if="showOrderConfirm" class="confirm-container">
    <h2>Tack för din beställning!</h2>
    <p>Din order har skickats till köket.</p>
    <h3 v-if="orderId">Ordernummer: {{ orderId }}</h3>
</div>

  <div v-else class="order-container">

    <h1>Summering</h1>
    <div v-if="groupedProducts.length <= 0">
      <p>Din kundvagn är tom!</p>
    </div>
    <div v-else class="order-info-container">
      <ul>
        <li v-for="(item, index) in groupedProducts" :key="index">
          <div class="order-name-price">
            <span>{{ item.name }} </span>
            <div class="quantity-holder">
              
              <button class="quantity-button" @click="decreaseQuantity(item)">➖</button>
              <span id="quantity">{{ item.quantity }}</span>
              <button class="quantity-button" @click="increaseQuantity(item)">➕</button>
              <span class="order-price">{{ item.price.current }} kr</span>
            </div>
          </div>
        </li>
      </ul>
      <hr class="order-divider"/>
      <div>
        <h3>Totalt: {{ totalPrice }} kr</h3>
      </div>
      <div>
        <button class="order-button" @click="placeOrder">Beställ</button>
        <label> Ta med:</label>
        <input type="checkbox" id="toggle" class="checkbox" v-model="takeAway"/>
        <label for="toggle" class="switch"></label>
      </div>
    </div>

  </div>
</template>

<style>
#quantity{
  padding: 5px;
}
.confirm-container{
  display: flex;
  flex-direction: column;
  width: 100%;
  margin: 5rem auto;
  text-align: center;
  text-shadow: -1px 3px 50px #4f4492;;
}
.confirm-container h3{
  margin-top: 1rem;
  font-size: x-large;
  color: darkgoldenrod;
}
.order-container{
  display: flex;
  flex-direction: column;
  align-items: center;
  margin-top: 4rem;
}
.order-info-container{
  border: 1px solid #282828;
  display: flex;
  flex-direction: column;
  align-items: stretch;
  padding: 10px;
  min-width: 22rem;
}

.order-name-price{
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
.order-button{
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
.order-price{
  display: inline-block;
  width: 3.5rem;
  text-align: end;
}
.order-divider{
  margin: 1rem 0;
}
/* for toggle switch */
.switch {    
  position : relative ;   
  display : inline-block;   
  width : 40px;   
  height : 25px;   
  background-color: #da4f4f;   
  border-radius: 2px; 
  margin-left: 10px;
  margin-bottom: -7px;
  }
  .switch::after {  
    content: '';  
    position: absolute;  
    width: 18px;  
    height: 23px;  
    border-radius: 10%;  
    background-color: #0d0c0c; 
    top: 1px;   
    left: 1px;  
    transition: all 0.3s;
    }
  .checkbox:checked + .switch::after 
    {  
      left : 20px; 
    }
    .checkbox:checked + .switch 
    {  
      background-color: #22c56c;
    }
    .checkbox 
    {    
      display : none;
    }
</style>