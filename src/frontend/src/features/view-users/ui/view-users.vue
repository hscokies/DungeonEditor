<script setup lang="ts">
import { onMounted, ref } from 'vue';
import { SaveIcon as Save } from '@lucide/vue';
import { UiColumn, UiDatatable, UiIconButton, UiInput } from '@/shared/ui';
import type { LazyOptions } from '@/shared/ui/ui-datatable/ui-datatable.types.ts';
import type { User } from '@/entities/account/model/types.ts';
import { AccountApi } from '@/entities/account/api/account-api.ts';
import { IconSize } from '@/shared/types/icon-size.ts';
import { useI18n } from 'vue-i18n';

const { t } = useI18n();

const rows = ref<User[]>([]);
const keyFiled: keyof User = 'id';
const batchSize = 50;

async function loadData(options: LazyOptions) {
    const data = await AccountApi.list(options.offset, options.limit);
    rows.value = [...rows.value, ...data.users];
}

function applyChanges(row: User) {
    AccountApi.setBalance(row.id, row.balance);
}

onMounted(() => {
    if (!rows.value.length) {
        loadData({ offset: 0, limit: batchSize });
    }
});
</script>

<template>
    <div :class="$cn()">
        <div :class="$cn('header')">
            <h1 :class="$cn('title')">{{ $t('Pages.Users.Labels.Title') }}</h1>
            <p :class="$cn('subtitle')">{{ $t('Pages.Users.Labels.Subtitle') }}</p>
        </div>
        <ui-datatable
            ref="datatable"
            :key-field="keyFiled"
            :row-height="32"
            :rows="rows"
            @load-more="loadData"
            :max-visible-rows="batchSize"
        >
            <ui-column :header="$t('Pages.Users.Labels.Username')" field="username" />
            <ui-column :header="$t('Pages.Users.Labels.Balance')">
                <template v-slot:default="{ row }">
                    <ui-input
                        :class="$cn('input')"
                        v-model="row.balance"
                        type="number"
                        :placeholder="$t('Pages.Users.Labels.Balance')"
                    />
                </template>
            </ui-column>
            <ui-column :header="$t('Pages.Users.Labels.Actions')">
                <template v-slot:default="{ row }">
                    <ui-icon-button
                        :class="$cn('action')"
                        :label="$t('Common.Apply')"
                        @click="applyChanges(row as unknown as User)"
                    >
                        <save :sze="IconSize.sm" />
                    </ui-icon-button>
                </template>
            </ui-column>
        </ui-datatable>
    </div>
</template>

<style scoped lang="scss">
@use 'src/shared/ui/colors' as colors;

.view-users {
    &__input {
        --ui-input-background: transparent;
        --ui-input-border: none;
        --ui-input-focus-border-color: transparent;
        --ui-input-border-color: transparent;
        --ui-input-focus-box-shadow: none;

        :deep(> .ui-input__field) {
            box-shadow: none;
            border: none;
        }

        width: 100%;
    }

    &__action:hover {
        --ui-icon-button-color: #{colors.$cpt-peach};
    }
}
</style>
