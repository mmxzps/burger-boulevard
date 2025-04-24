import { createRouter, createWebHistory } from 'vue-router'
import GetAllPoducts from '../components/GetAllPoducts.vue'

const routes = [
  {
    path: '/:category',
    // name: '',
    component: GetAllPoducts
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router
