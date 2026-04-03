<script setup lang="ts">
import { useCustomFocus } from '@/shared/hooks';
import { ref } from 'vue';

const { focused, onFocus, onBlur } = useCustomFocus();
const props = defineProps({
    value: String,
    type: {
        type: String,
        default: 'text',
    },
    placeholder: {
        type: String,
        required: false,
    },
    disabled: {
        type: Boolean,
        default: false,
    },
    invalid: {
        type: Boolean,
        default: false,
    },
    readonly: {
        type: Boolean,
        default: false,
    },
});

const fieldRef = ref<HTMLInputElement | null>(null);

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
            :type="props.type"
            :class="$cn('field')"
            :disabled="props.disabled"
            :readonly="props.readonly"
            :placeholder="props.placeholder"
        />
        <div v-if="$slots.suffix" :class="$cn('suffix')">
            <slot name="suffix" />
        </div>
    </div>
</template>

<style lang="scss" scoped>
@use '.' as styles;
@use 'src/shared/ui/border-radius' as border-radius;
@use 'src/shared/ui/typography' as typography;
@use 'src/shared/ui/spacing' as spacing;
@use 'src/shared/ui/colors' as colors;
@use 'src/shared/ui/utils' as utils;
@use 'src/shared/ui/variables' as variables;

.ui-input {
    $root: &;

    display: inline-flex;
    align-items: stretch;
    height: var(--ui-input-height, variables.$input-height);
    background: var(--ui-input-background, colors.$surface-element-0);
    border: var(--ui-input-border, variables.$border);
    color: var(--ui-input-text-color, colors.$label-1);
    border-radius: var(--ui-input-border-radius, border-radius.$border-radius-md);
    font-size: var(--ui-input-font-size);

    @include utils.transitions(background-color, border-color, box-shadow);

    &__suffix,
    &__prefix {
        display: grid;
        place-items: center;
        height: 100%;
    }

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

    @include styles.variant(
        $root,
        var(--ui-input-border-color, colors.$overlay-0),
        var(--ui-input-focus-border-color, colors.$cpt-mauve),
        var(--ui-input-focus-box-shadow, variables.$box-shadow rgba(colors.$cpt-mauve, 0.25))
    );

    &--invalid {
        @include styles.variant(
            $root,
            var(--ui-input-invalid-border-color, colors.$error),
            var(--ui-input-invalid-focus-border-color, colors.$error),
            var(--ui-input-invalid-focus-box-shadow, variables.$box-shadow rgba(colors.$error, 0.25))
        );
    }

    &--readonly {
        background: var(--ui-input-readonly-background, colors.$surface-element-2);
    }

    &--disabled {
        pointer-events: none;
        opacity: variables.$disabled-opacity;
    }
}
</style>
