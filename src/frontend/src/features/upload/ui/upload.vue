<script setup lang="ts">
import { useI18n } from 'vue-i18n';
import { FileUp } from '@lucide/vue';
import { UiButton, UiForm } from '@/shared/ui';
import { computed, ref, useTemplateRef } from 'vue';
import { SaveFileApi } from '@/entities/save-file/api/save-file-api.ts';
import { useLock, useProblemDetails } from '@/shared/hooks';
import { router, Routes } from '@/app/router';
import type { ProblemDetails } from '@/shared/api/types.ts';
import type { Model } from '@/features/upload/model/types.ts';
import { formatSize } from '@/shared/utils/string.ts';

const MEGABYTE = 1024 * 1024;
const MAX_SIZE_BYTES_MB = 5 * MEGABYTE;

const { locked, lock, release } = useLock();

const { t } = useI18n();
const { model, error, applyProblemDetails } = useProblemDetails<Model>({
    file: {
        value: undefined,
        error: '',
    },
});
const inputRef = useTemplateRef('hiddenInput');
const dragOver = ref(false);

const displayedFile = computed(() => {
    const file = model.file.value;
    if (!file) {
        return '';
    }

    const sizeFormatted = formatSize(file.size);
    return `${file.name} (${sizeFormatted})`;
});

async function onSubmit() {
    const file = model.file.value;
    if (!file) {
        return;
    }

    lock();
    try {
        await SaveFileApi.upload(file);
        await router.push({ name: Routes.Uploads });
    } catch (err: unknown) {
        applyProblemDetails(err as ProblemDetails);
    } finally {
        release();
    }
}

function openFileDialog() {
    inputRef.value?.click();
}

function handleFile(file?: File) {
    if (!file) {
        return;
    }

    model.file.value = undefined;
    model.file.error = '';
    error.value = '';
    if (file.size > MAX_SIZE_BYTES_MB) {
        model.file.error = t('Errors.SaveFile.TooLarge');
        return;
    }

    model.file.value = file;
}

function onFileChange(event: Event) {
    const files = (event.target as HTMLInputElement).files;
    handleFile(files?.[0]);
}

function onDrop(e: DragEvent) {
    e.preventDefault();
    dragOver.value = false;
    handleFile(e.dataTransfer?.files[0]);
}

function onDragOver(e: DragEvent) {
    e.preventDefault();
    dragOver.value = true;
}

function onDragLeave() {
    dragOver.value = false;
}
</script>

<template>
    <ui-form :class="$cn()" @submit.prevent="onSubmit" :error="error">
        <div :class="$cn('hidden-input-container')">
            <input ref="hiddenInput" type="file" @change="onFileChange" />
        </div>
        <h1 :class="$cn('title')">
            {{ $t('Pages.Upload.Labels.Title') }}
        </h1>
        <div
            :class="$cn('drop-zone', { active: dragOver })"
            @dragover="onDragOver"
            @dragleave="onDragLeave"
            @drop="onDrop"
        >
            <file-up :size="48" />
            <p :class="$cn('label')">{{ $t('Pages.Upload.Labels.DragNDrop') }}</p>
            <p :class="$cn('sub')">{{ $t('Common.Or') }}</p>
            <ui-button
                :class="$cn('select-button')"
                :label="$t('Common.BrowseFiles')"
                type="button"
                @click="openFileDialog"
            />
            <p :class="$cn('footer')">
                <span v-if="model.file.value" :class="$cn('file')">
                    {{ displayedFile }}
                </span>
                <span v-else-if="model.file.error.length > 0" :class="$cn('error')">
                    {{ model.file.error }}
                </span>
            </p>
        </div>
        <ui-button
            :class="$cn('upload-button')"
            :label="$t('Pages.Upload.Labels.Upload')"
            type="submit"
            :disabled="locked || !model.file.value"
        />
    </ui-form>
</template>

<style scoped lang="scss">
@use 'src/shared/ui/colors' as colors;
@use 'src/shared/ui/border-radius' as border-radius;
@use 'src/shared/ui/typography' as typography;
@use 'src/shared/ui/spacing' as spacing;
@use 'src/shared/ui/variables' as variables;
@use 'src/shared/ui/utils' as utils;

.upload {
    display: flex;
    flex-flow: column nowrap;
    background-color: colors.$secondary-pane-1;
    border-radius: border-radius.$border-radius-lg;
    padding: spacing.$spacing-5;
    width: 30rem;

    &__hidden-input-container {
        position: absolute;
        width: 1px;
        height: 1px;
        padding: 0;
        margin: -1px;
        overflow: hidden;
        white-space: nowrap;
        pointer-events: none;
        border: 0;
        opacity: 0;
    }

    &__title {
        font-size: typography.$font-size-lg;
        color: colors.$label-0;
        margin-bottom: spacing.$spacing-3;
    }

    &__drop-zone {
        display: flex;
        flex-flow: column nowrap;
        align-items: center;
        border: 2px dashed colors.$surface-element-1;
        padding: spacing.$spacing-8;

        @include utils.transitions(background);

        &--active {
            background: colors.$overlay-2;
        }
    }

    &__label {
        margin: spacing.$spacing-1-5 0;
        color: colors.$sub-heading-1;
        font-weight: typography.$font-weight-medium;
    }

    &__sub {
        margin: 0;
        color: colors.$sub-heading-0;
    }

    &__select-button {
        margin-top: spacing.$spacing-1-5;
    }

    &__upload-button {
        align-self: flex-end;
        margin-top: spacing.$spacing-2-5;
    }

    &__footer {
        $size: typography.$font-size-md;

        margin: spacing.$spacing-2 0 0 0;
        font-size: $size;
        min-height: $size;
    }

    &__error {
        color: colors.$error;
    }
}
</style>
