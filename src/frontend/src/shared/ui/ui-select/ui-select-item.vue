<script setup lang="ts">
import type { PropTypes } from './ui-select-item.types.ts';
import { computed } from 'vue';

const { header, label, role, focused, selected, height } = defineProps<PropTypes>();

const styles = computed(() => ({
    height: `${height}px`,
}));
</script>

<template>
    <div
        :class="$cn({ header, selected, focused })"
        :style="styles"
        :aria-label="label"
        :aria-checked="selected"
        :role="role"
        v-bind="$attrs"
    >
        <span :class="$cn('label')">
            {{ label }}
        </span>
    </div>
</template>

<style scoped lang="scss">
@use 'src/shared/ui/colors' as colors;
@use 'src/shared/ui/spacing' as spacing;
@use 'src/shared/ui/utils' as utils;
@use 'src/shared/ui/typography' as typography;
@use 'sass:color' as color;

.ui-select-item {
    display: inline-flex;
    align-items: center;
    width: 100%;
    padding: var(--ui-select-option-padding, spacing.$spacing-1);

    @include utils.transitions(background);

    &__label {
        overflow: auto hidden;
        white-space: nowrap;
        scrollbar-width: none;
    }

    &--selected {
        background-color: color.scale(colors.$cpt-lavender, $lightness: -30%);
        color: colors.$on-accent;
    }

    &--focused {
        background-color: colors.$cpt-lavender;
        color: colors.$on-accent;
        cursor: pointer;
    }

    &--header {
        font-weight: typography.$font-weight-semibold;
        text-transform: uppercase;
        cursor: default;
    }
}
</style>
