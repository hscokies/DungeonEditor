<script setup lang="ts">
import { Register } from '@/features';
</script>

<template>
    <div :class="$cn()">
        <register />
    </div>
</template>

<style scoped lang="scss">
.register-page {
    display: grid;
    place-items: center;
    height: 100%;
}
</style>
