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
    this.fetchSides();
  },
  methods: {
    fetchSides() {
      this.loading = true;
      Promise.all([
        api.getComponents(1, 4), // level 1, categoryId 4 (sides)
        api.getComponents(1, 2)  // Hot wings
      ])
        .then(([category4Response, category2Response]) => {
          this.menus = [...category4Response.data, ...category2Response.data];
          this.loading = false;
        })
        .catch(error => {
          console.error("Fel vid hämtning:", error);
          this.error = "Kunde inte hämta tillbehör";
          this.loading = false;
        });
    }
  }
};
</script>

<template>
  <div>
    <router-link to="/" class="back-button">← Tillbaka</router-link>
    <h1 class="meny-h1">Tillbehör</h1>

    <div v-if="loading" class="loading-state">Laddar tillbehör...</div>
    <div v-else-if="error" class="error-state">{{ error }}</div>
    <ul v-else class="cards-container">
      <li class="menu-card" v-for="menu in menus" :key="menu.id">
        <router-link :to="`/side/${menu.id}`" class="menu-link">
          <h2>{{ menu.name }}</h2>
          <img src="../categoryImg/sides.png" alt="Bild saknas" class="burgerPic" />
          <p>Kategori: {{ menu.categories[0]?.name || 'Okänd' }}</p>
          <p>Pris: {{ menu.price.current }} kr</p>
          <button class="view-button">Visa detaljer</button>
        </router-link>
      </li>
    </ul>
  </div>
</template>
