<script setup lang="ts">
import { ref } from 'vue';
import { SaveIcon as Save } from '@lucide/vue';
import { UiColumn, UiDatatable, UiIconButton, UiInput } from '@/shared/ui';
import type { LazyOptions } from '@/shared/ui/ui-datatable/ui-datatable.types.ts';
import type { User } from '@/entities/account/model/types.ts';
import { AccountApi } from '@/entities/account/api/account-api.ts';
import { IconSize } from '@/shared/types/icon-size.ts';
import { useLock } from '@/shared/hooks';

const { locked, lock, release } = useLock();

const rows = ref<User[]>([]);
const keyFiled: keyof User = 'id';
const batchSize = 50;

async function loadMore(options: LazyOptions) {
    if (locked.value) {
        return;
    }

    lock();
    try {
        const data = await load(options);
        rows.value = [...rows.value, ...data.users];
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
        rows.value = data.users;
    } finally {
        release();
    }
}

function load(options: LazyOptions) {
    return AccountApi.list(options.offset, options.limit, options.filter);
}

function applyChanges(row: User) {
    AccountApi.setBalance(row.id, row.balance);
}
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
            filter
            :filter-placeholder="$t('Pages.Users.Placeholders.Search')"
            :max-visible-rows="batchSize"
            :loading="locked"
            @load-more="loadMore"
            @load="loadData"
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
@use 'src/shared/ui/spacing' as spacing;
@use 'src/shared/ui/typography' as typography;

.view-users {
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
