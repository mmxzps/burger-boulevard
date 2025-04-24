<template>
  <div class="order">
    <div class="order-header">
      <h2>Order #{{ props.order.id }}</h2>
      <h4 class="align-right">{{ props.order.takeAway ? 'Take Away' : 'Eat Here' }}</h4>
      <h4 class="align-right">Ordered: {{ new Date('1970-01-01T' + props.order.orderTime).toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' }) }}</h4>
      <button @click="collapsed = !collapsed">
        <span :class="{ rotated: !collapsed }">▼</span>
      </button>
    </div>
    <div class="btn-container">
      <UpdateStatusBtn v-for="status in statusOptions" :key="status" :orderId="props.order.id" :status="status"
        @click="store.updateOrderStatus" />
    </div>
    <div v-for="main in mainComponents" :key="main.id" class="item">
      <h4>{{ main.component?.name || 'Unnamed' }}</h4>

      <div v-show="!collapsed">
        <ul>
          <li v-for="child in getChildren(main.id)" :key="child.id">
            {{ child.component?.name || 'Unnamed' }}
          </li>
        </ul>
      </div>
    </div>
  </div>
</template>

<!-- <template>
  <div class="order">
    <div class="order-header">
      <h2>Order #{{ props.order.id }}</h2>
      <h4 class="align-right">{{ props.order.takeAway ? 'Take Away' : 'Eat Here' }}</h4>
      <h4 class="align-right">Ordered: {{ props.order.orderTime }}</h4>

      
      <button @click="toggleCollapse">
        <span :class="{ rotated: !collapsed }">▼</span>
        Show/Hide Burger Ingredients
      </button>
    </div>

    <div class="btn-container">
      <UpdateStatusBtn
        v-for="status in statusOptions"
        :key="status"
        :orderId="props.order.id"
        :status="status"
        @click="store.updateOrderStatus"
      />
    </div>

   
    <div v-for="main in mainComponents" :key="main.id" class="item">
      <h4>{{ main.component?.name || "Unnamed" }}</h4>

     
      <div v-show="!collapsed && main.component?.type === 'burger'">
        <ul>
          <li v-for="ingredient in props.order.components.filter(c => c.parentId === main.id)"
            :key="ingredient.id"
          >
            {{ ingredient.component?.name }}
          </li>
        </ul>
      </div>
    </div>
  </div>
</template> -->

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

const getChildren = (mainId) =>
  props.order.components.filter(c => c.parent === mainId);

const statusOptions = computed(() => {
  const currentStatus = props.order.status;
  return statusMap[currentStatus] || []
});

</script>

<style scoped>
.order {
  margin: 0.5rem auto;
  padding: 0.5rem;

  background: #fafafa;
  border-radius: 8px;
  box-shadow: 0 1px 4px rgba(0, 0, 0, 0.1);
}

.order-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  width: 100%;
}

.btn-container {
  display: flex;
  justify-content: space-around;
  margin-bottom: 0.5rem;
}

.order h2 {
  margin-bottom: 0.5rem;
}

.order p {
  margin: 0.25rem 0;
  color: #555;
}

order:first-child {
  margin-top: 0;
  padding-top: 20px;
}

.rotated {
  transform: rotate(180deg);
  transition: transform 0.3s ease;
}

.item {
  margin-top: 0.5rem;
  padding: 0.5rem;
  background: #fff;
  border-radius: 6px;
  border-left: 4px solid #007bff;
  transition: background 0.3s ease, transform 0.2s ease;
}

.item:hover {
  background: #f0f0f0;
  transform: translateY(-2px);
}

.item h3 {
  margin: 0 0 0.25rem;
  font-size: 1.1rem;
}

.item p {
  margin: 0.15rem 0;
  color: #666;
}
</style>
