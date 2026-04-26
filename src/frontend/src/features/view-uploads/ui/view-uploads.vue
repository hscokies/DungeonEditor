<script setup lang="ts">
import { UiColumn, UiDatatable, UiIconButton, UiSpinner } from '@/shared/ui';
import type { LazyOptions } from '@/shared/ui/ui-datatable/ui-datatable.types.ts';
import { ref } from 'vue';
import { type SaveFile, SaveFileApi, SaveFileState } from '@/entities/save-file';
import { FileDown, Info, Pencil, Trash2 } from '@lucide/vue';
import { IconSize } from '@/shared/types/icon-size.ts';
import { useI18n } from 'vue-i18n';
import { router, Routes } from '@/app/router';
import { useLock } from '@/shared/hooks';

const { t } = useI18n();
const { locked, lock, release } = useLock();
const rows = ref<SaveFile[]>([]);
const keyFiled: keyof SaveFile = 'id';
const batchSize = 50;

async function loadMore(options: LazyOptions) {
    if (locked.value) {
        return;
    }

    lock();
    try {
        const data = await load(options);
        rows.value = [...rows.value, ...data.saveFiles];
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
        rows.value = data.saveFiles;
    } finally {
        release();
    }
}

function load(options: LazyOptions) {
    return SaveFileApi.list(options.offset, options.limit, options.filter);
}

async function removeSaveFile(id: string) {
    if (!confirm(t('Pages.Uploads.Labels.DeleteConfirmation'))) {
        return;
    }

    await SaveFileApi.remove(id);
    rows.value = rows.value.filter(sf => sf.id !== id);
}

async function editSaveFile(id: string) {
    await router.push({ name: Routes.SaveFile, params: { id } });
}

async function download(id: string) {
    await SaveFileApi.download(id);
}

function isFailed(state: SaveFileState) {
    return state === SaveFileState.Failed || state === SaveFileState.Error;
}
</script>

<template>
    <div :class="$cn()">
        <div :class="$cn('header')">
            <h1 :class="$cn('title')">{{ $t('Pages.Uploads.Labels.Title') }}</h1>
            <p :class="$cn('subtitle')">{{ $t('Pages.Uploads.Labels.Subtitle') }}</p>
        </div>
        <ui-datatable
            ref="datatable"
            :key-field="keyFiled"
            :row-height="32"
            :rows="rows"
            filter
            :filter-placeholder="$t('Pages.Uploads.Placeholders.Search')"
            :max-visible-rows="batchSize"
            :loading="locked"
            @load-more="loadMore"
            @load="loadData"
        >
            <ui-column field="fileName" :header="$t('Pages.Uploads.Labels.FileName')" />
            <ui-column :header="$t('Pages.Uploads.Labels.UploadedAt')">
                <template v-slot:default="{ row }">
                    {{ (row.uploadedAt as Date).toLocaleString() }}
                </template>
            </ui-column>
            <ui-column :header="$t('Pages.Uploads.Labels.State')">
                <template v-slot:default="{ row }">
                    {{ SaveFileState[row.state as number] }}
                </template>
            </ui-column>
            <ui-column :header="$t('Pages.Uploads.Labels.Actions')">
                <template v-slot:default="{ row }">
                    <ui-spinner v-if="(row.state as number) <= SaveFileState.Processing" :size="IconSize.sm" />
                    <div v-else :class="$cn('actions')">
                        <ui-icon-button
                            v-if="row.state === SaveFileState.Processed"
                            :class="$cn('action')"
                            :label="$t('Pages.Uploads.Labels.Edit')"
                            @click="editSaveFile(row[keyFiled] as string)"
                        >
                            <pencil :size="IconSize.sm" />
                        </ui-icon-button>
                        <ui-icon-button
                            v-if="row.state === SaveFileState.Processed"
                            :class="$cn('action')"
                            :label="$t('Pages.Uploads.Labels.Download')"
                            @click="download(row[keyFiled] as string)"
                        >
                            <file-down :size="IconSize.sm" />
                        </ui-icon-button>
                        <ui-icon-button
                            v-if="row.state === SaveFileState.Processed"
                            :class="$cn('delete')"
                            :label="$t('Pages.Uploads.Labels.Delete')"
                            @click="removeSaveFile(row[keyFiled] as string)"
                        >
                            <trash2 :size="IconSize.sm" />
                        </ui-icon-button>
                        <ui-icon-button
                            v-if="isFailed(row.state as number)"
                            :label="$t(`Pages.Uploads.Labels.Info.${SaveFileState[row.state as number]}`)"
                        >
                            <info :size="IconSize.sm" />
                        </ui-icon-button>
                    </div>
                </template>
            </ui-column>
        </ui-datatable>
    </div>
</template>

<style scoped lang="scss">
@use 'src/shared/ui/colors' as colors;
@use 'src/shared/ui/typography' as typography;
@use 'src/shared/ui/spacing' as spacing;

.view-uploads {
    --ui-datatable-width: 900px;

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

    &__actions {
        display: inline-flex;
        gap: spacing.$spacing-1-5;
    }
    &__delete {
        &:hover {
            --ui-icon-button-color: #{colors.$error};
        }
    }

    &__action {
        &:hover {
            --ui-icon-button-color: #{colors.$cursor};
        }
    }
}
</style>
