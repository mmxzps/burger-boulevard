<template>
    <div :class="['queue-order', orderStatusClass]">
        <h3>Order #{{props.order.id}}</h3>
    </div>

</template>

<script setup>
import { computed } from 'vue';

const props = defineProps({
  order: {
    type: Object,
    required: true,
  },
});

const orderStatusClass = computed(() => {
  if (props.order.status === 'pending' || props.order.status === 'preparing') {
    return 'preparing';
  } else if (props.order.status === 'done') {
    console.log('Order status:', props.order.status);
    return 'done';
  } else {
    return '';
  }
});
</script>

<style scoped>
.queue-order {
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
  padding: 1rem;
  border-radius: 0.5rem;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

.preparing {
  background-color: #fff45f;
}

.done {
  background-color: #67f33c
}
h3{
    margin: 0;
    font-size: 2.5rem;
    font-weight: bold;
    color: #333;
}
</style>
