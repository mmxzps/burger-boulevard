import { createRouter, createWebHistory } from 'vue-router'
import GetAllPoducts from '../components/GetAllPoducts.vue'

// Define routes
const routes = [
  {
    path: '/',
    name: 'Home',
    component: GetAllPoducts
  },
  {
    path: '/hamburgare',
    name: 'Hamburgare',
    component: GetAllPoducts
  },
  {
    path: '/kycklingfisk',
    name: 'KycklingFisk',
    component: GetAllPoducts
  },
  {
    path: '/snacks',
    name: 'Snacks',
    component: GetAllPoducts
  },
  {
    path: '/sidesdip',
    name: 'SidesDip',
    component: GetAllPoducts
  },
  {
    path: '/sallad',
    name: 'Sallad',
    component: GetAllPoducts
  },
  {
    path: '/kalldryck',
    name: 'KallDryck',
    component: GetAllPoducts
  },
  {
    path: '/varmdryck',
    name: 'VarmDryck',
    component: GetAllPoducts
  },
  {
    path: '/dessert',
    name: 'Dessert',
    component: GetAllPoducts
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router