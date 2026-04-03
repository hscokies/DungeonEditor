<script setup lang="ts"></script>

<template>
    <div :class="$cn()">
        <div :class="$cn('first-el')">
            <div :class="$cn('second-el', { active: true, number: 5, additional: 'str' })">
                {{ $t('Test') }}
            </div>
        </div>
    </div>
</template>

<style lang="scss" scoped>
@use 'src/shared/ui/colors' as colors;

.app {
    background: colors.$color-bg-page;
    height: 100vh;
    color: colors.$color-accent;
}
</style>
