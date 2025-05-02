<template>
  <div class="order">

    <div class="left-side">
      <div class="order-header">
        <div class="order-title">
          <h2>Order #{{ props.order.id }}</h2>
          <button class="collapse-btn" @click="collapsed = !collapsed" :disabled="isCollapseDisabled">
            <span :class="{ rotated: !collapsed }">▼</span>
          </button>
        </div>
        <h4>{{ props.order.takeAway ? 'Take Away' : 'Eat Here' }}</h4>
        <h4>Ordered: {{ new Date('1970-01-01T' + props.order.orderTime).toLocaleTimeString([], {
          hour: '2-digit', minute: '2-digit'
        }) }}</h4>
      </div>


    </div>


    <div class="middle">
      <div v-for="main in mainComponents" :key="main.id" class="item">
        <h4>{{ main.component?.name || 'Unnamed' }}</h4>
        <div v-show="!collapsed" >
          <ul>
            <li v-for="a in main.component.addedComponents" :key="a.id">+ {{ a.name }}</li>
          </ul>
          <ul>
            <li v-for="r in main.component.removedComponents" :key="r.id">- {{ r.name }}</li>
          </ul>
        </div>
      </div>
    </div>
    <div class="right-side">
      <div class="btn-container">
        <UpdateStatusBtn v-for="opt in statusOptions" :key="opt.targetStatus"
          :arrow="opt.arrow" :targetStatus="opt.targetStatus"
          @click="status => store.updateOrderStatus(props.order.id, status)" />
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
const mainComponents = computed(() =>
  props.order.components.filter(c => c.component.level === 1)
);

const isCollapseDisabled = computed(() =>
  mainComponents.value.every(
    main =>
      (main.component?.addedComponents?.length ?? 0) === 0 &&
      (main.component?.removedComponents?.length ?? 0) === 0
  )
);

const statusMap = {
  pending: ['preparing'],
  preparing: ['pending', 'done'],
  done: ['preparing'],
};

const arrowMap = {
  forward: '→',
  backward: '←',
};

const statusOrder = ['pending', 'preparing', 'done'];

const statusOptions = computed(() => {
  const currentStatus = props.order.status;
  const possibleStatuses = statusMap[currentStatus] || [];

  return possibleStatuses.map(targetStatus => {
    const currentIndex = statusOrder.indexOf(currentStatus);
    const targetIndex = statusOrder.indexOf(targetStatus);
    const direction = targetIndex > currentIndex ? 'forward' : 'backward';
    return {
      targetStatus,
      arrow: arrowMap[direction],
    };
  });
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
  font-size: 1.2rem;
  font-weight: 600;
}

.order h4 {
  margin: 0.1rem 0;
  font-size: 0.9rem;
}
span {
  display: inline-block;
  transition: transform 0.3s ease;
}
.rotated {
  transform: rotate(180deg);
  transition: transform 0.3s ease;
}

.btn-container {
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  gap: 0.5rem;
  margin-top: 0.25rem;
}

.middle {
  display: flex;
  flex-direction: column;
  flex-grow: 1;
  width: 50%;

}

.right-side {
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  width: 20%;

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
  background: linear-gradient(135deg, #4f46e5, #3b82f6);
  color: white;
}
.collapse-btn:disabled {
  background: #e5e7eb;
  color: #9ca3af;
  cursor: not-allowed;
}
.collapse-btn:hover {
  background: linear-gradient(135deg, #4338ca, #2563eb);
  transform: translateY(-2px);
  box-shadow: 0 6px 16px rgba(59, 130, 246, 0.4);
}

.collapse-btn:active {
  transform: scale(0.97);
  box-shadow: 0 2px 6px rgba(59, 130, 246, 0.2);
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
