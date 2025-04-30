<template>
    <div class="queue-wrapper">
      <div class="column incoming">
        <div v-for="order in incomingOrders" :key="order.id" class="order incoming">
          <QueueOrder :order="order" />
        </div>
      </div>

      <div class="column done">
        <div v-for="order in doneOrders" :key="order.id" class="order done">
          <QueueOrder :order="order" />
        </div>
      </div>
    </div>
</template>

<script setup>
import { onMounted, onBeforeUnmount } from 'vue';
import { storeToRefs } from 'pinia';
import { useOrdersQueueStore } from '@/components/ordersQueueStore';
import QueueOrder from '@/components/QueueOrder.vue';

const ordersQueueStore = useOrdersQueueStore();
const { incomingOrders, doneOrders } = storeToRefs(ordersQueueStore);

onMounted(async () => {
    await ordersQueueStore.fetchQueue();
    ordersQueueStore.startFetchingQueue();
});

onBeforeUnmount(() => {
    ordersQueueStore.stopFetchingQueue()
})
</script>

<style scoped>
.queue-wrapper {
  display: flex;
  gap: 2rem;
  padding: 1rem;
}

.column {
  flex: 1;
  padding: 1rem;
}

.column h2 {
  margin-bottom: 0.5rem;
  font-size: 1.25rem;
}

.column.incoming,
.column.done {
  columns: 2;          
  column-gap: 1rem;    
  border: 1px solid black;
  border-radius: 0.5rem;
}

.order {
  break-inside: avoid; 
  margin-bottom: 1rem;
}
.column.incoming{
    background-color: #a1a1a1; /* Light blue for incoming orders */
}
.column.done{
    background-color: #b9cfd1; /* Light beige for done orders */
}
</style>