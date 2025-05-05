<script>
import { useCartStore } from '@/stores/cart'
import * as api from '@/api.js'

export default {
  data() {
    return {
    burger: null,
    ingredients: [],
    loading: true,
    error: null,
    quantity: 1,
    baseUrl: api.baseUrl
    }
  },

  mounted() {
    this.fetchBurgerDetails();
  },

  methods: {
    async fetchBurgerDetails() {
      try {
        const burgerId = this.$route.params.id;
        this.loading = true;

        const response = await api.getComponents(1, 1, burgerId);
        this.burger = response.data[0] || response.data;


        await this.fetchIngredients();

        this.loading = false;
      } catch (error) {
        console.error('Error fetching burger details:', error);
        this.error = 'Kunde inte ladda hamburgardetaljer';
        this.loading = false;
      }
    },

    async fetchIngredients() {
      try {
        const response = await api.getComponentPolicies(this.burger.id);
        this.ingredients = response.data.map(item => ({
          id: item.id,
          name: item.child.name,
          description: item.child.description,
          quantity: item.default,
          min: item.min,
          max: item.max,
          originalQuantity: item.default
        }));
      } catch (error) {
        console.error('Error fetching ingredients:', error);
      }
    },

    changeIngredientQuantity(index, change) {
      const ingredient = this.ingredients[index];
      const newQuantity = ingredient.quantity + change;

      if (newQuantity >= ingredient.min && newQuantity <= ingredient.max) {
        ingredient.quantity = newQuantity;
      }
    },

    increaseQuantity() {
      this.quantity++;
    },

    decreaseQuantity() {
      if (this.quantity > 1) {
        this.quantity--;
      }
    },

    addToCart() {
      const cartStore = useCartStore();

      const burgerToAdd = {
        ...this.burger,
        quantity: this.quantity,
        ingredients: this.ingredients.map(ingredient => ({
          ...ingredient
        }))
      };

      cartStore.cart.push(burgerToAdd);
      cartStore.save();

      this.$router.push('/');
    }
  }
};
</script>

<template>
  <div class="burger-detail">
    <router-link to="/burgers" class="back-button">← Tillbaka till hamburgare</router-link>

    <div v-if="loading" class="loading-state">Laddar hamburgardetaljer...</div>
    <div v-else-if="error" class="error-state">{{ error }}</div>
    <div v-else-if="burger" class="burger-content">
      <div class="burger-header">
  <h1>{{ burger.name }}</h1>
  <img
    :src="`${baseUrl}${burger.imageUrl}`"
    :alt="burger.name"
    class="burger-image"
    @error="$event.target.style.display = 'none'"
  >
  <p class="burger-description">{{ burger.description }}</p>
  <p class="burger-price">{{ burger.price.current }} kr</p>
</div>

      <div class="ingredients-section">
        <h2>Anpassa din hamburgare</h2>

        <ul class="ingredients-list">
          <li v-for="(ingredient, index) in ingredients" :key="ingredient.id" class="ingredient-item">
            <div class="ingredient-info">
              <h3>{{ ingredient.name }}</h3>
              <p>{{ ingredient.description }}</p>
            </div>

            <div class="ingredient-controls">
              <button
                @click="changeIngredientQuantity(index, -1)"
                :disabled="ingredient.quantity <= ingredient.min"
                class="quantity-button"
              >
                -
              </button>
              <span class="ingredient-quantity">{{ ingredient.quantity }}</span>
              <button
                @click="changeIngredientQuantity(index, 1)"
                :disabled="ingredient.quantity >= ingredient.max"
                class="quantity-button"
              >
                +
              </button>
            </div>
          </li>
        </ul>
      </div>

      <div class="order-controls">
        <div class="quantity-control">
          <span>Antal:</span>
          <button @click="decreaseQuantity" class="quantity-button">-</button>
          <span class="quantity">{{ quantity }}</span>
          <button @click="increaseQuantity" class="quantity-button">+</button>
        </div>

        <button @click="addToCart" class="add-to-cart-button">
          Lägg till i kundvagn
        </button>
      </div>
    </div>
    <div v-else class="not-found">Hamburgaren hittades inte</div>
  </div>
</template>

<style scoped>
.burger-detail {
  max-width: 800px;
  margin: 0 auto;
  padding: 2rem;
}

.back-button {
  display: inline-block;
  margin-bottom: 2rem;
  padding: 0.5rem 1rem;
  background-color: #f5d451;
  color: #3c2c18;
  text-decoration: none;
  font-weight: bold;
  border-radius: 8px;
  box-shadow: 0 2px 6px rgba(0, 0, 0, 0.15);
}

.burger-header {
  display: flex;
  flex-direction: column;
  align-items: center;
  margin-bottom: 2rem;
}

.burger-image {
  height: 200px;
  width: auto;
  margin: 1rem 0;
}

.burger-description {
  text-align: center;
  margin-bottom: 0.5rem;
}

.burger-price {
  font-size: 1.5rem;
  font-weight: bold;
}

.ingredients-section {
  margin: 2rem 0;
}

.ingredients-list {
  list-style: none;
  padding: 0;
}

.ingredient-item {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 1rem;
  border-bottom: 1px solid #eee;
}

.ingredient-info {
  flex: 1;
}

.ingredient-info h3 {
  margin: 0 0 0.5rem 0;
}

.ingredient-info p {
  margin: 0;
  color: #666;
  font-size: 0.9rem;
}

.ingredient-controls {
  display: flex;
  align-items: center;
}

.quantity-button {
  width: 2rem;
  height: 2rem;
  border-radius: 50%;
  border: 1px solid #f5d451;
  background-color: #f5d451;
  cursor: pointer;
  font-weight: bold;
  font-size: 1.2rem;
  display: flex;
  align-items: center;
  justify-content: center;
}

.quantity-button:disabled {
  opacity: 0.5;
  cursor: not-allowed;
}

.ingredient-quantity {
  margin: 0 1rem;
  font-weight: bold;
  min-width: 1.5rem;
  text-align: center;
}

.order-controls {
  display: flex;
  flex-direction: column;
  align-items: center;
  margin-top: 2rem;
}

.quantity-control {
  display: flex;
  align-items: center;
  margin-bottom: 1rem;
}

.quantity {
  margin: 0 1rem;
  font-weight: bold;
}

.add-to-cart-button {
  padding: 0.8rem 1.5rem;
  background-color: #f5d451;
  color: #3c2c18;
  border: none;
  border-radius: 8px;
  font-weight: bold;
  font-size: 1.1rem;
  cursor: pointer;
}

.loading-state, .error-state, .not-found {
  text-align: center;
  padding: 3rem;
  font-size: 1.2rem;
}

.error-state {
  color: #e53935;
}
</style>
