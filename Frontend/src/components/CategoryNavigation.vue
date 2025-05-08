<script>
import { useApiCacheStore } from '@/stores/apiCache'

export default {
  data: () => ({
    categories: [],
    isOpen: false
  }),

  mounted() {
    useApiCacheStore().categories.then(r => this.categories = r)
  }
}
</script>

<template>
  <nav :class="{ open: isOpen }">
    <button class="menu-toggle" @click="isOpen = !isOpen">
      <span v-for="i in 3" :key="i" class="bar"></span>
    </button>

    <ul>
      <li><router-link to="/">Hem</router-link></li>
      <li v-for="category in categories" :key="category.id">
        <router-link @click="isOpen = false" :to="'/' + category.id">
          {{ category.name }}
        </router-link>
      </li>
    </ul>
  </nav>
</template>

<style scoped>
nav {
  display: flex;
  flex-direction: column;
  width: min-content;
  height: 100%;
  padding: 0 3rem;
  align-items: center;
  justify-content: space-around;
  background-color: #fffdf0;
}

.menu-toggle {
  display: none;
}

nav>ul {
  display: flex;
  flex-direction: column;
  list-style: none;
  margin: 0;
  padding: 0;
  text-align: center;
}

nav>ul li a {
  display: block;
  padding: 1rem 1rem;
  font-size: 1.6rem;
  font-weight: bold;
  color: #3c2c18;
  text-decoration: 3px underline transparent;
  transition: text-decoration 0.1s;
}

nav>ul li a.router-link-active,
nav>ul li a:hover {
  text-decoration-color: #f5d451;
}

@media (max-width: 768px) {
  nav {
    position: fixed;
    width: 100%;
  }

  nav:not(.open) {
    right: 100%;
  }

  nav.open .menu-toggle {
    left: 0;
  }

  .menu-toggle {
    position: absolute;
    display: flex;
    flex-direction: column;
    width: 3rem;
    height: 3rem;
    top: 0;
    left: 100%;
    background: none;
    border: none;
    border-bottom-right-radius: 40%;
    cursor: pointer;
    font-size: 2rem;
  }

  .menu-toggle .bar {
    height: 0.3rem;
    width: 2rem;
    background-color: #333;
    margin: 4px 0;
    transition: 0.4s;
  }
}
</style>
