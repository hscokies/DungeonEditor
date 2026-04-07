<script setup lang="ts">
import { computed } from 'vue';
import type { PropTypes } from './ui-spinner.types.ts';

const { size, thickness } = defineProps<PropTypes>();

const styles = computed(() => {
    return {
        ...(size && { '--ui-spinner-size': `${size}px` }),
        ...(thickness && { '--ui-spinner-thickness': `${thickness}px` }),
    };
});
</script>

<template>
    <div :class="$cn()" :style="styles"></div>
</template>

<style scoped lang="scss">
.ui-spinner {
    display: inline-block;
    width: var(--ui-spinner-size, 30px);
    aspect-ratio: 1;
    border: var(--ui-spinner-thickness, 3px) solid currentcolor;
    border-bottom-color: transparent;
    border-radius: 50%;
    opacity: var(--ui-spinner-opacity, 0.5);
    animation: spin 1s linear infinite;
}

@keyframes spin {
    0% {
        rotate: 0deg;
    }
    100% {
        rotate: 360deg;
    }
}
</style>
