<script setup lang="ts">
import { ArrowUp } from '@lucide/vue';
import { useScroll } from '@/shared/hooks';
import { computed, useTemplateRef } from 'vue';
import { UiIconButton } from '@/shared/ui';
import { IconSize } from '@/shared/types/icon-size.ts';
import { Menu } from '@/widgets';

const contentRef = useTemplateRef('content');
const { y } = useScroll(contentRef);
const contentScrolled = computed(() => y.value > 0);

function scrollTop() {
    contentRef.value?.scrollTo({ top: 0, behavior: 'smooth' });
}
</script>

<template>
    <div :class="$cn()">
        <Menu />
        <div :class="$cn('content')" ref="content">
            <ui-icon-button
                v-show="contentScrolled"
                :class="$cn('scroll-top')"
                :label="$t('Common.ScrollTop')"
                @click="scrollTop"
            >
                <arrow-up :size="IconSize.md" />
            </ui-icon-button>
            <slot />
        </div>
    </div>
</template>

<style scoped lang="scss">
@use 'src/shared/ui/colors' as colors;
@use 'src/shared/ui/spacing' as spacing;
@use 'src/shared/ui/utils' as utils;
@use 'src/shared/ui/border-radius' as border-radius;
@use 'src/shared/ui/z-index' as z-index;

.page {
    display: flex;
    flex-flow: column nowrap;
    height: 100%;

    &__content {
        height: 100%;
        overflow-y: auto;
    }

    &__scroll-top {
        position: absolute;
        right: 1em;
        bottom: 1.5em;
        background: colors.$surface-element-2;
        padding: spacing.$spacing-2;
        border-radius: border-radius.$border-radius-full;
        z-index: z-index.$scroll-top-button;

        @include utils.transitions(background);

        &:hover {
            background: colors.$surface-element-1;
        }
    }
}
</style>
