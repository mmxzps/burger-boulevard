<template>
  <div v-for="order in orders" :key="order.id" class="order">
    <h2>Order # {{ order.id }}</h2>
    <p>Status: {{ order.status === 0 ? "Pending" : order.status === 1 ? "Preparing" : order.status === 2 ? "Done" : "Unknown"}}</p>
    <p>Take Away: {{ order.takeAway ? 'Yes' : 'No' }}</p>

    <div v-for="orderComponent in order.components" :key="orderComponent.id" class="order-item">
      
      <div>
        <h3>{{ orderComponent.component.name ?? "Product name missing" }}</h3>
        <p>{{ orderComponent.component.description ?? "Description missing" }}</p>
        <p>Price: {{ orderComponent.component.price }} SEK</p>
      </div>
    </div>
  </div>
</template>

<script>
import { ordersStore } from './ordersStore.js';

export default {
  data() {
    return {
      orders: ordersStore.orders
    };
  },
  async mounted() {
  try {
    const response = await fetch('https://localhost:7115/api/Orders');
    if (!response.ok) {
      throw new Error('Failed to fetch data');
    }
    const data = await response.json();
    console.log('Fetched orders data:', data); // Log the data to inspect
    this.orders = data;  // Store it in your component's orders
  } catch (error) {
    console.error('Error fetching orders:', error);
  }
}
  // async mounted() {
  //   await ordersStore.fetchOrders();
  //   this.orders = ordersStore.orders;
  // }
};
</script>


<style scoped>
.order {
  font-family: 'Arial', sans-serif;
  padding: 20px;
  max-width: 900px;
  margin: 0 auto;
  background-color: #f9f9f9;
  border-radius: 10px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}


.order h2 {
  font-size: 2rem;
  margin-bottom: 10px;
  color: #333;
}

.order p {
  font-size: 1rem;
  color: #666;
  margin-bottom: 10px;
}


.order-item {
  margin-bottom: 20px;
  padding: 15px;
  background-color: #fff;
  border-radius: 8px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

.order-item h3 {
  font-size: 1.5rem;
  color: #444;
  margin-bottom: 10px;
}

.order-item h4 {
  font-size: 1.2rem;
  margin-bottom: 5px;
  color: #555;
}


.order-item p {
  font-size: 1rem;
  color: #777;
  margin-left: 20px;
}


.order-item ul {
  list-style-type: none;
  padding-left: 20px;
}

.order-item ul li {
  margin-bottom: 5px;
}

.order-item div {
  margin-bottom: 15px;
}

.order-item div p {
  font-size: 1rem;
  color: #666;
}


.order-item .menu {
  border-left: 4px solid #007bff;
  padding-left: 20px;
  margin-top: 15px;
}

.order-item .product {
  border-left: 4px solid #28a745;
  padding-left: 20px;
  margin-top: 10px;
}


.order-item .ingredient {
  font-size: 1rem;
  color: #888;
  margin-left: 20px;
  padding: 5px 0;
}


.order-item:hover {
  background-color: #f4f4f4;
  cursor: pointer;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.15);
}

.order-item h3:hover {
  color: #007bff;
}

/* Responsive Design */
@media (max-width: 768px) {
  .order {
    padding: 15px;
    max-width: 100%;
  }

  .order-item h3 {
    font-size: 1.3rem;
  }

  .order-item h4 {
    font-size: 1rem;
  }

  .order-item p {
    font-size: 0.9rem;
  }
}
</style>