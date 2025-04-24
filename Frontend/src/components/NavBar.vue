<script>
import Cart from './Cart.vue';

export default {
  name: "NavBar",

  data() {
    return {
      isOpen: false,
    }
  },

  methods: {
    toggleMenu() {
      this.isOpen = !this.isOpen
    },

    closeMenu() {
      // Close the menu. Triggered by clicking a link
      this.isOpen = false
    }
  },

  components: {
    Cart
  }
}
</script>

<template>
  <nav class="navbar">
    <div class="navbar-content">
      <button class="menu-toggle" @click="toggleMenu">
        <span class="bar"></span>
        <span class="bar"></span>
        <span class="bar"></span>
      </button>
      <ul :class="{ 'nav-links': true, open: isOpen }">
        <li><router-link @click="closeMenu" to="/hamburgare">Hamburgare</router-link></li>
        <li><router-link @click="closeMenu" to="/kycklingfisk">Kyckling & Fisk</router-link></li>
        <li><router-link @click="closeMenu" to="/snacks">Snacks</router-link></li>
        <li><router-link @click="closeMenu" to="/sidesdip">Sides & Dip</router-link></li>
        <li><router-link @click="closeMenu" to="/sallad">Sallad</router-link></li>
        <li><router-link @click="closeMenu" to="/kalldryck">Kall Dryck</router-link></li>
        <li><router-link @click="closeMenu" to="/varmdryck">Varm Dryck</router-link></li>
        <li><router-link @click="closeMenu" to="/dessert">Dessert</router-link></li>
      </ul>
      <Cart />
    </div>
  </nav>
</template>

<style scoped>
.navbar {
  position: fixed;
  width: 100%;
  background-color: #f5d451;
}

.navbar-content {
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 0 1rem;
  height: 60px;
}

.menu-toggle {
  display: none;
  flex-direction: column;
  background: none;
  border: none;
  cursor: pointer;
}

.menu-toggle .bar {
  height: 3px;
  width: 25px;
  background-color: #ffffff;
  margin: 4px 0;
  transition: 0.4s;
}

/* Desktop navigation links */
.nav-links {
  list-style: none;
  display: flex;
  margin: 0;
  padding: 0;
}

.nav-links li {
  margin-left: 1rem;
}

.nav-links li a {
  text-decoration: none;
  font-weight: bold;
  color: #3c2c18;
  transition: color 0.3s ease;
}

.nav-links li a:hover {
  color: #fff;
}

@media (max-width: 768px) {
  .menu-toggle {
    display: flex;
  }

  /* When on mobile, make the nav-links container a dropdown */
  .nav-links {
    flex-direction: column;
    position: absolute;
    top: 60px;
    right: 0;
    width: 100%;
    background: #050000;
    text-align: center;
    overflow: hidden;
    max-height: 0;
    transition: max-height 0.3s ease;
  }

  .nav-links.open {
    max-height: 500px;
  }

  .nav-links li {
    margin: 1rem 0;
  }
}
</style>
