<script setup lang="ts">
import { UiColumn, UiDatatable, UiIconButton, UiSpinner } from '@/shared/ui';
import type { LazyOptions } from '@/shared/ui/ui-datatable/ui-datatable.types.ts';
import { onMounted, ref } from 'vue';
import { SaveFileApi } from '@/entities/save-file/api/save-file-api.ts';
import { type SaveFile, SaveFileState } from '@/features/view-uploads/model/types.ts';
import { Info, Pencil, Trash2 } from '@lucide/vue';
import { IconSize } from '@/shared/types/icon-size.ts';
import { useI18n } from 'vue-i18n';

const { t } = useI18n();
const rows = ref<SaveFile[]>([]);
const keyFiled: keyof SaveFile = 'id';

async function loadData(options: LazyOptions) {
    const data = await SaveFileApi.list(options.offset, options.limit);
    rows.value = [
        ...rows.value,
        ...data.saveFiles.map(sf => ({
            ...sf,
            uploadedAt: new Date(sf.uploadedAt),
        })),
    ];
}

async function removeSaveFile(id: string) {
    if (!confirm(t('Pages.Uploads.Labels.DeleteConfirmation'))) {
        return;
    }

    await SaveFileApi.remove(id);
    rows.value = rows.value.filter(sf => sf.id !== id);
}

async function editSaveFile(id: string) {
    // TODO
}

function isFailed(state: SaveFileState) {
    return state === SaveFileState.Failed || state === SaveFileState.Error;
}

onMounted(() => {
    if (!rows.value.length) {
        loadData({ offset: 0, limit: 0 });
    }
});
</script>

<template>
    <div :class="$cn()">
        <div :class="$cn('header')">
            <h1 :class="$cn('title')">{{ $t('Pages.Uploads.Labels.Title') }}</h1>
            <p :class="$cn('subtitle')">{{ $t('Pages.Uploads.Labels.Subtitle') }}</p>
        </div>
        <ui-datatable ref="datatable" :key-field="keyFiled" :row-height="32" :rows="rows" @load-more="loadData">
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
                            :class="$cn('edit')"
                            :label="$t('Pages.Uploads.Labels.Edit')"
                            @click="editSaveFile(row[keyFiled] as string)"
                        >
                            <pencil :size="IconSize.sm" />
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

.uploads {
    &__header {
        display: flex;
        flex-flow: column nowrap;
        gap: spacing.$spacing-0-5;
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

    &__edit {
        &:hover {
            --ui-icon-button-color: #{colors.$cursor};
        }
    }
}
</style>
