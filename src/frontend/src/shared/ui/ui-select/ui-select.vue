<script setup lang="ts" generic="T">
import 'vue-virtual-scroller/dist/vue-virtual-scroller.css';

import {
    type OptionItem,
    SelectItemType,
    useCustomFocus,
    useDebounce,
    useKeyboardNavigation,
    useSelectItems,
} from '@/shared/hooks';
import type { Emits, PropTypes } from './ui-select.types.ts';
import { UiIconButton, UiInput, UiPopup, UiSpinner } from '@/shared/ui';
import { computed, ref, useTemplateRef } from 'vue';
import { ChevronDown, Search } from '@lucide/vue';
import { RecycleScroller } from 'vue-virtual-scroller';
import UiSelectItem from '@/shared/ui/ui-select/ui-select-item.vue';

const {
    id,
    placeholder,
    tabIndex,
    options,
    optionValue = 'value',
    optionLabel = 'label',
    optionGroupLabel,
    optionGroupChildren,
    disabled = false,
    invalid = false,
    loading = false,
    filter = false,
    filterPlaceholder = undefined,
    itemHeight = 39,
    maxVisibleItems = 5,
} = defineProps<PropTypes>();
const emit = defineEmits<Emits<T>>();

const headerRef = useTemplateRef('header');
const popupRef = useTemplateRef('popup');
const hiddenInputRef = useTemplateRef('hiddenInput');
const scrollerRef = useTemplateRef('scroller');

const { focused, onFocus, onBlur } = useCustomFocus();
const { focusedIndex, onKeyDown } = useKeyboardNavigation(
    popupRef,
    findNearestOption,
    moveFocus,
    selectByIndex,
    !filter
);
const { filterValue, optionItems, filteredItems } = useSelectItems<T>(
    options,
    optionLabel,
    optionValue,
    optionGroupLabel,
    optionGroupChildren
);

const model = defineModel<T>();
const label = ref<string>();
const selected = ref<string>();

const scrollerHeight = computed(() => maxVisibleItems * itemHeight);
const scrollerStyles = computed(() => ({
    maxHeight: `${scrollerHeight.value}px`,
}));

const visibleItems = computed(() =>
    filteredItems.value?.map((item, index) => {
        const group = item.type === SelectItemType.Group;
        const emptyCallback = () => {};

        return {
            header: group,
            renderKey: item.renderKey,
            label: item.label,
            onSelect: group ? emptyCallback : () => onSelect(item),
            onHover: group ? emptyCallback : () => (focusedIndex.value = index),
        };
    })
);

function findNearestOption(index: number, forward: boolean) {
    const options = visibleItems.value;

    const step = forward ? 1 : -1;
    index %= options.length;

    for (const _ of options) {
        if (index < 0) {
            index = options.length - 1;
        }

        const item = options[index];
        if (item?.header === false) {
            return index;
        }

        index = (index + step) % options.length;
    }

    return -1;
}

function moveFocus(index: number) {
    scrollToItem(index);
    focusedIndex.value = index;
}

function scrollToItem(index: number) {
    const scroller = scrollerRef.value;
    if (!scroller) {
        return;
    }

    const itemTopBorder = itemHeight * index;
    const itemBottomBorder = itemTopBorder + itemHeight;

    const scrollerTopOffset = scroller.el!.scrollTop;
    const scrollerBottomOffset = scrollerTopOffset + scrollerHeight.value;

    if (itemVisible(itemTopBorder, itemBottomBorder, scrollerTopOffset, scrollerBottomOffset)) {
        return;
    }

    const relativeIndex = focusedIndex.value === index ? index : Math.abs(focusedIndex.value - index);
    if (itemTopBorder < scrollerTopOffset) {
        const offset = scrollerTopOffset - itemHeight * relativeIndex;
        scroller.scrollToPosition(offset);
    } else {
        const offset = scrollerTopOffset + itemHeight * relativeIndex;
        scroller.scrollToPosition(offset);
    }
}

function selectByIndex(index: number) {
    visibleItems.value[index]?.onSelect();
}

function onHeaderClick() {
    focusHiddenInput();
    popupRef.value?.toggle();
}

function onSelect(option: OptionItem<T>, renderKey: string) {
    model.value = option.value;
    label.value = option.label;
    selected.value = option.renderKey;

    popupRef.value?.hide();
}

const onFilterChange = useDebounce((query: string) => {
    filterValue.value = query;
}, 350);

function focusHiddenInput() {
    hiddenInputRef.value?.focus();
}

function itemVisible(
    itemTopBorder: number,
    itemBottomBorder: number,
    scrollerTopOffset: number,
    scrollerBottomOffset: number
) {
    return itemTopBorder >= scrollerTopOffset && itemBottomBorder <= scrollerBottomOffset;
}
</script>

