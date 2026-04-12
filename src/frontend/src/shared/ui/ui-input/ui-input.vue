<script setup lang="ts">
import { useCustomFocus } from '@/shared/hooks';
import { ref } from 'vue';
import type { PropTypes } from './ui-input.types.ts';

const { focused, onFocus, onBlur } = useCustomFocus();
const { type = 'text', placeholder, disabled = false, invalid = false, readonly = false } = defineProps<PropTypes>();

const fieldRef = ref<HTMLInputElement | null>(null);
const model = defineModel();

function onFocusIn(e: FocusEvent) {
    if (e.target === fieldRef.value) {
        onFocus();
    }
}
</script>

<template>
    <div
        :class="$cn({ disabled, readonly, invalid, focused: focused && !disabled && !readonly })"
        @focusin="onFocusIn"
        @focusout="onBlur"
    >
        <div v-if="$slots.prefix" :class="$cn('prefix')">
            <slot name="prefix" />
        </div>
        <input
            ref="fieldRef"
            v-bind="$attrs"
            :type="type"
            :class="$cn('field')"
            :disabled="disabled"
            :readonly="readonly"
            :placeholder="placeholder"
            v-model="model"
        />
        <div v-if="$slots.suffix" :class="$cn('suffix')">
            <slot name="suffix" />
        </div>
    </div>
</template>

<style lang="scss" scoped>
@use 'src/shared/ui/input' as input;
@use 'src/shared/ui/border-radius' as border-radius;
@use 'src/shared/ui/typography' as typography;
@use 'src/shared/ui/spacing' as spacing;
@use 'src/shared/ui/colors' as colors;
@use 'src/shared/ui/utils' as utils;
@use 'src/shared/ui/variables' as variables;

.ui-input {
    $root: &;

    @include input.style();

    &__field {
        flex: 1;
        border: none;
        background: transparent;
        color: inherit;
        font-size: inherit;
        padding: var(--ui-input-field-padding, spacing.$spacing-2);

        &:focus {
            outline: none;
        }

        &:read-only {
            border: none !important;
        }

        &::placeholder {
            color: var(--ui-input-placeholder-color, rgba(colors.$label-1, 0.5));
        }

        &[type='number']::-webkit-outer-spin-button,
        &[type='number']::-webkit-inner-spin-button {
            margin: 0;
            appearance: none;
        }

        &[type='number'] {
            appearance: textfield;
        }
    }

    @include input.variant(
        $root,
        var(--ui-input-border-color, colors.$overlay-0),
        var(--ui-input-focus-border-color, colors.$cpt-mauve),
        var(--ui-input-focus-box-shadow, variables.$box-shadow rgba(colors.$cpt-mauve, 0.25))
    );

    &--invalid {
        @include input.variant(
            $root,
            var(--ui-input-invalid-border-color, colors.$error),
            var(--ui-input-invalid-focus-border-color, colors.$error),
            var(--ui-input-invalid-focus-box-shadow, variables.$box-shadow rgba(colors.$error, 0.25))
        );
    }
}
</style>
