<script>
import { useCartStore } from '@/stores/cart'
import { useApiCacheStore } from '@/stores/apiCache'
import * as api from '@/api'
import { evaluateCost, componentToTreeWithDefaults, gatherAllergens } from '@/util'

export default {
  data() {
    return {
      cartStore: useCartStore(),
      baseUrl: api.baseUrl,
      componentTree: componentToTreeWithDefaults(this.component),
      quantity: 1,
      detailsVisible: false
    }
  },

  props: {
    component: Object
  },

  methods: {
    countChildrenInTree(id) {
      return this.componentTree.children.filter(o => o.componentId == id).length
    },

    addToCart() {
      this.cartStore.cart.push(...Array(this.quantity).fill(this.componentTree))
      this.cartStore.save()
    },

    changeQuantity(diff) {
      const newQuantity = this.quantity + diff
      if (newQuantity >= 0)
        this.quantity = newQuantity
    },

    changeIngredientQuantity(policy, diff) {
      const newQuantity = this.countChildrenInTree(policy.child.id) + diff

      if (newQuantity < policy.min || newQuantity > policy.max)
        return

      this.componentTree.children = this.componentTree.children.filter(o => o.componentId != policy.child.id)
      this.componentTree.children.push(...Array(newQuantity).fill(componentToTreeWithDefaults(policy.child)))
    },

    resetData() {
      Object.assign(this.$data, this.$options.data.apply(this))
    }
  }
}
</script>

<template>
  <div class="card-container">
    <div class="product" @click="detailsVisible = true">
      <img :src="component.imageUrl ? baseUrl + component.imageUrl : ''" alt="" class="product-image" />
      <span class="product-name">{{ component.name }}</span>
      <p class="product-description">{{ component.description }}</p>
      <span class="product-price">{{ component.price.current }}kr</span>
      <button class="button" @click.stop="addToCart">Lägg till</button>
    </div>

    <div v-if="detailsVisible" class="product-detail-overlay" @click.self="resetData">
      <div class="product-detail-container">
        <button class="close-button" @click="resetData">×</button>

        <div class="product-info">
          <h2>{{ component.name }}</h2>
          <p class="description">{{ component.description }}</p>
          <p class="price">{{ component.price.current }} kr</p>

          <div v-if="component.childPolicies.length > 0" class="popup-ingredients-section">
            <h1>Ingredienser</h1>
            <ul class="popup-ingredients-list">
              <li v-for="policy in component.childPolicies" :key="policy.id" class="popup-ingredient-item">
                <div class="ingredient-content">
                  <span>{{ policy.child.name }}</span>
                  <div class="ingredient-controls">
                    <button class="ingredient-control-btn remove-btn" @click="changeIngredientQuantity(policy, -1)"
                      :disabled="countChildrenInTree(policy.child.id) <= policy.min">
                      -
                    </button>
                    <span class="ingredient-quantity">{{ countChildrenInTree(policy.child.id) }}</span>
                    <button class="ingredient-control-btn add-btn" @click="changeIngredientQuantity(policy, 1)"
                      :disabled="countChildrenInTree(policy.child.id) >= policy.max">
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
              <button @click="changeQuantity(-1)" :disabled="quantity <= 1">-</button>
              <span>{{ quantity }}</span>
              <button @click="changeQuantity(1)">+</button>
            </div>
          </div>
        </div>

        <div class="action-buttons">
          <button class="add-button" @click="addToCart(); resetData()">
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
  animation: popup .4s ease;
}

@keyframes popup {
  0% {
    opacity: 0;
    scale: 0.9;
  }

  20% {
    opacity: 1;
    scale: 1.05;
  }

  100% {
    scale: 1;
  }
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

.popup-ingredients-section h1 {
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
