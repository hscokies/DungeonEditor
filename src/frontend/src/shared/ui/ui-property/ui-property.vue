<script setup lang="ts">
defineProps({
    id: { type: String, required: true },
    label: { type: String, required: true },
    direction: { type: String, default: 'horizontal' },
    error: String,
});
</script>

<template>
    <div :class="$cn({ direction })">
        <label :class="$cn('label')" :for="id">{{ label }}</label>
        <slot v-bind="{ id, invalid: !!error?.length }" />
        <span :class="$cn('spacer')" />
        <span :class="$cn('error')">
            {{ error }}
        </span>
    </div>
</template>

<style scoped lang="scss">
@use 'src/shared/ui/spacing' as spacing;
@use 'src/shared/ui/colors' as colors;

.ui-property {
    $root: &;

    display: inline-grid;

    &--direction-horizontal {
        grid-template:
            'a b' auto
            '. c' 1fr;
        place-items: baseline;
        gap: var(--ui-property-horizontal-gap, spacing.$spacing-1 spacing.$spacing-2-5);
    }

    &--direction-vertical {
        grid-auto-flow: row;

        & #{$root}__label {
            margin-bottom: var(--ui-property-vertical-label-spacing, spacing.$spacing-1-5);
        }

        & #{$root}__error {
            margin-top: var(--ui-property-vertical-error-spacing, spacing.$spacing-1);
        }

        & #{$root}__spacer {
            display: none;
        }
    }

    &__error {
        color: var(--ui-input-invalid-border-color, colors.$error);
    }
}
</style>
