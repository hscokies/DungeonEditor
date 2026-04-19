export interface DatatableColumn {
    field?: string;
    header: string;
    slots?: Record<string, unknown>;
}

export interface PropTypes {
    keyField: string;
    maxVisibleRows?: number;
    rowHeight: number;
    rows: Record<string, unknown>[];
}

export type Emits = (e: 'load-more', options: LazyOptions) => void;

export interface LazyOptions {
    limit: number;
    offset: number;
}

export type RegisterColumn = (column: DatatableColumn) => void;

export enum Injections {
    RegisterColumn = 'registerColumn',
}
