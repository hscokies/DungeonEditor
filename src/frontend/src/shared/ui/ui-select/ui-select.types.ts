import type { LazyOptions } from '@/shared/types/lazy-loading.ts';
import type { PositioningStrategy } from '@popperjs/core';

export interface PropTypes {
    id?: string;
    placeholder?: string;
    tabIndex?: number;

    options: Record<string, unknown>[];
    optionLabel?: string;
    optionValue?: string;
    optionGroupLabel?: string;
    optionGroupChildren?: string;

    disabled?: boolean;
    invalid?: boolean;
    loading?: boolean;

    filter?: boolean;
    filterPlaceholder?: string;

    itemHeight?: number;
    maxVisibleItems?: number;

    batchSize?: number;

    positioningStrategy?: PositioningStrategy;
}

export interface Emits<T> {
    (e: 'change', value: T): void;
    (e: 'load-more', options: LazyOptions): void;
    (e: 'load', options: LazyOptions): void;
}

export interface SelectedItem {
    label: string;
    renderKey: string;
}