<template>
    <ui-popup ref="popup" :reference="() => headerRef" placement="bottom-start" strategy="absolute">
        <template #default="{ triggerClassName, active }">
            <div :class="$cn('hidden-input-container')">
                <input
                    :id="id"
                    ref="hiddenInput"
                    :class="triggerClassName"
                    type="text"
                    readonly
                    :disabled="disabled"
                    :placeholder="placeholder"
                    :tabindex="!disabled ? tabIndex : -1"
                    :aria-expanded="active"
                    role="combobox"
                    @focus="onFocus"
                    @blur="onBlur"
                    @keydown="onKeyDown"
                />
            </div>

            <div ref="header" :class="$cn('header', { focused, disabled, invalid })" @click="onHeaderClick">
                <div v-if="$slots.prefix" :class="$cn('prefix')">
                    <slot name="prefix" />
                </div>
                <div :class="$cn('label-container')">
                    <span v-if="label" :class="$cn('label')">
                        {{ label }}
                    </span>
                    <span v-else :class="$cn('placeholder')">
                        {{ placeholder }}
                    </span>
                </div>
                <div :class="$cn('suffix')">
                    <slot v-if="loading" name="loading">
                        <ui-spinner :size="14" :thickness="1" />
                    </slot>
                    <slot v-else name="chevron">
                        <ui-icon-button
                            :class="$cn('action-button')"
                            :label="$t('Accessibility.ToggleDropdown')"
                            :tabindex="-1"
                        >
                            <chevron-down :size="16" />
                        </ui-icon-button>
                    </slot>
                </div>
            </div>
        </template>
        <template #popup>
            <div :class="$cn('overlay')">
                <ui-input
                    v-if="filter"
                    :class="$cn('filter')"
                    :placeholder="filterPlaceholder"
                    @input="onFilterChange($event.target.value)"
                    @keydown.tab.prevent="focusHiddenInput"
                >
                    <template #suffix>
                        <slot name="searchIcon">
                            <search :size="14" />
                        </slot>
                    </template>
                </ui-input>
                <recycle-scroller
                    ref="scroller"
                    :style="scrollerStyles"
                    :item-size="itemHeight"
                    key-field="renderKey"
                    list-tag="ul"
                    item-tag="li"
                    :items="visibleItems"
                >
                    <template #empty>
                        <div :class="$cn('no-data')">
                            {{ $t('Common.NoData') }}
                        </div>
                    </template>
                    <template #default="{ item, index }">
                        <ui-select-item
                            :key="item.renderKey"
                            :selected="item.renderKey === selected"
                            role="menuitemradio"
                            :header="item.header"
                            :focused="index === focusedIndex"
                            :height="itemHeight"
                            :label="item.label"
                            @click.prevent="item.onSelect"
                            @mousemove="item.onHover"
                        />
                    </template>
                </recycle-scroller>
            </div>
        </template>
    </ui-popup>
</template>

<style scoped lang="scss">
@use 'src/shared/ui/input' as input;

@use 'src/shared/ui/border-radius' as border-radius;
@use 'src/shared/ui/typography' as typography;
@use 'src/shared/ui/spacing' as spacing;
@use 'src/shared/ui/colors' as colors;
@use 'src/shared/ui/utils' as utils;
@use 'src/shared/ui/variables' as variables;

$box-shadow: 0 8px 16px 0 rgba(colors.$overlay-0, 0.35);

.ui-select {
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

    &__header {
        $root: &;

        cursor: pointer;
        column-gap: spacing.$spacing-2;
        padding: var(--ui-input-field-padding, spacing.$spacing-2 spacing.$spacing-1-5);
        width: 100%;
        user-select: none;
        @include input.style();

        @include input.variant(
            $root,
            var(--ui-input-border-color, colors.$overlay-0),
            var(--ui-input-focus-border-color, colors.$cpt-mauve),
            var(--ui-input-focus-box-shadow, variables.$box-shadow rgba(colors.$cpt-mauve, 0.25))
        );

        &--invalid {
            @include input.variant(
                $root,
                var(--ui-input-invalid-border-color, colors.$error),
                var(--ui-input-invalid-focus-border-color, colors.$error),
                var(--ui-input-invalid-focus-box-shadow, variables.$box-shadow rgba(colors.$error, 0.25))
            );
        }
    }

    &__action-button {
        min-height: 0;
    }

    &__suffix,
    &__prefix {
        display: grid;
        place-items: center;
        height: 100%;
    }

    &__label-container {
        display: flex;
        align-items: center;
        width: 100%;
        overflow: hidden;
        white-space: nowrap;
        scrollbar-width: none;
    }

    &__label,
    &__placeholder {
        overflow-x: hidden;
        text-overflow: ellipsis;
    }

    &__placeholder {
        color: var(--ui-input-placeholder-color, rgba(colors.$label-1, 0.5));
    }

    &__overlay {
        display: flex;
        flex-flow: column nowrap;
        width: 100%;
        background: colors.$overlay-0;
        border: variables.$border colors.$cpt-overlay-1;
        border-radius: border-radius.$border-radius-sm;
        box-shadow: $box-shadow;
    }

    &__filter {
        --ui-input-background: #{colors.$surface-element-1};
        --ui-input-affix-aspect-ratio: 1/1;
        --ui-input-field-padding: 0 #{spacing.$spacing-1};
        margin: spacing.$spacing-1-5 spacing.$spacing-1;
    }

    &__scroller {
        padding: spacing.$spacing-0;
        margin: spacing.$spacing-0;
    }

    &__no-data {
        text-align: center;
        padding: spacing.$spacing-2;
    }
}
</style>
