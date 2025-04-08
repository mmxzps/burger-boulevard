import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import KitchenOrder from './components/KitchenOrder.vue'
import { ordersStore } from './components/ordersStore.js'

const app = createApp(App)

app.use(router)
app.use(ordersStore)
app.component('KitchenOrder', KitchenOrder)

app.mount('#app')
