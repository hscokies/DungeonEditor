<script setup lang="ts">
import type { PropTypes } from '@/widgets/balance/model/types.ts';
import { IconSize } from '@/shared/types/icon-size.ts';
import { Coins } from '@lucide/vue';
import { UiSpinner } from '@/shared/ui';
import { computed } from 'vue';
import { useI18n } from 'vue-i18n';

const { balance, label = false } = defineProps<PropTypes>();
const { t } = useI18n();

const text = computed(() => {
    if (label) {
        return `${t('Common.Balance')}: ${balance}`;
    }

    return balance;
});
</script>

<template>
    <div :class="$cn()">
        <span v-if="balance">{{ text }}</span>
        <ui-spinner v-else :size="IconSize.xss" :thickness="2" />
        <coins :size="IconSize.xss" />
    </div>
</template>

<style scoped lang="scss">
@use 'src/shared/ui/colors' as colors;
@use 'src/shared/ui/spacing' as spacing;
@use 'src/shared/ui/border-radius' as border-radius;

.balance {
    color: colors.$cpt-peach;
    display: inline-flex;
    align-items: center;
    gap: spacing.$spacing-1-5;
    border-radius: border-radius.$border-radius-md;
    user-select: none;
    pointer-events: none;
}
</style>
