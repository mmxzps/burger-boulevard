import { createRouter, createWebHistory } from 'vue-router'
import ProductsView from '@/views/Products.vue'
import CheckoutView from '@/views/Checkout.vue'

const routes = [
  {
    path: '/:category?',
    // name: '',
    component: ProductsView
  },
  {
    path: '/checkout',
    name: 'Checkout',
    component: CheckoutView
  }
]

export default createRouter({
  history: createWebHistory(),
  routes
})
