<script>
import { useCartStore } from '@/stores/cart'
import * as api from '@/api.js'

export default {
  data() {
    return {
      baseUrl: api.baseUrl,
      ingredients: [],
      detailsVisible: false,
      quantity: 1
    }
  },

  props: {
    component: Object
  },

  mounted() {
    this.fetchIngredients()
  },

  methods: {
    addToCart(component) {
      const cartStore = useCartStore()
      cartStore.cart.push(component)
      cartStore.save()
    },

    async fetchIngredients() {
      this.ingredients = (await api.getComponentPolicies(this.component.id)).data.map(item => ({
        id: item.id,
        name: item.child.name,
        description: item.child.description,
        quantity: item.default,
        min: item.min,
        max: item.max,
        originalQuantity: item.default
      }))
    },

    changeIngredientQuantity(index, change) {
      const ingredient = this.extractedIngredients[index]
      const newQuantity = ingredient.quantity + change

      if (newQuantity >= ingredient.min && newQuantity <= ingredient.max)
        ingredient.quantity = newQuantity
    },

    showDetails(event) {
      if (!event.target.closest('.card-button'))
        this.detailsVisible = true
    },

    hideDetails() {
      this.detailsVisible = false
      this.quantity = 1

      if (this.extractedIngredients.length > 0)
        this.extractedIngredients.forEach(ingredient => {
          ingredient.quantity = ingredient.originalQuantity
        });
    },

    increaseQuantity() {
      this.quantity++
    },

    decreaseQuantity() {
      if (this.quantity > 1)
        this.quantity--
    },

    addCustomizedToCart() {
      const cartStore = useCartStore()

      const modifiedIngredients = this.extractedIngredients
        .filter(ing => ing.quantity !== ing.originalQuantity)

      const customizedBurger = {
        ...this.component,
        quantity: this.quantity,
        ingredients: this.extractedIngredients.filter(ing => ing.quantity > 0),
        modifiedIngredients: modifiedIngredients,
        isCustomized: modifiedIngredients.length > 0,
        totalPrice: this.component.price.current * this.quantity
      }

      console.log('Adding customized burger to cart with modified ingredients:', modifiedIngredients)
      cartStore.addToCart(customizedBurger)
      this.hideDetails()
    }
  }
}
</script>

<template>
  <div class="product">
    <img :src="baseUrl + component.imageUrl" alt="" class="product-image" />
    <span class="product-name">{{ component.name }}</span>
    <p class="product-description">{{ component.description }}</p>
    <span class="product-price">{{ component.price.current }}kr</span>
    <button class="button" @click="addToCart(component)">Lägg till</button>
  </div>

  <div class="card-container">
    <div class="card" @click="showDetails">
      <div class="card-text">
        <h2>{{ component.name }}</h2>
        <p>{{ component.description }}</p>
        <p>{{ component.price.current + 'kr' }}</p>
      </div>

      <button class="card-button" @click.stop="addToCart(component)">Lägg till</button>
    </div>

    <div v-if="detailsVisible" class="product-detail-overlay" @click.self="hideDetails">
      <div class="product-detail-container">
        <button class="close-button" @click="hideDetails">×</button>

        <div class="product-info">
          <h2>{{ component.name }}</h2>
          <p class="description">{{ component.description }}</p>
          <p class="price">{{ component.price.current }} kr</p>

          <div v-if="extractedIngredients.length > 0" class="popup-ingredients-section">
            <h3>Ingredienser</h3>
            <ul class="popup-ingredients-list">
              <li v-for="(ingredient, index) in extractedIngredients" :key="ingredient.id"
                class="popup-ingredient-item">
                <div class="ingredient-content">
                  <span>{{ ingredient.name }}</span>
                  <div class="ingredient-controls">
                    <button class="ingredient-control-btn remove-btn" @click="changeIngredientQuantity(index, -1)"
                      :disabled="ingredient.quantity <= 0">
                      -
                    </button>
                    <span class="ingredient-quantity">{{ ingredient.quantity }}</span>
                    <button class="ingredient-control-btn add-btn" @click="changeIngredientQuantity(index, 1)">
                      +
                    </button>
                  </div>
                </div>
              </li>
            </ul>
          </div>

          <div class="quantity-control">
            <span>Antal:</span>
            <div class="quantity-buttons">
              <button @click="decreaseQuantity" :disabled="quantity <= 1">-</button>
              <span>{{ quantity }}</span>
              <button @click="increaseQuantity">+</button>
            </div>
          </div>
        </div>

        <div class="action-buttons">
          <button class="add-button" @click="addCustomizedToCart">
            Lägg till ({{ component.price.current * quantity }} kr)
          </button>
        </div>
      </div>
    </div>
  </div>
