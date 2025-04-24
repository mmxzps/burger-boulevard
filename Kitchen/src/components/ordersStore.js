import { defineStore } from 'pinia';

export const useOrdersStore = defineStore('orders', {
    state: () => ({
        orders: [],
        fetchInterval: null,
    }),
    getters: {
        getOrders: (state) => {
            return state.orders;
        },
        pendingOrders: (state) => {
            return state.orders.filter(order => order.status === 'pending');
        },
        preparingOrders: (state) => {
            return state.orders.filter(order => order.status === 'preparing');
        },
        doneOrders: (state) => {
            return state.orders.filter(order => order.status === 'done');
        },
    },
    actions: {
        async fetchOrders() {
            try {

                const response = await fetch('https://localhost:7115/api/Orders')
                if (response.ok) {
                    const data = await response.json()
                    console.log(data)
                    this.setOrders(data)
                }
            }
            catch (error) {
                console.error('Error fetching orders:', error)
            }
        },
        async updateOrderStatus(id, status) {
            try {
                const response = await fetch(`https://localhost:7115/api/Orders/${id}`, {
                    method: 'PUT',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify({ status }),
                })

                if (!response.ok) {
                    throw new Error('Failed to update order')
                }

                await this.fetchOrders()
            }
            catch (error) {
                console.error('Error updating order:', error)
            }

        },
        startFetchingOrders() {
            if (this.fetchInterval) return;
            
            this.fetchInterval = setInterval(() => {
                this.fetchOrders();
            }, 5000);
        },

        stopFetchingOrders() {
            if (this.fetchInterval) {
                clearInterval(this.fetchInterval);
                this.fetchInterval = null;
            }
        },
        setOrders(newOrders) {
            this.orders = newOrders;
        }
    }
});