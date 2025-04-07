import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import KitchenOrder from './components/KitchenOrder.vue'

const app = createApp(App)

app.use(router)
app.component('KitchenOrder', KitchenOrder)

app.mount('#app')
