import {defineStore} from 'pinia';

export const useOrdersQueueStore = defineStore('ordersQueue', {

    state: () => ({
        orders: [],
        fetchInterval: null,
    }),
    getters: {
        getOrders: (state) => {
            return state.orders;
        },
        incomingOrders: (state) => {
            return state.orders.filter(order => order.status === 'pending' || order.status === 'preparing');
        },
        doneOrders: (state) => {
            return state.orders.filter(order => order.status === 'done');
        },
    },
    actions: {
        async fetchQueue() {
            try {

                const response = await fetch('https://localhost:7115/api/Orders')
                if (response.ok) {
                    const data = await response.json()
                    console.log(data)
                    this.setQueue(data)
                }
            }
            catch (error) {
                console.error('Error fetching orders:', error)
            }
        },
        startFetchingQueue() {
            if (this.fetchInterval) return;
            
            this.fetchInterval = setInterval(() => {
                this.fetchQueue();
            }, 5000);
        },
    
        stopFetchingQueue() {
            if (this.fetchInterval) {
                clearInterval(this.fetchInterval);
                this.fetchInterval = null;
            }
        },
        setQueue(newOrders) {
            this.orders = newOrders;
        }
    },
    
});