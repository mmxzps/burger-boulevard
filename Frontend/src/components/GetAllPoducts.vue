<script>
import ProductCard from './ProductCard.vue';
export default {
  data() {
    return {
      burgers: [], // lagrar datan från api i en array
    };
  },
  
  methods: {
    async getApi() {
      try {
        const res = await fetch('https://localhost:7115/api/Components');
        const data = await res.json();
        
        this.burgers = data; // spara svaret i burgers[]

      } catch (error) {
        console.error('Error:', error);
      }
    },
  },

  mounted() {
    this.getApi();
  },

  components:{
    ProductCard
  }
};
</script>

<template>
  <div>
    <h1>Lista över burgare</h1>
    <ul>
      <li v-for="burger in burgers" :key="burger.id">
        <ProductCard :burger="burger"/>
        <br/>
      </li>
    </ul>
  </div>
</template>

<style>
h1 {
  color: white;
  font-weight: bold;
  text-align: center;
}
li{
  list-style:none;  
}
</style>
