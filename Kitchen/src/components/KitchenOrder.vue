<template>
  <div class="order">

    <div class="left-side">
      <div class="order-header">
        <div class="order-title">
          <h2>Order #{{ props.order.id }}</h2>
          <button class="collapse-btn" @click="collapsed = !collapsed">
            <span :class="{ rotated: !collapsed }">▼</span>
          </button>
        </div>
        <h4>{{ props.order.takeAway ? 'Take Away' : 'Eat Here' }}</h4>
        <h4>Ordered: {{ new Date('1970-01-01T' + props.order.orderTime).toLocaleTimeString([], {
          hour: '2-digit', minute: '2-digit'
        }) }}</h4>
      </div>

      <div class="btn-container">
        <UpdateStatusBtn v-for="status in statusOptions" :key="status" :orderId="props.order.id" :status="status"
          @click="store.updateOrderStatus" />
      </div>
    </div>


    <div class="right-side">
      <div v-for="main in mainComponents" :key="main.id" class="item">
        <h4>{{ main.component?.name || 'Unnamed' }}</h4>
        <div v-show="!collapsed">
          <ul>
            <li v-for="a in main.component.addedComponents" :key="a.id">+ {{ a.name }}</li>
          </ul>
          <ul>
            <li v-for="r in main.component.removedComponents" :key="r.id">- {{ r.name }}</li>
          </ul>
        </div>
      </div>
    </div>
  </div>
</template>


<script setup>
import { ref, computed } from 'vue';
import UpdateStatusBtn from './UpdateStatusBtn.vue';
import { useOrdersStore } from './ordersStore';

const store = useOrdersStore();

const props = defineProps({
  order: {
    type: Object,
    required: true,
  },
});

const collapsed = ref(false);

const statusMap = {
  pending: ['preparing'],
  preparing: ['pending', 'done'],
  done: ['preparing'],
};

const mainComponents = computed(() =>
  props.order.components.filter(c => c.parent === null)
);



const statusOptions = computed(() => {
  const currentStatus = props.order.status;
  return statusMap[currentStatus] || []
});

</script>

<style scoped>
.order {
  margin: 0 auto;
  padding: 0;
  background: #f9f9f9;
  border-radius: 4px;
  display: flex;
  justify-content: space-between;
  align-items: flex-start;
}

.left-side {
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  width: 30%;
  /* Allocate space for the left-side content */
  padding-right: 1rem;
  /* Add some space between left and right sections */
}

.order-header {
  display: flex;
  flex-direction: column;
  font-size: 0.9rem;
  margin-bottom: 0.5rem;
}

.order h2 {
  margin-bottom: 0;
  font-size: 1.1rem;
}

.order h4 {
  margin: 0.1rem 0;
  font-size: 0.9rem;
}

.rotated {
  transform: rotate(180deg);
  transition: transform 0.3s ease;
}

.btn-container {
  display: flex;
  flex-direction: row;
  justify-content: flex-start;
  gap: 0.5rem;
  margin-top: 0.25rem;
}

.right-side {
  display: flex;
  flex-direction: column;
  flex-grow: 1;
  /* Allow this side to grow and take up remaining space */
  width: 70%;
  /* Occupy remaining space */
}
.order-title {
  display: flex;
  align-items: center;
  gap: 0.3rem;
}

.collapse-btn {
  font-size: 1rem;
  padding: 0.1rem 0.3rem;
  margin-left: 0.5rem;
  cursor: pointer;
  line-height: 1;
}

.item {
  margin-top: 0.25rem;
  padding: 0.25rem;
  background: #fff;
  border-radius: 4px;
  border-left: 3px solid #007bff;
  transition: background 0.3s ease, transform 0.2s ease;
  font-size: 0.9rem;
}

.item:hover {
  background: #f1f1f1;
  transform: translateY(0);
  /* Flatter appearance */
}

.item h4 {
  margin: 0 0 0.15rem;
  font-size: 1rem;
}

.item p {
  margin: 0.1rem 0;
  color: #666;
}

ul {
  margin: 0;
  padding: 0;
  list-style-type: none;
}

li {
  font-size: 0.9rem;
  margin-bottom: 0.15rem;
}
</style>
