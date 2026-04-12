<script setup lang="ts">
import { Login } from '@/features';
</script>

<template>
    <div :class="$cn()">
        <login />
    </div>
</template>

<style scoped lang="scss">
.login-page {
    display: grid;
    place-items: center;
    height: 100%;
}
</style>
