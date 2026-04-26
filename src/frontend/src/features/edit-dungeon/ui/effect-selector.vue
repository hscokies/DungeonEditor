<script setup lang="ts">
import { UiProperty, UiSelect } from '@/shared/ui';
import { getEnumKeys } from '@/shared/utils/enum.ts';
import { Effect } from '@/entities/dungeon/model/types.ts';
import { useI18n } from 'vue-i18n';

const { index } = defineProps<{ index: number }>();
const { t } = useI18n();

const options = getEnumKeys(Effect).map(key => ({
    label: t(`Dungeons.Effect.${key}`),
    value: Effect[key],
}));
</script>

<template>
    <ui-property
        :id="`dungeon-effect-${index}`"
        :class="$cn()"
        :label="$t('Modals.EditDungeon.Labels.Effect', { index })"
        direction="vertical"
    >
        <template #default="{ id }">
            <ui-select v-bind="$attrs" :id="id" :options="options" positioning-strategy="fixed" filter />
        </template>
    </ui-property>
</template>

<style lang="scss" scoped>
$item-width: 20em;

.effect-selector {
    --ui-popup-width: #{$item-width};

    flex-grow: 1;
    width: $item-width;
}
</style>
