<script>
export default{
  data(){
    return{
      productList: []
    }
  },
  mounted(){
    this.loadCart();
  },
  methods:{
    loadCart(){
      this.productList = JSON.parse(localStorage.getItem('cart')) || [];
    },
    removeFromCart(index){
      this.productList.splice(index, 1);
      localStorage.setItem('cart',JSON.stringify(this.productList));
    }
  }
}
</script>



<template>
  <div class="varukorgtext">
    <h2>Varukorg: {{ productList.length }}st</h2>
  </div>
  <div class="cart-container">
    <ul>
      <li v-for="(item, index) in productList" :key="index" class="cart-li">
        <span>{{ item.name }} - {{ item.price.basePrice }} kr</span>
        <button @click="removeFromCart(index)">Ta bort</button>
      </li>
    </ul>
  </div>
</template>


<style>
.varukorgtext{
  width: 150px;
  height: 50px;
}

.cart-container ul{ 
  padding: 15px;
}
.cart-container {
  margin-bottom: 30px;
  border: 1px solid wheat;
  position: fixed;
  right: 0;
  top: 35px;
  background: white;
  color:black;
  padding: 10px;
  z-index: 1000;
}
.cart-li{
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-top: 5px;
}
</style>