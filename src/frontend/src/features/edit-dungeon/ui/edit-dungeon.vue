<script setup lang="ts">
import { computed, useTemplateRef } from 'vue';
import { UiButton, UiForm, UiInput, UiProperty } from '@/shared/ui';
import { DungeonMapSelector, EffectSelector, type Emits, JoinRequirementSelector } from '@/features/edit-dungeon';
import { JoinRequirement } from '@/entities/dungeon/model/types.ts';
import { DungeonApi } from '@/entities/dungeon/dungeon/dungeon-api.ts';
import { useLock, useProblemDetails } from '@/shared/hooks';
import type { ProblemDetails } from '@/shared/api/types.ts';
import { useI18n } from 'vue-i18n';

const MAX_AUTHOR_NAME = 16;

const { locked, lock, release } = useLock();
const { t } = useI18n();
const { model, error, applyProblemDetails } = useProblemDetails({
    map: {
        value: '',
        error: '',
    },
    joinRequirement: {
        value: JoinRequirement.Pthumeru,
        error: '',
    },
    effect1: {
        value: 0xff,
        error: '',
    },
    effect2: {
        value: 0xff,
        error: '',
    },
    effect3: {
        value: 0xff,
        error: '',
    },
    effect4: {
        value: 0xff,
        error: '',
    },
    effect5: {
        value: 0xff,
        error: '',
    },
    effect6: {
        value: 0xff,
        error: '',
    },
    effect7: {
        value: 0xff,
        error: '',
    },
    effect8: {
        value: 0xff,
        error: '',
    },
    effect9: {
        value: 0xff,
        error: '',
    },
    authorPSN: {
        value: '',
        error: '',
    },
    authorCharacter: {
        value: '',
        error: '',
    },
});

const dialogRef = useTemplateRef('dialog');
const emit = defineEmits<Emits>();

const hasErrors = computed(() => Object.values(model).some(p => p.error.length > 0));

let dungeonId: string;

async function loadDungeon(id: string) {
    const dungeon = await DungeonApi.get(id);
    dungeonId = id;
    model.map.value = dungeon.map;
    model.joinRequirement.value = dungeon.joinRequirement;
    model.effect1.value = dungeon.effect1;
    model.effect2.value = dungeon.effect2;
    model.effect3.value = dungeon.effect3;
    model.effect4.value = dungeon.effect4;
    model.effect5.value = dungeon.effect5;
    model.effect6.value = dungeon.effect6;
    model.effect7.value = dungeon.effect7;
    model.effect8.value = dungeon.effect8;
    model.effect9.value = dungeon.effect9;
    model.authorPSN.value = dungeon.authorPSN;
    model.authorCharacter.value = dungeon.authorCharacter;
}

function open(id: string) {
    loadDungeon(id);
    dialogRef.value?.showModal();
}

function close() {
    dialogRef.value?.close();
}

async function onSubmit() {
    if (locked.value || hasErrors.value) {
        return;
    }

    lock();
    try {
        error.value = '';
        const request = {
            map: model.map.value,
            joinRequirement: model.joinRequirement.value,
            effect1: model.effect1.value,
            effect2: model.effect2.value,
            effect3: model.effect3.value,
            effect4: model.effect4.value,
            effect5: model.effect5.value,
            effect6: model.effect6.value,
            effect7: model.effect7.value,
            effect8: model.effect8.value,
            effect9: model.effect9.value,
            authorPSN: model.authorPSN.value,
            authorCharacter: model.authorCharacter.value,
        };

        await DungeonApi.edit(dungeonId, request);
        emit('update');
        close();
    } catch (error) {
        applyProblemDetails(error as ProblemDetails);
    } finally {
        release();
    }
}

function validateAuthor(property: keyof typeof model) {
    const value = model[property].value as string;
    const byteSizeUtf8 = new TextEncoder().encode(value).length;
    if (byteSizeUtf8 > MAX_AUTHOR_NAME) {
        model[property].error = t('Errors.Dungeon.AuthorNameTooLong');
    } else {
        model[property].error = '';
    }
}

defineExpose({
    open,
});
</script>

