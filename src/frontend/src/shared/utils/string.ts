import { i18n } from '@/shared/i18n';

export function toKebabCase(value: string) {
    return value
        .replaceAll(/([a-z])([A-Z])/g, '$1-$2')
        .replaceAll(/[\s_]+/g, '-')
        .toLowerCase();
}

export function formatSize(size: number) {
    const units = ['Byte', 'Kilobyte', 'Megabyte'];
    let index = 0;

    while (size >= 1024) {
        size /= 1024;
        index++;
    }

    const unit = i18n.global.t(`Common.Units.${units[index]}.Short`);
    return `${size.toFixed(2)}${unit}`;
}
