<script setup lang="ts">
const props = defineProps<{
    label: string;
}>();
</script>

<template>
    <button :aria-label="label" :title="label" :class="$cn()" v-bind="$attrs">
        <slot />
    </button>
</template>

<style scoped lang="scss">
.ui-icon-button {
    all: unset;
    display: flex;
    align-items: center;
    justify-content: center;
    cursor: pointer;

    &__icon {
        color: var(--ui-icon-button-color, currentColor);
    }
}
</style>
