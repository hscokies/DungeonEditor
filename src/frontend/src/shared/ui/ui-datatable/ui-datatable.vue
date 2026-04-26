<script setup lang="ts">
import 'vue-virtual-scroller/dist/vue-virtual-scroller.css';
import { computed, onMounted, provide, ref } from 'vue';
import { type DatatableColumn, type Emits, Injections, type PropTypes } from './ui-datatable.types.ts';
import { RecycleScroller } from 'vue-virtual-scroller';
import { UiInput, UiSpinner } from '@/shared/ui';
import { useDebounce } from '@/shared/hooks';
import { IconSize } from '@/shared/types/icon-size.ts';

const emit = defineEmits<Emits>();
const {
    keyField,
    rowHeight,
    maxVisibleRows = 30,
    rows,
    filter = false,
    filterPlaceholder,
    loading = false,
} = defineProps<PropTypes>();

const scrollerHeight = computed(() => maxVisibleRows * rowHeight);
const datatableStyles = computed(() => ({
    '--ui-datatable-cell-height': `${rowHeight}px`,
    '--ui-datatable-max-height': `${scrollerHeight.value}px`,
    '--ui-datatable-columns': columns.value.length,
}));

const columns = ref<DatatableColumn[]>([]);
const filterValue = ref<string>();

let offsetCounter = 0;

function loadMore() {
    offsetCounter++;

    emit('load-more', {
        filter: filterValue.value,
        limit: maxVisibleRows,
        offset: offsetCounter * maxVisibleRows,
    });
}

function loadData() {
    emit('load', {
        filter: filterValue.value,
        limit: maxVisibleRows,
        offset: 0,
    });
}

const onFilterChange = useDebounce((query: string) => {
    filterValue.value = query;
    loadData();
}, 350);

function getRenderKey(...args: unknown[]) {
    return args.join('_');
}

onMounted(() => {
    if (!rows.length) {
        loadData();
    }
});

provide(Injections.RegisterColumn, (column: DatatableColumn) => {
    if (!columns.value.some(x => x.header === column.header)) {
        columns.value.push(column);
    }
});
</script>

<template>
    <div :class="$cn()" :style="datatableStyles">
        <slot />
        <ui-input v-if="filter" :placeholder="filterPlaceholder" @input="onFilterChange($event.target.value)" />
        <div :class="$cn('content')">
            <recycle-scroller
                ref="scroller"
                :item-size="rowHeight"
                :key-field="keyField"
                :items="rows"
                :class="$cn('scroller')"
                @scrollend="loadMore"
            >
                <template #before>
                    <div :class="[$cn('row'), $cn('header-row')]">
                        <div
                            v-for="column in columns"
                            :key="getRenderKey(column.header, column.field)"
                            :class="$cn('cell')"
                        >
                            <div :class="$cn('cell-content')">
                                <span :class="$cn('column-title')">
                                    {{ column.header }}
                                </span>
                            </div>
                        </div>
                    </div>
                </template>
                <template #default="{ item: row }">
                    <div :class="$cn('row')" :key="getRenderKey(row[keyField])">
                        <div v-for="column in columns" :class="$cn('cell')">
                            <div :class="$cn('cell-content')">
                                <span
                                    v-if="column.field && row[column.field]"
                                    :class="$cn('cell-text')"
                                    :key="getRenderKey(row[keyField], column.header, row[column.field])"
                                >
                                    {{ row[column.field] }}
                                </span>
                                <component :is="column.slots?.default" :row="row" />
                            </div>
                        </div>
                    </div>
                </template>
            </recycle-scroller>
            <div :class="$cn('footer')">
                <ui-spinner v-show="loading" :size="IconSize.xl" />
            </div>
        </div>
    </div>
</template>

<style scoped lang="scss">
@use 'src/shared/ui/spacing' as spacing;
@use 'src/shared/ui/colors' as colors;
@use 'src/shared/ui/typography' as typography;
@use 'src/shared/ui/z-index' as z-index;

.ui-datatable {
    $root: &;

    display: flex;
    flex-grow: 0;
    flex-flow: column nowrap;
    align-items: start;
    gap: var(--ui-datatable-gap, spacing.$spacing-4);
    min-width: 100%;
    overflow-x: auto;

    &__scroller {
        max-height: var(--ui-datatable-max-height);
    }

    &__content {
        width: 100%;
        min-width: var(--ui-datatable-width);
    }

    &__row {
        display: grid;
        grid-template-columns: repeat(var(--ui-datatable-columns), 1fr);
        background-color: var(--ui-datatable-background-color, colors.$background-pane);
    }

    &__cell-content {
        display: flex;
        align-items: center;
        gap: spacing.$spacing-2;
        height: 100%;
    }

    &__cell {
        padding: var(--ui-datatable-cell-padding, spacing.$spacing-1-5);
        width: 100%;
        height: var(--ui-datatable-cell-height);
        border-color: var(--ui-datatable-cell-border-color, colors.$surface-element-2);
        border-style: solid;
        border-width: 0 0 1px 0;
        white-space: nowrap;
        min-width: 0;
        overflow: hidden;
    }

    &__header-row {
        position: sticky;
        top: 0;
        z-index: z-index.$datatable-header;

        #{$root}__column-title {
            font-weight: typography.$font-weight-semibold;
        }
    }

    &__footer {
        display: grid;
        place-items: center;
        width: 100%;
        height: 5em;
        margin-top: spacing.$spacing-2;
    }
}
</style>
