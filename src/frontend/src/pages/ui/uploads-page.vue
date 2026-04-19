<script setup lang="ts">
import Page from './page.vue';
import { Uploads } from '@/features';
</script>

<template>
    <page>
        <template #default>
            <div :class="$cn()">
                <uploads />
            </div>
        </template>
    </page>
</template>

<style lang="scss" scoped>
@use 'src/shared/ui/spacing' as spacing;
.uploads-page {
    padding: spacing.$spacing-4;
}
</style>
