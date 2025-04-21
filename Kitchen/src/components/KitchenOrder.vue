<template>
  <div class="order">
    <h2>Order #{{ order.id }}</h2>
    <p>Status: {{ order.status || 'Unknown' }}</p>
    <p>Take Away: {{ order.takeAway ? 'Yes' : 'No' }}</p>
    <button @click="collapsed = !collapsed">
        <span :class="{ rotated: !collapsed }">▼</span>
      </button>
    <div v-for="item in order.components" :key="item.id" class="item">
      <h3>{{ item.component?.name || "Unnamed" }}</h3>
      <div v-show="!collapsed">
        <p>{{ item.component?.description || "No description" }}</p>
          <p>{{ item.component?.price || "N/A"}} SEK</p>
      </div>
      
    </div>
  </div>

  
  <!-- <div v-for="child in item.component.children" :key="child.id">
      <p>{{ child.name }}</p>
  </div> -->

</template>

<script setup>
import { ref } from 'vue';
defineProps({
  order: {
    type: Object,
    required: true,
  },
});

const collapsed = ref(true);
</script>

<style scoped>
.order {
  margin: 0.5rem auto;
  padding: 0.5rem;
  
  background: #fafafa;
  border-radius: 8px;
  box-shadow: 0 1px 4px rgba(0, 0, 0, 0.1);
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
