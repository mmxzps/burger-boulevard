<script>
export default {
  data() {
    return {
      menus: []
    };
  },
  mounted() {
    fetch("https://localhost:7115/api/Components?level=1")
      .then(response => response.json())
      .then(data => {
        this.menus = data.filter(item =>
          item.categories.some(category => category.name === "Sides & Dip")
        );
      })
      .catch(error => {
        console.error("Fel vid hämtning:", error);
      });
  }
};
</script>

<template>
  <div>
    <router-link to="/" class="back-button">← Tillbaka</router-link>
    <h1 class="meny-h1">Tillbehör</h1>
    <ul class="cards-container">
      <li class="menu-card" v-for="menu in menus" :key="menu.id">
        <h2>{{ menu.name }}</h2>
        <img src="../categoryImg/sides.png" alt="Bild saknas" class="burgerPic" />
        <p>Kategori: {{ menu.categories[0]?.name || 'Okänd' }}</p>
        <p>Pris: {{ menu.price.current }} kr</p>
        <button class="add-button">Lägg till</button>
      </li>
    </ul>
  </div>
</template>

