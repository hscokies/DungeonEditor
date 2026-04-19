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
@use 'src/shared/ui/utils' as utils;

.ui-icon-button {
    all: unset;
    display: flex;
    align-items: center;
    justify-content: center;
    cursor: pointer;
    color: var(--ui-icon-button-color);

    @include utils.transitions(color);
}
</style>
