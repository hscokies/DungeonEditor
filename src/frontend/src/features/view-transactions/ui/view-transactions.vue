<script setup lang="ts">
import { UiColumn, UiDatatable } from '@/shared/ui';
import { ref } from 'vue';
import { type Transaction, TransactionType } from '@/entities/transaction/model/types.ts';
import { TransactionsApi } from '@/entities/transaction/api/transactions-api.ts';
import { useLock } from '@/shared/hooks';
import type { LazyOptions } from '@/shared/types/lazy-loading.ts';

const { locked, lock, release } = useLock();

const rows = ref<Transaction[]>([]);
const keyFiled: keyof Transaction = 'id';
const batchSize = 50;

async function loadMore(options: LazyOptions) {
    if (locked.value) {
        return;
    }

    lock();
    try {
        const data = await load(options);
        rows.value = [...rows.value, ...data.transactions];
    } finally {
        release();
    }
}

async function loadData(options: LazyOptions) {
    if (locked.value) {
        return;
    }

    lock();
    try {
        const data = await load(options);
        rows.value = data.transactions;
    } finally {
        release();
    }
}

function load(options: LazyOptions) {
    return TransactionsApi.list(options.offset, options.limit);
}

function formatAmount(amount: number, type: TransactionType) {
    return type === TransactionType.Inbound ? `+${amount}` : `-${amount}`;
}
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
            :max-visible-rows="batchSize"
            :loading="locked"
            @load-more="loadMore"
            @load="loadData"
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
@use 'src/shared/ui/spacing' as spacing;
@use 'src/shared/ui/typography' as typography;

.view-transactions {
    --ui-datatable-width: 700px;

    &__header {
        display: flex;
        flex-flow: column nowrap;
        gap: spacing.$spacing-0-5;
        margin-bottom: spacing.$spacing-4;
    }

    &__title {
        color: colors.$heading;
        font-size: typography.$font-size-xl;
        margin: 0;
    }

    &__subtitle {
        color: colors.$sub-heading-0;
        font-size: typography.$font-size-lg;
        margin: 0;
    }

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
