<script setup lang="ts">
import Page from '@/pages/ui/page.vue';
import { ViewDungeons } from '@/features';
</script>

<template>
    <page>
        <template #default>
            <div :class="$cn()">
                <view-dungeons />
            </div>
        </template>
    </page>
</template>

<style scoped lang="scss">
@use 'src/shared/ui/spacing' as spacing;

.save-file-page {
    padding: spacing.$spacing-4;
}
</style>
