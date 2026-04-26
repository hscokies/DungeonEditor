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
    rows: Record<string, unknown>[];
}

export interface Emits {
    (e: 'load-more', options: LazyOptions): void;
    (e: 'load', options: LazyOptions): void;
}

export interface LazyOptions {
    filter?: string;
    limit: number;
    offset: number;
}

export type RegisterColumn = (column: DatatableColumn) => void;

export enum Injections {
    RegisterColumn = 'registerColumn',
}
