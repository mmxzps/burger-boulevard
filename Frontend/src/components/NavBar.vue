<template>
  <nav class="navbar">
    <div class="navbar-content">
      <div class="logo">
        <router-link to="/">Burger Boulevard</router-link>
      </div>
      <!-- Hamburger button for mobile -->
      <button class="menu-toggle" @click="toggleMenu">
        <span class="bar"></span>
        <span class="bar"></span>
        <span class="bar"></span>
      </button>
      <!-- Navigation Links -->
      <ul :class="{'nav-links': true, open: isOpen}">
        <li><router-link @click="closeMenu" to="/hamburgare">Hamburgare</router-link></li>
        <li><router-link @click="closeMenu" to="/kycklingfisk">Kyckling & Fisk</router-link></li>
        <li><router-link @click="closeMenu" to="/snacks">Snacks</router-link></li>
        <li><router-link @click="closeMenu" to="/sidesdip">Sides & Dip</router-link></li>
        <li><router-link @click="closeMenu" to="/sallad">Sallad</router-link></li>
        <li><router-link @click="closeMenu" to="/kalldryck">Kall Dryck</router-link></li>
        <li><router-link @click="closeMenu" to="/varmdryck">Varm Dryck</router-link></li>
        <li><router-link @click="closeMenu" to="/dessert">Dessert</router-link></li>
      </ul>
    </div>
  </nav>
</template>

<script>
export default {
  name: "NavBar",
  data() {
    return {
      isOpen: false,
    };
  },
  methods: {
    toggleMenu() {
      this.isOpen = !this.isOpen;
    },
    closeMenu() {
      // Close the menu. Triggered by clicking a link
      this.isOpen = false;
    },
  },
};
</script>

<style scoped>
/* The navbar fills the full width and is fixed at the top */
.navbar {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  background-color: #000000;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  z-index: 1000;
  padding: 0; /* Remove any external horizontal padding so it fills edge-to-edge */
}

/* Internal container for spacing */
.navbar-content {
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 0 1rem; /* internal padding for content */
  height: 60px;
}

/* Logo styling */
.logo a {
  font-size: 1.5rem;
  font-weight: bold;
  color: #ffffff;
  text-decoration: none;
}

/* Hamburger menu button */
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
  color: #ffffff;
  transition: color 0.3s ease;
}

.nav-links li a:hover {
  color: #007BFF;
}

/* Responsive adjustments for mobile devices */
@media (max-width: 768px) {
  .menu-toggle {
    display: flex;
  }
  /* When on mobile, make the nav-links container a dropdown */
  .nav-links {
    flex-direction: column;
    position: absolute;
    top: 60px; /* directly below the NavBar */
    right: 0;
    width: 100%;
    background: #050000;
    text-align: center;
    overflow: hidden;
    max-height: 0;
    transition: max-height 0.3s ease;
  }
  .nav-links.open {
    max-height: 500px; /* Increase if you add more links */
  }
  .nav-links li {
    margin: 1rem 0;
  }
}
</style>
