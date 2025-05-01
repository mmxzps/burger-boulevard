<script>
import * as api from '@/api.js'

export default {
  data() {
    return {
      menu: null,
      menuItems: [],
      loading: true,
      error: null
    };
  },
  mounted() {
    this.fetchMenuDetails();
  },
  methods: {
    fetchMenuDetails() {
      const menuId = this.$route.params.id;
      this.loading = true;

      api.getComponents(2, null, menuId)
        .then(menuResponse => {
          this.menu = Array.isArray(menuResponse.data)
                    ? menuResponse.data[0]
                    : menuResponse.data;

          return api.getComponentPolicies(menuId);
        })
        .then(policiesResponse => {
          console.log('Menu Policies Response:', policiesResponse.data);

          this.menuItems = policiesResponse.data.map(policy => ({
            id: policy.childId,
            name: policy.child?.name || 'Unnamed Item',
            description: policy.child?.description || '',
            imageUrl: policy.child?.imageUrl || null,
            price: policy.child?.price || { current: 0 },
            quantity: policy.default || 1
          }));

          this.loading = false;
        })
        .catch(error => {
          console.error("Fel vid hämtning av menydetaljer:", error);
          this.error = "Kunde inte hämta menydetaljer";
          this.loading = false;
        });
    }
  },
  watch: {
    '$route.params.id': 'fetchMenuDetails'
  }
};
</script>

<template>
  <div>
    <router-link to="/menus" class="back-button">← Tillbaka till menyer</router-link>

    <div v-if="loading" class="loading-state">Laddar menydetaljer...</div>
    <div v-else-if="error" class="error-state">{{ error }}</div>
    <div v-else-if="menu" class="menu-detail">
      <h1 class="menu-title">{{ menu.name }}</h1>
      <p class="menu-description">{{ menu.description }}</p>

      <h2>Innehåller:</h2>
      <ul class="menu-items-list">
        <li v-for="item in menuItems" :key="item.id" class="menu-item-card">
          <h3>{{ item.name }}</h3>
          <img src="../categoryImg/menumat.png" :alt="item.name" class="item-image" />
          <p class="item-description">{{ item.description }}</p>
        </li>
      </ul>

      <div class="menu-price">
        <p>Total pris: {{ menu.price.current }} kr</p>
        <button class="add-button">Lägg till i kundvagn</button>
      </div>
    </div>
    <div v-else class="not-found">Meny hittades inte</div>
  </div>
</template>

<style scoped>
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

.menu-detail {
  max-width: 1200px;
  margin: 0 auto;
  padding: 0 1rem 2rem;
}

.menu-title {
  font-size: 2.5rem;
  text-align: center;
  margin: 2rem 0;
  text-shadow: 11px 11px 25px #f5d451;
}

.menu-description {
  text-align: center;
  font-size: 1.2rem;
  margin-bottom: 2rem;
}

.menu-items-list {
  display: flex;
  flex-wrap: wrap;
  gap: 1.5rem;
  justify-content: center;
  padding: 0;
  list-style: none;
}

.menu-item-card {
  border: 1px solid #ccc;
  border-radius: 8px;
  padding: 1rem;
  width: 250px;
  display: flex;
  flex-direction: column;
  align-items: center;
}

.item-image {
  height: 120px;
  object-fit: cover;
  border-radius: 4px;
  margin: 0.5rem 0;
}

.item-description {
  font-size: 0.9rem;
  text-align: center;
}

.item-price, .item-quantity {
  font-size: 0.95rem;
  margin: 0.3rem 0;
  font-weight: bold;
}

.menu-price {
  margin-top: 2rem;
  text-align: center;
  font-size: 1.5rem;
  font-weight: bold;
}

.add-button {
  border: 1px solid #f5d451;
  background-color: #f5d451;
  padding: 0.7rem 1.5rem;
  border-radius: 5px;
  font-size: 1rem;
  font-weight: bold;
  cursor: pointer;
  margin-top: 1rem;
}

.loading-state, .error-state, .not-found {
  text-align: center;
  margin: 2rem;
  font-size: 1.2rem;
}

.error-state {
  color: #e53935;
}
</style>
