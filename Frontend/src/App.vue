<script>
import GetAllPoducts from './components/GetAllPoducts.vue';
import NavBar from './components/NavBar.vue';
import Cart from './components/Cart.vue';

export default {
  components: {
    GetAllPoducts,
    NavBar
  }
}
export default{
  components:{GetAllPoducts, Cart}
}
</script>

<template>
  <div id="app">
    <NavBar />
    <main>
      <router-view />
    </main>
  </div>
</template>

<style>
body {
  margin: 0;
  padding: 0;
  font-family: Arial, sans-serif;
}
    <GetAllPoducts/>
    <Cart/>

</template>

#app {
  min-height: 100vh;
}

main {
  padding-top: 60px;
}
</style>