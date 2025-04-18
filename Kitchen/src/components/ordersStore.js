import { defineStore } from 'pinia';

export const useOrdersStore = defineStore('orders', {
    state: () => ({
        orders: [],
    }),
    getters: {
        getOrders: (state) => {
            return state.orders;
        },
        pendingOrders: (state) => {
            return state.orders.filter(order => order.status === 0);
        },
        preparingOrders: (state) => {
            return state.orders.filter(order => order.status === 1);
        },
        doneOrders: (state) => {
            return state.orders.filter(order => order.status === 2);
        },
    },
    actions: {
        async fetchOrders() {
            try {

                const response = await fetch('https://localhost:7115/api/Orders')
                if (response.ok) {
                    const data = await response.json()
                    this.setOrders(data)
                }
            }
            catch (error) {
                console.error('Error fetching orders:', error)
            }
        },
        setOrders(newOrders) {
            this.orders = newOrders;
        }
    }
});