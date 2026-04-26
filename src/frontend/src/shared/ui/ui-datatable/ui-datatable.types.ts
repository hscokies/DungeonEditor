import type { LazyOptions } from '@/shared/types/lazy-loading.ts';

export interface DatatableColumn {
    field?: string;
    header: string;
    slots?: Record<string, unknown>;
}

export interface PropTypes {
    keyField: string;
    maxVisibleRows?: number;
    rowHeight: number;
    filter?: boolean;
    filterPlaceholder?: string;
    loading?: boolean;
    rows: Record<string, unknown>[];
}

export interface Emits {
    (e: 'load-more', options: LazyOptions): void;
    (e: 'load', options: LazyOptions): void;
}

export type RegisterColumn = (column: DatatableColumn) => void;

export enum Injections {
    RegisterColumn = 'registerColumn',
}
