<script setup lang="ts">
import type { PropTypes } from './ui-form.types.ts';

const { error } = defineProps<PropTypes>();
</script>
<template>
    <form :class="$cn()" v-bind="$attrs">
        <div v-if="error?.length" :class="$cn('error')">
            {{ error }}
        </div>
        <fieldset :class="$cn('fields')">
            <slot />
        </fieldset>
    </form>
</template>

<style scoped lang="scss">
@use 'src/shared/ui/variables' as variables;
@use 'src/shared/ui/colors' as colors;
@use 'src/shared/ui/spacing' as spacing;
@use 'src/shared/ui/border-radius' as border-radius;

.ui-form {
    display: flex;
    flex-flow: column nowrap;
    gap: spacing.$spacing-3;
    width: 100%;

    &__error {
        background: rgba(colors.$error, 0.12);
        border: variables.$border rgba(colors.$error, 0.4);
        border-radius: border-radius.$border-radius-sm;
        padding: spacing.$spacing-2-5 spacing.$spacing-2;
        text-align: center;
    }

    &__fields {
        display: flex;
        flex-flow: column nowrap;
        gap: spacing.$spacing-2-5;
        border: none;
        outline: none;
        padding: spacing.$spacing-0;
        margin: spacing.$spacing-0;
    }
}
</style>
