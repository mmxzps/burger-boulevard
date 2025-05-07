<script>
import { useCartStore } from '@/stores/cart'
import { useApiCacheStore } from '@/stores/apiCache'
import * as api from '@/api'
import { evaluateCost, componentToTreeWithDefaults, gatherAllergens } from '@/util'

export default {
  data() {
    return {
      cartStore: useCartStore(),
      components: null,

      baseUrl: api.baseUrl,
      componentTree: null,
      quantity: 1,
      detailsVisible: false
    }
  },

  mounted() {
    this.resetData()
  },

  props: {
    component: Object
  },

  methods: {
    gatherAllergens,

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

    defaultComponentTree() {
      return componentToTreeWithDefaults(this.component)
    },

    resetComponentTree() {
      this.componentTree = this.defaultComponentTree()
    },

    resetData() {
      Object.assign(this.$data, this.$options.data.apply(this))
      this.resetComponentTree()
      useApiCacheStore().components.then(r => this.components = r)
    }
  },

  computed: {
    totalPrice() {
      return evaluateCost(this.components, this.componentTree)
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
      <button class="button" @click.stop="addToCart">{{ component.price.current }} kr</button>
    </div>

    <div v-if="detailsVisible" class="product-detail-overlay" @click.self="resetData">
      <div class="product-detail-container">
        <button class="close-button" @click="resetData">×</button>

        <div class="product-info">
          <h2>{{ component.name }}</h2>
          <p class="description">{{ component.description }}</p>

          <div v-if="component.childPolicies.length > 0" class="popup-ingredients-section">
            <ul class="popup-ingredients-list">
              <li v-for="policy in component.childPolicies" :key="policy.id" class="popup-ingredient-item">
                <div class="ingredient-content">
                  <span>
                    {{ policy.child.name }}
                    ({{ policy.child.price.current }} kr)
                  </span>
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

          <button v-if="isChanged" @click="resetComponentTree">Återställ
            ingredienser</button>

          <ul class="allergens">
            <li v-for="allergen in gatherAllergens(components, componentTree).sort((a, b) => a.id - b.id)"
              :key="allergen.id">
              {{ allergen.name }}
            </li>
          </ul>

          <div class="quantity-control">
            <div class="quantity-buttons">
              <button @click="changeQuantity(-1)" :disabled="quantity <= 1">-</button>
              <span>{{ quantity }} st</span>
              <button @click="changeQuantity(1)">+</button>
            </div>
          </div>
        </div>

        <div class="action-buttons">
          <button class="add-button" @click="addToCart(); resetData()">
            ({{ totalPrice * quantity }} kr)
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
  border: 1px solid #ccc;
  padding: 1rem;
  border-radius: 8px;
  display: flex;
  flex-direction: column;
  align-items: center;
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
  height: 12rem;
  width: auto;
  border-radius: 5px;
}

.product-name {
  font-size: 1.4rem;
  font-weight: bold;
  margin: 0.5rem 0;
  text-align: center;
}

.product-description {
  font-size: 1rem;
  color: #333;
  text-align: center;
  margin-bottom: 0.5rem;
}

.product-price {
  font-size: 1.2rem;
  font-weight: bold;
  margin: 0.5rem 0;
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
  background-color: #fff;
  border-radius: 8px;
  padding: 24px;
  width: 90%;
  max-width: 800px;
  max-height: 90vh;
  overflow-y: auto;
  color: #3c2c18;
  box-shadow: 0 5px 15px rgba(0, 0, 0, 0.5);
}

.close-button {
  position: absolute;
  top: 10px;
  right: 10px;
  font-size: 24px;
  background: none;
  border: none;
  color: #3c2c18;
  cursor: pointer;
}

.product-info h2 {
  font-size: 2rem;
  margin-bottom: 16px;
  text-align: center;
  margin: 2rem 0;
  text-shadow: 11px 11px 25px #f5d451;
}

.description {
  font-size: 1.2rem;
  text-align: center;
  margin-bottom: 2rem;
}

.price {
  font-size: 1.5rem;
  font-weight: bold;
  margin-bottom: 24px;
  text-align: center;
}

.popup-ingredients-section h1 {
  font-size: 1.5rem;
  margin: 2rem 0 1rem;
  text-align: center;
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
  font-size: 1rem;
  background-color: #f9f9f9;
  padding: 1rem;
  border-radius: 8px;
  display: block;
  border-bottom: 1px solid #eee;
}

.ingredient-content {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.ingredient-controls {
  display: flex;
  align-items: center;
  gap: 1rem;
}

.ingredient-quantity {
  min-width: 1.5rem;
  text-align: center;
  font-weight: bold;
}

.ingredient-control-btn {
  width: 2rem;
  height: 2rem;
  border-radius: 50%;
  background-color: #f5d451;
  color: #3c2c18;
  border: 1px solid #f5d451;
  cursor: pointer;
  display: flex;
  justify-content: center;
  align-items: center;
  font-size: 1.2rem;
  padding: 0;
  font-weight: bold;
}

.ingredient-control-btn:disabled {
  opacity: 0.5;
  cursor: not-allowed;
}

.quantity-control {
  display: flex;
  justify-content: center;
  align-items: center;
  margin: 2rem 0;
  gap: 1rem;
}

.quantity-buttons {
  display: flex;
  align-items: center;
  gap: 1rem;
}

.quantity-buttons button {
  width: 2rem;
  height: 2rem;
  border-radius: 50%;
  border: 1px solid #f5d451;
  background-color: #f5d451;
  cursor: pointer;
  font-weight: bold;
  font-size: 1.2rem;
}

.quantity-buttons button:disabled {
  opacity: 0.5;
  cursor: not-allowed;
}

.quantity-buttons span {
  margin: 0 0.5rem;
  font-weight: bold;
  min-width: 1.5rem;
  text-align: center;
}

.action-buttons {
  display: flex;
  justify-content: center;
  margin-top: 2rem;
}

.add-button {
  padding: 0.8rem 1.5rem;
  background-color: #f5d451;
  color: #3c2c18;
  border: none;
  border-radius: 8px;
  font-weight: bold;
  font-size: 1.1rem;
  cursor: pointer;
  min-width: 250px;
  white-space: nowrap;
  display: flex;
  justify-content: center;
  align-items: center;
}

.allergens {
  display: flex;
  flex-wrap: wrap;
  gap: 0.5rem;
  list-style: none;
  padding: 0;
  margin: 1rem 0;
  justify-content: center;
}

.allergens li {
  padding: 0.3rem 0.7rem;
  border-radius: 5px;
  background: #f5d451;
  color: #3c2c18;
  font-size: 0.9rem;
}

button[v-if="isChanged"] {
  display: block;
  margin: 1rem auto;
  padding: 0.5rem 1rem;
  background-color: #f5d451;
  color: #3c2c18;
  border: none;
  border-radius: 5px;
  font-weight: bold;
  cursor: pointer;
}
</style>
