export interface PropTypes {
    id?: string;
    placeholder?: string;
    tabIndex?: number;

    options: Record<string, unknown>[];
    optionLabel: string;
    optionValue: string;
    optionGroupLabel?: string;
    optionGroupChildren?: string;

    disabled?: boolean;
    invalid?: boolean;
    loading?: boolean;

    filter?: boolean;
    filterPlaceholder?: string;

    itemHeight?: number;
    maxVisibleItems?: number;
}

export type Emits<T> = (e: 'change', value: T) => void;

export interface VisibleItem {
    header: boolean;
    renderKey: string;
    label: string;
    selected: boolean;
    onSelect: () => void;
    onHover: () => void;
}
