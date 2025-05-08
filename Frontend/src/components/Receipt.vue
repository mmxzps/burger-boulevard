<script>
export default {
  props: {
    order: Object
  },

  computed: {
    vatAmounts() {
      let acc = {}
      for (var oc of this.order.components) {
        if (oc.parent == null) return
        acc[oc.component.price.vatRate] = (acc[oc.component.price.vatRate] ?? 0) +
          oc.component.price.vatAmount
      }
      return Object.entries(acc)
    }
  }
}
</script>

<template>
  <div class="receipt-container">
    <section class="queue-nr-container">
      <p>Tack för din beställning!</p>
      <p>Du har ordernummer:</p>
      <p class="queue-number"> {{ order.id }} </p>
    </section>

    <section class="section-divider text-center">
      <img class="company-logo" src="../images/Bblogo.png" alt="Boorger Booleevaerd">

      <div class="date-receipt-nr">
        <p> {{ orderDate }} </p>
        <p> {{ receiptNumber }} </p>
      </div>

      <div class="company-details">
        <p class="text-left">[Adress]</p>
        <p class="text-right">[OrgNr]</p>
      </div>
    </section>

    <section class="receipt-grid">
      <span class="label-bold">Vara</span>
      <span class="label-bold text-center">Pris</span>
    </section>

    <section class="receipt-section section-divider">
      <ul>
        <li v-for="oc in order.components.filter(oc => oc.parent == null)" :key="oc.id">
          <div class="receipt-grid">
            <span>{{ oc.component.name }} </span>
            <span class="text-center">{{ oc.totalPrice }}</span>
          </div>

          <ul>
            <li v-for="(added, index) in oc.addedComponents" :key="index">
              <div class="receipt-grid">
                <span class="receipt-ingredient">+ {{ added.name }} </span>
                <span class="text-center"> {{ added.price.current }} </span>
              </div>
            </li>
          </ul>

          <ul>
            <li v-for="(removed, index) in oc.removedComponents" :key="index">
              <div>
                <span class="receipt-ingredient">- {{ removed.name }} </span>
              </div>
            </li>
          </ul>
        </li>
      </ul>
    </section>

    <section class="total-price-container">
      <div>
        <p class="total-price">Totalt</p>
        <p v-for="[key, value] in vatAmounts" :key="key">
          Varav moms {{ key * 100 }}%
        </p>
      </div>

      <div>
        <p class="total-price"> {{ order.totalPrice }}kr </p>
        <p v-for="[key, value] in vatAmounts" :key="key" class="vat-amount">
          {{ value }}kr
        </p>
      </div>
    </section>
  </div>
</template>

<style scoped>
.receipt-container ul {
  list-style: none;
}

.receipt-container {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  width: 70%;
  min-width: 270px;
  max-width: 350px;
  margin: 0 auto;
  margin-top: 5rem;
  margin-bottom: 5rem;
  height: 100%;
  border: 1px solid black;
  padding: 1rem;
}

.receipt-container .receipt-section {
  width: 100%;
}

.receipt-container .queue-nr-container {
  display: flex;
  flex-direction: column;
  align-items: center;
  border-bottom: 5px dotted;
  margin-bottom: 1rem;
  padding-bottom: 0.7rem;
  width: 100%;
}

.receipt-container .queue-number {
  font-size: 5rem;
}

.receipt-container .company-logo {
  width: 35%;
}

.receipt-container .date-receipt-nr {
  display: flex;
  justify-content: space-between;
  width: 100%;
}

.receipt-container .company-details {
  display: flex;
  justify-content: space-between;
  width: 100%;
}

.receipt-container .receipt-grid {
  display: grid;
  grid-template-columns: 2fr 1fr 1fr;
  width: 100%;
}

.receipt-container .label-bold {
  margin-bottom: 10px;
  font-weight: bold;
}

.receipt-container .text-center {
  text-align: center;
}

.receipt-container .text-right {
  text-align: right;
}

.receipt-container .text-left {
  text-align: left;
}

.receipt-ingredient {
  padding-left: 0.7rem;
  font-size: 0.8rem;
}

.receipt-container .total-price-container {
  display: flex;
  justify-content: space-between;
  width: 100%;
}

.receipt-container .total-price {
  font-size: 20px;
  font-weight: bold;
}

.receipt-container .vat-amount {
  display: flex;
  justify-content: flex-end;
}

.receipt-container .section-divider {
  border-bottom: 3px dotted;
  margin-bottom: 0.7rem;
  padding-bottom: 0.7rem;
}

.reset-btn {
  position: fixed;
  top: 95%;
  left: 10%;
  transform: translate(-50%, -50%);
  padding: 0.5rem;
}
</style>
