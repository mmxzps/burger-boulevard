import { createRouter, createWebHistory } from 'vue-router'
import ProductsView from '@/views/Products.vue'
import CheckoutView from '@/views/Checkout.vue'
import Menus from '@/views/Menus.vue'
import Burgers from '@/views/Burgers.vue'
import Sides from '@/views/Sides.vue'
import Drinks from '@/views/Drinks.vue'

const routes = [
  {
    path: '/checkout',
    name: 'Checkout',
    component: CheckoutView
  },
  {
    path: '/menus',
    name: 'Menus',
    component: Menus
  },
  {
    path: '/burgers',
    name: 'Burgers',
    component: Burgers
  },
  {
    path: '/sides',
    name: 'Sides',
    component: Sides
  },
  {
    path: '/drinks',
    name: 'Drinks',
    component: Drinks
  },
  {
    path: '/:category?',
    name: 'Category',
    component: ProductsView
  }
]


const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router
