<script setup lang="ts">
import { UiProperty, UiSelect } from '@/shared/ui';
import { getEnumKeys } from '@/shared/utils/enum.ts';
import { JoinRequirement } from '@/entities/dungeon/model/types.ts';
import { useI18n } from 'vue-i18n';

const { t } = useI18n();

const options = getEnumKeys(JoinRequirement).map(key => ({
    label: t(`Dungeons.JoinRequirements.${key}`),
    value: JoinRequirement[key],
}));
</script>

<template>
    <ui-property id="join-requirement" :label="$t('Modals.EditDungeon.Labels.JoinRequirement')" direction="vertical">
        <template #default="{ id }">
            <ui-select v-bind="$attrs" :id="id" :class="$cn()" :options="options" positioning-strategy="fixed" filter />
        </template>
    </ui-property>
</template>