<template>
    <dialog ref="dialog" :class="$cn()" @cancel.prevent>
        <h1 :class="$cn('title')">
            {{ $t('Modals.EditDungeon.Labels.Title') }}
        </h1>
        <p :class="$cn('subtitle')">
            {{ $t('Modals.EditDungeon.Labels.Subtitle') }}
        </p>

        <ui-form :class="$cn('form')" :error="error" @submit.prevent="onSubmit">
            <dungeon-map-selector v-model="model.map.value" />
            <join-requirement-selector v-model="model.joinRequirement.value" />
            <fieldset :class="$cn('group')">
                <legend :class="$cn('group-title')">
                    {{ $t('Modals.EditDungeon.Labels.Effects') }}
                </legend>
                <effect-selector v-model="model.effect1.value" :index="1" />
                <effect-selector v-model="model.effect2.value" :index="2" />
                <effect-selector v-model="model.effect3.value" :index="3" />
                <effect-selector v-model="model.effect4.value" :index="4" />
                <effect-selector v-model="model.effect5.value" :index="5" />
                <effect-selector v-model="model.effect6.value" :index="6" />
                <effect-selector v-model="model.effect7.value" :index="7" />
                <effect-selector v-model="model.effect8.value" :index="8" />
                <effect-selector v-model="model.effect9.value" :index="9" />
            </fieldset>
            <fieldset :class="$cn('group')">
                <legend :class="$cn('group-title')">
                    {{ $t('Modals.EditDungeon.Labels.Author') }}
                </legend>

                <ui-property
                    :class="$cn('author')"
                    id="author-character"
                    :label="$t('Modals.EditDungeon.Labels.Character')"
                    direction="vertical"
                    :error="model.authorCharacter.error"
                >
                    <template #default="{ id, invalid }">
                        <ui-input
                            v-model="model.authorCharacter.value"
                            :id="id"
                            :invalid="invalid"
                            :maxlength="MAX_AUTHOR_NAME"
                            @blur="validateAuthor('authorCharacter')"
                        />
                    </template>
                </ui-property>

                <ui-property
                    :class="$cn('author')"
                    id="author-psn"
                    :label="$t('Modals.EditDungeon.Labels.PSN')"
                    direction="vertical"
                    :error="model.authorPSN.error"
                >
                    <template #default="{ id, invalid }">
                        <ui-input
                            v-model="model.authorPSN.value"
                            :id="id"
                            :invalid="invalid"
                            :maxlength="MAX_AUTHOR_NAME"
                            @blur="validateAuthor('authorPSN')"
                        />
                    </template>
                </ui-property>
            </fieldset>

            <div :class="$cn('footer')">
                <ui-button :label="$t('Modals.EditDungeon.Labels.Apply')" type="submit" />
                <ui-button
                    :class="$cn('cancel')"
                    :label="$t('Modals.EditDungeon.Labels.Cancel')"
                    type="button"
                    @click="close"
                />
            </div>
        </ui-form>
    </dialog>
</template>

<style scoped lang="scss">
@use 'sass:color';
@use 'src/shared/ui/colors' as colors;
@use 'src/shared/ui/border-radius' as border-radius;
@use 'src/shared/ui/spacing' as spacing;
@use 'src/shared/ui/typography' as typography;
@use 'src/shared/ui/variables' as variables;

$property-height: 70px;

.edit-dungeon {
    --ui-property-vertical-label-spacing: spacing.$spacing-1;

    color: colors.$body-copy;
    background: lighten(colors.$background-pane, 5);
    border: 1px solid transparent;
    border-radius: border-radius.$border-radius-md;
    width: 50em;
    max-height: 80vh;

    @media (max-width: 769px) {
        margin: 0;
        width: 100%;
        max-width: none;
        height: 100%;
        max-height: none;
        padding: spacing.$spacing-3-5;
    }

    &__title,
    &__subtitle {
        margin: 0;
    }

    &__title {
        font-size: typography.$font-size-xl;
        font-weight: typography.$font-weight-bold;
    }

    &__subtitle {
        font-size: typography.$font-size-lg;
        font-weight: typography.$font-weight-semibold;
    }

    &__form {
        margin-top: spacing.$spacing-4;
    }

    &__group {
        display: flex;
        flex-flow: row wrap;
        justify-content: space-between;
        gap: spacing.$spacing-2;
        border: variables.$border colors.$overlay-1;
    }

    &__author {
        flex-grow: 1;
        width: 20em;
    }

    &__footer {
        display: inline-flex;
        gap: spacing.$spacing-2;
        justify-content: flex-end;
    }

    &__cancel {
        --ui-button-hover-color: #{colors.$on-accent};
        --ui-button-hover-background: #{colors.$error};
    }
}
</style>
