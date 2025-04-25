import { createRouter, createWebHistory } from 'vue-router'
import GetAllProducts from '../components/GetAllProducts.vue'
import CheckoutView from '../Views/CheckoutView.vue'

const routes = [
  {
    path: '/:category',
    // name: '',
    component: GetAllProducts
  },
  {
    path: '/checkout',
    name: 'Checkout',
    component: CheckoutView
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router
