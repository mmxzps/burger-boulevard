<template>
  <div class="card-container">
      <div class="card">
          <div class="card-text">
            <h2>{{ burger.name }}</h2>
            <p>{{ burger.description }}</p>
            <p>{{ burger.price.basePrice +'kr' }}</p>
        </div>
        <button class="card-button" v-on:click="addToCart(burger)">Lägg till</button>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    burger: Object,
  },
  data(){
    return{
     
    }
  },
  methods:{
    async addToCart(burger){
      let cart = JSON.parse(localStorage.getItem('cart')) || [];
      cart.push(burger);
      localStorage.setItem('cart', JSON.stringify(cart))
    }
  },
  mounted(){
    // this.cart = JSON.parse(localStorage.getItem('cart')) || [];
  }
};
</script>

<style scoped>
.card {
  border: 1px solid #ccc;
  padding: 0px 3px;
  margin: 10px 0;
  border-radius: 8px;
  background-color: grey;
  background-image: url('https://images.unsplash.com/photo-1551782450-17144efb9c50?q=80&w=960');
  background-position: center;
  background-repeat: no-repeat;
  background-size: cover; 
  height: auto;
  min-height: 100px;
}
.card-text{
  background-color: rgb(0, 10, 0, 0.8);
  height:100%;
  min-height:98px;
  padding:10px;
  color:white;
  text-shadow: 1px 1px black;

}
.card-text p {
  font-size:15px; 
  font-style: italic; 
  }

  .card-text h2 {
    border-bottom: 1px solid grey;
  }

.card-button{
  z-index: 4;
}
.card:hover{
  transform: scale(1.01);
  transition: transform 0.9 eas-out;
}
</style>
