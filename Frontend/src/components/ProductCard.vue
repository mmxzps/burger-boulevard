<template>
  <div class="card-container">
      <div class="card">
          <div class="card-text">
            <h2>{{ burger.name }}</h2>
            <p>{{ burger.description }}</p>
            <p>{{ burger.price.current +'kr' }}</p>
        </div>
        <button class="card-button" v-on:click="addToCart(burger)">Lägg till</button>
    </div>
  </div>
</template>

<script>
import { useCartStore } from '@/stores/cart';

export default {
  props: {
    burger: Object,
  },
  methods: {
    addToCart(burger) {
      const cartStore = useCartStore();
      cartStore.addToCart(burger);
    }
  }
};
</script>


<style scoped>
.card {
  border: 1px solid #534f4f;
  padding: 0px 3px;
  margin: 10px 0;
  border-radius: 8px;
  border-radius: 3px;
  background-color: #242323;
  height: auto;
  min-height: 100px;
}
.card-text{
  height:100%;
  min-height:98px;
  padding:10px;
  color:rgb(196, 190, 190);
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
  margin: 10px;
  border:1px solid black;
  padding: 5px;
  border-radius: 3px;
  color:white;
  font: 1em sans-serif;
  background-color: #4f4492;
  width: 4.5rem;
}
.card:hover{
  transform: scale(1.01);
  transition: transform 0.9 eas-out;
}
</style>
