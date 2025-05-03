<script>
import * as api from '@/api.js'

export default {
  data() {
    return {
      menus: [],
      loading: true,
      error: null
    };
  },
  mounted() {
    this.fetchMenus();
  },
  methods: {
    fetchMenus() {
      this.loading = true;
      api.getBurgerMenus()
        .then(response => {
          this.menus = response.data;
          this.loading = false;
        })
        .catch(error => {
          console.error("Fel vid hämtning:", error);
          this.error = "Kunde inte hämta menyer";
          this.loading = false;
        });
    }
  }
};
</script>

<template>
  <div>
    <router-link to="/" class="back-button">← Tillbaka</router-link>
    <h1 class="meny-h1">Menyer</h1>

    <div v-if="loading" class="loading-state">Laddar menyer...</div>
    <div v-else-if="error" class="error-state">{{ error }}</div>
    <ul v-else class="cards-container">
      <li class="menu-card" v-for="menu in menus" :key="menu.id">
        <router-link :to="`/menu/${menu.id}`" class="menu-link">
          <h2>{{ menu.name }}</h2>
          <img src="../categoryImg/menumat.png" alt="Bild saknas" class="burgerPic" />
          <p>Kategori: {{ menu.categories[0]?.name || 'Okänd' }}</p>
          <p>Pris: {{ menu.price.current }} kr</p>
          <button class="view-button">Visa detaljer</button>
        </router-link>
      </li>
    </ul>
  </div>
</template>

<style>
.back-button {
  display: inline-block;
  margin: 1.5rem;
  padding: 0.5rem 1rem;
  background-color: #f5d451;
  color: #3c2c18;
  text-decoration: none;
  font-weight: bold;
  border-radius: 8px;
  box-shadow: 0 2px 6px rgba(0, 0, 0, 0.15);
}

.back-button:hover {
  background-color: #ffcd00;
}

.meny-h1{
  text-align: center;
  margin: 2rem;
  text-shadow:11px 11px 25px #f5d451;
}

.burgerPic{
  height: 12rem;
  width: auto;
}

.add-button, .view-button {
  border: 1px solid #f5d451;
  background-color: #f5d451;
  width: 8rem;
  height: 2rem;
  border-radius: 5px;
  cursor: pointer;
  font-weight: bold;
  margin-top: 0.5rem;
}

.cards-container {
  display: flex;
  flex-wrap: wrap;
  gap: 1rem;
  margin-top: 2rem;
  justify-content: center;
  margin-bottom: 4rem;
}

.menu-card {
  border: 1px solid #ccc;
  padding: 1rem;
  min-width: fit-content;
  width: 18rem;
  border-radius: 8px;
  align-items: center;
  display: flex;
  flex-direction: column;
}

.menu-link {
  display: flex;
  flex-direction: column;
  align-items: center;
  text-decoration: none;
  color: inherit;
  width: 100%;
}

.loading-state, .error-state {
  text-align: center;
  margin: 2rem;
  font-size: 1.2rem;
}

.error-state {
  color: #e53935;
}
</style>
