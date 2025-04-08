export const ordersStore = {
    orders: [],

    async fetchOrders() {
        try {

            const response = await fetch('https://localhost:7115/api/Orders')
            if (response.ok) {
                const data = await response.json()
                this.orders = data;
            }
        }
        catch (error) {
            console.error('Error fetching orders:', error)
        }
    },
    setOrders(newOrders) {
        this.orders = newOrders;
    },
      
};