<template>
    <div>
        <button @click="emit('click', orderId, status)" class="px-4 py-2 bg-blue-600 text-white rounded">
            <slot> {{ status }} </slot>
        </button>
    </div>
</template>

<script setup>
const { orderId, status } = defineProps({
    orderId: {
    type: Number,
    required: true
  },
  status: {
    type: String,
    required: true
  }
})

const emit = defineEmits(['click'])

</script>

