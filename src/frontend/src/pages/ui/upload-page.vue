<script setup lang="ts">
import Page from './page.vue';
import { Upload } from '@/features';
</script>

<template>
    <page>
        <template #default>
            <div :class="$cn()">
                <upload />
            </div>
        </template>
    </page>
</template>

<style scoped lang="scss">
.upload-page {
    display: grid;
    place-items: center;
    height: 100%;
}
</style>
