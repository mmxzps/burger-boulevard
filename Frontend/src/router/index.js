import { createRouter, createWebHistory } from 'vue-router'
import ProductsView from '@/views/Products.vue'
import CheckoutView from '@/views/Checkout.vue'

const routes = [
  {
    path: '/checkout',
    name: 'Checkout',
    component: CheckoutView
  },
  {
    path: '/:category?',
    component: ProductsView
  }
]

export default createRouter({
  history: createWebHistory(),
  routes
})
