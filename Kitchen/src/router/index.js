import { createRouter, createWebHistory } from 'vue-router'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'orders',
      // route level code-splitting
      // this generates a separate chunk (About.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import('@/views/OrdersView.vue'),
    },
    {
      path: '/queue',
      name: 'queue',
      component: () => import('@/views/QueueView.vue'),
    }
  ],
})

export default router
