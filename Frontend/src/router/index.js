import { createRouter, createWebHistory } from 'vue-router'
import GetAllPoducts from '../components/GetAllPoducts.vue'
import CheckoutView from '../Views/CheckoutView.vue'

const routes = [
  {
    path: '/:category',
    // name: '',
    component: GetAllPoducts
  },
  {
    path:'/checkout',
    name: 'Checkout',
    component: CheckoutView
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router
