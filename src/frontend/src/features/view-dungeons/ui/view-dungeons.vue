<script setup lang="ts">
import { useRoute } from 'vue-router';
import { computed, onMounted, ref } from 'vue';
import { type DungeonPreview, JoinRequirement } from '@/entities/dungeon/model/types.ts';
import { SaveFileApi } from '@/entities/save-file';
import { useI18n } from 'vue-i18n';
import { router, Routes } from '@/app/router';
import { UiButton, UiColumn, UiDatatable, UiIconButton } from '@/shared/ui';
import { FileCog, Pencil } from '@lucide/vue';
import { IconSize } from '@/shared/types/icon-size.ts';
import { toKebabCase } from '@/shared/utils/string.ts';
import { getFileNameWithoutExtension } from '@/shared/utils/file.ts';

const route = useRoute();
const { t } = useI18n();

const placeholderAsset = 'сhalice-placeholder';

const rows = ref<DungeonPreview[]>([]);
const keyFiled: keyof DungeonPreview = 'id';

const icons = Object.fromEntries(
    Object.keys(import.meta.glob('@/features/view-dungeons/assets/*.png', { eager: true, import: 'default' })).map(
        path => {
            const fileName = getFileNameWithoutExtension(path)!;
            return [fileName, path];
        }
    )
);

const id = computed(() => {
    const parameter = route.params.id;

    if (typeof parameter !== 'string') {
        router.replace({ name: Routes.Uploads });
        throw new Error('unable to fetch dungeons: Invalid save file ID format');
    }

    return parameter;
});

function getIcon(id: number) {
    const fileName = toKebabCase(JoinRequirement[id]!);
    return icons[fileName] || icons[placeholderAsset];
}

function getJoinRequirementName(id: number) {
    return t(`Dungeons.JoinRequirements.${JoinRequirement[id]}`);
}

async function compile() {
    try {
        await SaveFileApi.compile(id.value);
    } finally {
        await router.replace({ name: Routes.Uploads });
    }
}

function editDungeon(id: string) {
    //  todo
}

onMounted(async () => {
    try {
        const response = await SaveFileApi.get(id.value);
        rows.value = response.dungeons;
    } catch (error) {
        await router.replace({ name: Routes.Uploads });
        throw error;
    }
});
</script>

<template>
    <div :class="$cn()">
        <div :class="$cn('header')">
            <div :class="$cn('section')">
                <h1 :class="$cn('title')">{{ $t('Pages.SaveFile.Labels.Title') }}</h1>
                <p :class="$cn('subtitle')">{{ $t('Pages.SaveFile.Labels.Subtitle') }}</p>
            </div>
            <div :class="$cn('section')">
                <ui-button :class="$cn('button')" :label="$t('Pages.SaveFile.Labels.Compile')" @click="compile">
                    <template #prefix>
                        <file-cog :size="IconSize.xs" />
                    </template>
                </ui-button>
            </div>
        </div>
        <ui-datatable :key-field="keyFiled" :row-height="64" :rows="rows">
            <ui-column :header="$t('Pages.SaveFile.Labels.Dungeon')">
                <template v-slot:default="{ row }">
                    <img :class="$cn('icon')" :src="getIcon(row.joinRequirement as number)" alt="alt" />
                    {{ getJoinRequirementName(row.joinRequirement as number) }}
                </template>
            </ui-column>
            <ui-column :header="$t('Pages.SaveFile.Labels.Actions')">
                <template v-slot:default="{ row }">
                    <ui-icon-button
                        :label="$t('Pages.SaveFile.Labels.Edit')"
                        @click="editDungeon(row[keyFiled] as string)"
                    >
                        <pencil :size="IconSize.sm" />
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

.view-dungeons {
    $root: &;

    &__header {
        display: flex;
        flex-flow: row wrap;
        justify-content: space-between;
        align-items: center;
        margin-bottom: spacing.$spacing-4;

        #{$root}__section {
            display: flex;
            flex-flow: column nowrap;
            gap: spacing.$spacing-0-5;
        }
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

    &__button {
        white-space: nowrap;
    }
    &__icon {
        height: 100%;
    }
}
</style>
