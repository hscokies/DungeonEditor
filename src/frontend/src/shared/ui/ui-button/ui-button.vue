<script setup lang="ts">
import type { PropTypes } from './ui-button.styles.ts';
import { UiSpinner } from '@/shared/ui';

const { label, active, disabled, loading } = defineProps<PropTypes>();
</script>

<template>
    <button v-bind="$attrs" :class="$cn({ active: active && !disabled })" :disabled="disabled">
        <slot name="prefix" />
        <span v-if="!loading" :class="$cn('label')">
            {{ label }}
        </span>
        <span v-else>
            <ui-spinner :size="16" />
        </span>
        <slot name="suffix" />
    </button>
</template>

<style scoped lang="scss">
@use '.' as styles;
@use 'src/shared/ui/border-radius' as border-radius;
@use 'src/shared/ui/typography' as typography;
@use 'src/shared/ui/spacing' as spacing;
@use 'src/shared/ui/colors' as colors;
@use 'src/shared/ui/utils' as utils;
@use 'src/shared/ui/variables' as variables;

.ui-button {
    $root: &;

    display: inline-grid;
    place-items: center;
    grid-auto-flow: column;
    gap: var(--ui-button-gap, spacing.$spacing-1);
    border-radius: var(--ui-button-border-radius, border-radius.$border-radius-md);
    padding: var(--ui-button-padding, spacing.$spacing-2 spacing.$spacing-3-5);
    font-size: var(--ui-button-font-size);
    font-weight: var(--ui-button-font-weight, typography.$font-weight-semibold);
    user-select: none;
    border: variables.$border;
    cursor: pointer;

    @include utils.transitions(border-color, background-color);

    @include styles.variant(
        $root,
        var(--ui-button-color, colors.$label-1),
        var(--ui-button-background, colors.$surface-element-0),
        var(--ui-button-hover-background, colors.$surface-element-1),
        var(--ui-button-hover-color),
        var(--ui-button-active-border-color, colors.$cpt-mauve)
    );

    &:disabled {
        pointer-events: none;
        opacity: variables.$disabled-opacity;
    }
}
</style>
