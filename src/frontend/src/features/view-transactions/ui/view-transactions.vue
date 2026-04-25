<script setup lang="ts">
import { UiColumn, UiDatatable } from '@/shared/ui';
import { onMounted, ref } from 'vue';
import { type Transaction, TransactionType } from '@/entities/transaction/model/types.ts';
import type { LazyOptions } from '@/shared/ui/ui-datatable/ui-datatable.types.ts';
import { TransactionsApi } from '@/entities/transaction/api/transactions-api.ts';

const rows = ref<Transaction[]>([]);
const keyFiled: keyof Transaction = 'id';
const batchSize = 50;

async function loadData(options: LazyOptions) {
    const data = await TransactionsApi.list(options.offset, options.limit);
    rows.value = [...rows.value, ...data.transactions];
}

function formatAmount(amount: number, type: TransactionType) {
    return type === TransactionType.Inbound ? `+${amount}` : `-${amount}`;
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
            <h1 :class="$cn('title')">{{ $t('Pages.Transactions.Labels.Title') }}</h1>
            <p :class="$cn('subtitle')">{{ $t('Pages.Transactions.Labels.Subtitle') }}</p>
        </div>
        <ui-datatable
            ref="datatable"
            :key-field="keyFiled"
            :row-height="32"
            :rows="rows"
            @load-more="loadData"
            :max-visible-rows="batchSize"
        >
            <ui-column :header="$t('Pages.Transactions.Labels.CreatedAt')">
                <template v-slot:default="{ row }">
                    {{ (row.createdAt as Date).toLocaleString() }}
                </template>
            </ui-column>
            <ui-column :header="$t('Pages.Transactions.Labels.Description')" field="description" />
            <ui-column :header="$t('Pages.Transactions.Labels.Amount')">
                <template v-slot:default="{ row }">
                    <span :class="$cn('amount', { type: TransactionType[row.type as number] })">
                        {{ formatAmount(row.amount as number, row.type as TransactionType) }}
                    </span>
                </template>
            </ui-column>
        </ui-datatable>
    </div>
</template>

<style scoped lang="scss">
@use 'src/shared/ui/colors' as colors;

.view-transactions {
    --ui-datatable-width: 700px;

    &__amount {
        &--type-inbound {
            color: colors.$success;
        }

        &--type-outbound {
            color: colors.$error;
        }
    }
}
</style>
