<script>
import GetAllPoducts from './components/GetAllPoducts.vue';
import NavBar from './components/NavBar.vue';

export default {
  components: {
    GetAllPoducts,
    NavBar
  }
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

#app {
  min-height: 100vh;
}

main {
  padding-top: 60px;
}
</style>