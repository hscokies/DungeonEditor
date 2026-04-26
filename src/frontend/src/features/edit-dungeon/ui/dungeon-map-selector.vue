<script setup lang="ts">
import { UiProperty, UiSelect } from '@/shared/ui';
import { useLock } from '@/shared/hooks';
import type { LazyOptions } from '@/shared/types/lazy-loading.ts';
import { DungeonApi } from '@/entities/dungeon/dungeon/dungeon-api.ts';
import { ref } from 'vue';
import type { DungeonMapOption } from '@/features/edit-dungeon/model/types.ts';

const { locked, lock, release } = useLock();

const maps = ref<DungeonMapOption[]>([]);

async function loadMore(options: LazyOptions) {
    if (locked.value) {
        return;
    }

    lock();
    try {
        const data = await load(options);
        maps.value = [...maps.value, ...data];
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
        maps.value = await load(options);
    } finally {
        release();
    }
}

async function load(options: LazyOptions): Promise<DungeonMapOption[]> {
    const response = await DungeonApi.getMaps(options.limit, options.offset, options.filter);
    return response.maps.map(map => ({
        label: map,
        value: map,
    }));
}
</script>

<template>
    <ui-property id="dungeon-map" :label="$t('Modals.EditDungeon.Labels.Map')" direction="vertical">
        <template #default="{ id }">
            <ui-select
                v-bind="$attrs"
                :id="id"
                :class="$cn()"
                :options="maps"
                :loading="locked"
                positioning-strategy="fixed"
                filter
                @load="loadData"
                @load-more="loadMore"
            />
        </template>
    </ui-property>
</template>
