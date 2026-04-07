import { ref, computed } from 'vue';

type RawOption = Record<string, unknown>;

export enum SelectItemType {
    Group,
    Option,
}

export interface Option<T> {
    value: T;
    label: string;
}

export interface Group<T> {
    label: string;
    children: Option<T>[];
}

interface GroupItem {
    type: SelectItemType.Group;
    renderKey: string;
    label: string;
}

export interface OptionItem<T> extends Option<T> {
    type: SelectItemType.Option;
    renderKey: string;
}

export type SelectItem<T> = OptionItem<T> | GroupItem;

export function useSelectItems<T>(
    rawOptions: RawOption[],
    optionLabel: string,
    optionValue: string,
    optionGroupLabel?: string,
    optionGroupChildren?: string
) {
    const grouped = optionGroupLabel !== undefined && optionGroupChildren !== undefined;

    function isOption<T>(item: Option<T> | Group<T> | SelectItem<T>): item is Option<T> {
        const optionValueKey: keyof Option<T> = 'value';
        return optionValueKey in item;
    }

    function getRenderKey(item: Option<T> | Group<T>) {
        if (isOption(item)) {
            return `${item.label}_${item.value}`;
        } else {
            return item.label;
        }
    }

    const filterValue = ref<string>('');

    function getOption(option: RawOption): Option<T> {
        return {
            label: option[optionLabel] as string,
            value: option[optionValue] as T,
        };
    }

    const groups = computed(() =>
        rawOptions.map(item => {
            if (!grouped) {
                return getOption(item);
            }

            const groupChildren = item[optionGroupChildren] as RawOption[];
            if (!groupChildren) {
                return getOption(item);
            }

            return {
                label: item[optionGroupLabel] as string,
                children: groupChildren.map((c: any) => getOption(c)),
            } as Group<T>;
        })
    );

    const flatGroupItems = computed(() => {
        const groupItems = groups.value;

        const orphans: OptionItem<T>[] = [];
        const flatItems: SelectItem<T>[] = [];

        for (const item of groupItems) {
            if (isOption(item)) {
                orphans.push({
                    type: SelectItemType.Option,
                    renderKey: getRenderKey(item),
                    ...item,
                });
                continue;
            }

            if (!item.children.length) {
                continue;
            }

            flatItems.push({ type: SelectItemType.Group, renderKey: getRenderKey(item), label: item.label });

            item.children.forEach(child => {
                flatItems.push({
                    type: SelectItemType.Option,
                    renderKey: getRenderKey(child),
                    label: child.label,
                    value: child.value,
                });
            });
        }

        return flatItems.concat(orphans);
    });

    const items = computed(() => {
        if (!rawOptions.length) {
            return [];
        }

        return grouped
            ? flatGroupItems.value
            : rawOptions.map(o => {
                  const option = getOption(o);

                  return {
                      ...option,
                      type: SelectItemType.Option,
                      renderKey: getRenderKey(option),
                  };
              });
    });

    const optionItems = computed(() => items.value.filter(item => item.type === SelectItemType.Option));

    const filteredItems = computed(() => {
        if (!filterValue.value?.length) {
            return items.value;
        }

        const normalizedFilter = filterValue.value.toLocaleUpperCase();

        return optionItems.value.filter(item => {
            return item.label.toLocaleUpperCase().includes(normalizedFilter);
        });
    });

    return {
        filterValue,
        optionItems,
        filteredItems,
    };
}
