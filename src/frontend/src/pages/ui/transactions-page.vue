<script setup lang="ts">
import { ViewTransactions } from '@/features';
import Page from '@/pages/ui/page.vue';
</script>

<template>
    <page>
        <template #default>
            <div :class="$cn()">
                <view-transactions />
            </div>
        </template>
    </page>
</template>

<style lang="scss" scoped>
@use 'src/shared/ui/spacing' as spacing;
.transactions-page {
    padding: spacing.$spacing-4;
}
</style>
