<template>
    <div class="orders-wrapper">
      <div class="column pending">
        <h2>Pending</h2>
        <div v-for="order in pendingOrders" :key="order.id" class="order">
          <KitchenOrder :order="order" />
        </div>
      </div>
  
      <div class="column preparing">
        <h2>Preparing</h2>
        <div v-for="order in preparingOrders" :key="order.id" class="order">
          <KitchenOrder :order="order" />
        </div>
      </div>
  
      <div class="column done">
        <h2>Done</h2>
        <div v-for="order in doneOrders" :key="order.id" class="order">
          <KitchenOrder :order="order" />
        </div>
      </div>
    </div>
</template>

<script setup>
import { onMounted } from 'vue';
import { storeToRefs } from 'pinia';
import { useOrdersStore } from '../components/ordersStore.js';
import KitchenOrder from '../components/KitchenOrder.vue';

const ordersStore = useOrdersStore();
const {pendingOrders, preparingOrders, doneOrders} = storeToRefs(ordersStore);

onMounted(() =>  {
  ordersStore.fetchOrders();
});

</script>

<style scoped>
.orders-wrapper {
  display: flex;
  justify-content: space-between;
  gap: 1rem;
  padding: 1rem;
  width: 100%;
  height: 100%;
  flex-wrap: nowrap; /* Ensure columns do not wrap */
  box-sizing: border-box;
}

.column {
  flex: 1 1 33.33%; /* Allow each column to grow equally and fill available space */
  padding: 1rem;
  background-color: #f9f9f9;
  border-radius: 8px;
  box-shadow: 0 1px 4px rgba(0, 0, 0, 0.1);
  overflow-y: auto;
  height: 100%; /* Ensure the columns fill vertically */
  border-width: 2px;
  border-style: solid;
}

.column h2 {
  text-align: center;
  margin-bottom: 1rem;
}

.order {
  margin-bottom: 1rem;
  background-color: white;
  padding: 0.75rem;
  border-radius: 6px;
  box-shadow: 0 1px 4px rgba(0, 0, 0, 0.1);
}
.pending {
  border-color: red;
}
.preparing {
  border-color: yellow;
}
.done {
  border-color: green;
}
.order:last-child {
  margin-bottom: 0;
}
</style>