</template>


<style scoped>
.product {
  width: 20rem;
  cursor: pointer;
}

.product-image {
  display: block;
  margin: 0 auto;
  height: 14rem;
  width: auto;
  border-radius: 5px;
}

.product-name {
  font-size: 1.6rem;
  font-weight: bold;
}

.product-description {
  font-size: 1.1rem;
  color: #333;
}

.product-price {
  font-size: 1.5rem;
}

.product-detail-overlay {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: rgba(0, 0, 0, 0.8);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 1000;
}

.product-detail-container {
  position: relative;
  background-color: #242323;
  border-radius: 8px;
  padding: 24px;
  width: 90%;
  max-width: 500px;
  max-height: 90vh;
  overflow-y: auto;
  color: rgb(196, 190, 190);
  box-shadow: 0 5px 15px rgba(0, 0, 0, 0.5);
}

.close-button {
  position: absolute;
  top: 10px;
  right: 10px;
  font-size: 24px;
  background: none;
  border: none;
  color: rgb(196, 190, 190);
  cursor: pointer;
}

.product-info h2 {
  font-size: 24px;
  margin-bottom: 16px;
  border-bottom: 1px solid grey;
  padding-bottom: 8px;
}

.description {
  font-size: 16px;
  font-style: italic;
  margin-bottom: 16px;
}

.price {
  font-size: 18px;
  font-weight: bold;
  margin-bottom: 24px;
}

.popup-ingredients-section h3 {
  font-size: 18px;
  margin-bottom: 16px;
  border-bottom: 1px solid grey;
  padding-bottom: 8px;
}

.popup-ingredients-list {
  list-style-type: none;
  padding: 0;
  margin: 0 0 24px 0;
  display: flex;
  flex-direction: column;
  gap: 8px;
}

.popup-ingredient-item {
  font-size: 14px;
  background-color: #3a3a3a;
  padding: 8px 12px;
  border-radius: 8px;
  display: block;
}

.ingredient-content {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.ingredient-controls {
  display: flex;
  align-items: center;
  gap: 8px;
}

.ingredient-quantity {
  min-width: 20px;
  text-align: center;
}

.ingredient-control-btn {
  width: 24px;
  height: 24px;
  border-radius: 50%;
  background-color: #4f4492;
  color: white;
  border: none;
  cursor: pointer;
  display: flex;
  justify-content: center;
  align-items: center;
  font-size: 14px;
  padding: 0;
}

.ingredient-control-btn:disabled {
  background-color: #333;
  cursor: not-allowed;
}

.quantity-control {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 24px;
}

.quantity-buttons {
  display: flex;
  align-items: center;
  gap: 16px;
}

.quantity-buttons button {
  background-color: #4f4492;
  color: white;
  border: none;
  width: 30px;
  height: 30px;
  border-radius: 50%;
  cursor: pointer;
  font-size: 16px;
}

.action-buttons {
  display: flex;
  justify-content: center;
}

.add-button {
  background-color: #4f4492;
  color: white;
  border: none;
  padding: 12px 24px;
  border-radius: 4px;
  font-size: 16px;
  cursor: pointer;
  width: 100%;
  transition: background-color 0.3s;
}

.add-button:hover {
  background-color: #3b3370;
}
</style>
