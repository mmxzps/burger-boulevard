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
    component: () => import('../views/Menus.vue')
  },
  {
    path: '/menu/:id',
    name: 'MenuDetail',
    component: () => import('../views/MenuDetail.vue')
  },
  {
    path: '/burgers',
    name: 'Burgers',
    component: Burgers
  },
  {
    path: '/burger/:id',
    name: 'BurgerDetail',
    component: () => import('../views/BurgerDetail.vue')
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
    component: ProductsView
  }
]

export default createRouter({
  history: createWebHistory(),
  routes
})
