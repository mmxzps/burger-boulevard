<script>
import * as api from '@/api.js'
import Cart from './Cart.vue'

export default {
  data() {
    return {
      categories: [],
      isOpen: false
    }
  },

  mounted() {
    api.getCategories().then(r => this.categories = r.data)
  },

  methods: {
    closeMenu() {
      this.isOpen = false
    }
  },

  components: {
    Cart
  }
}
</script>

<template>
  <nav :class="{ open: isOpen }">
    <button class="menu-toggle" @click="isOpen = !isOpen">
      <span v-for="_ in 3" class="bar"></span>
    </button>

    <ul>
      <li v-for="category in categories">
        <router-link @click="closeMenu" :to="'/' + category.id">
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
