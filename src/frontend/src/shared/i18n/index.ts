import { createI18n } from 'vue-i18n';
import { getFileNameWithoutExtension } from '@/shared/utils/file.ts';

function loadLocalizationFiles(files: Record<string, unknown>) {
    return Object.fromEntries(
        Object.entries(files).map(([path, contents]) => [getFileNameWithoutExtension(path), contents])
    );
}

export const i18n = createI18n({
    locale: 'en',
    fallbackLocale: 'en',
    messages: loadLocalizationFiles(import.meta.glob('@/shared/i18n/*.json', { eager: true, import: 'default' })),
});
