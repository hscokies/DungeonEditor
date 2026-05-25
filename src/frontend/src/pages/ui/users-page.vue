<script setup lang="ts">
import Page from './page.vue';
import { ViewUsers } from '@/features';

defineOptions({
    name: 'users-page',
});
</script>

<template>
    <page>
        <template #default>
            <div :class="$cn()">
                <view-users />
            </div>
        </template>
    </page>
</template>

<style scoped lang="scss">
@use 'src/shared/ui/spacing' as spacing;
.users-page {
    padding: spacing.$spacing-4;
}
</style>
