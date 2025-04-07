<template>
    <div class="order">
        <h2>Order # {{ order.id }}</h2>
        <p>Status: {{ order.status }}</p>
        <p>Take Away: {{ order.takeAway ? 'Yes' : 'No' }}</p>

        <div v-for="orderComponent in order.orderComponents" :key="orderComponent.id" class="order-item">
            <template v-if="orderComponent.component.level === 'Menu'">
                <div>
                    <h3>{{ orderComponent.component.name }}</h3>
                    <div v-for="childPolicy in orderComponent.component.childPolicies" :key="childPolicy.id">
                        <div v-if="childPolicy.child.level === 'Product'">
                            <h4>{{ childPolicy.child.name }}</h4>
                            <div v-for="ingredient in childPolicy.child.childPolicies" :key="ingredient.id">
                                <p>{{ ingredient.child.name }}</p>
                            </div>
                        </div>
                    </div>
                </div>
            </template>
            
            <template v-else-if="orderComponent.component.level === 'Product'">
                <div>
                    <h3>{{ orderComponent.component.name }}</h3>
                    <div v-for="ingredient in orderComponent.component.childPolicies" :key="ingredient.id">
                        <p>{{ ingredient.child.name }}</p>
                    </div>
                </div>
            </template>
        </div>
    </div>
</template>

<script>
export default {
  data() {
    return {
      order: {
        id: 1,
        status: "Pending",
        takeAway: false,
        orderComponents: [
          {
            id: 1,
            component: {
              id: 101,
              level: "Menu",
              name: "Combo Meal",
              childPolicies: [
                {
                  id: 1,
                  child: {
                    id: 201,
                    level: "Product",
                    name: "Cheeseburger",
                    childPolicies: [
                      { id: 301, child: { id: 401, name: "Lettuce" } },
                      { id: 302, child: { id: 402, name: "Tomato" } },
                    ],
                  },
                },
                {
                  id: 2,
                  child: {
                    id: 202,
                    level: "Product",
                    name: "Fries",
                    childPolicies: [],
                  },
                },
              ],
            },
          },
          {
            id: 2,
            component: {
              id: 102,
              level: "Product",
              name: "Soda",
              childPolicies: [
                { id: 303, child: { id: 403, name: "Ice" } },
              ],
            },
          },
        ],
      },
    };
  },
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