import { setup, definePreview } from '@storybook/vue3-vite';
import { classNamePlugin } from '@/shared/plugins/class-name.ts';
import '@/app/ui/global.scss';
import { i18n } from '@/shared/i18n';

setup(app => {
    app.use(classNamePlugin).use(i18n);
});

export default definePreview({
    parameters: {
        controls: {
            matchers: {
                color: /(background|color)$/i,
                date: /Date$/i,
            },
        },
        backgrounds: {
            options: {
                dark: {
                    name: 'dark',
                    value: '#1e1e2e',
                },
            },
        },
    },

    initialGlobals: {
        backgrounds: { value: 'dark' },
    },

    addons: [],
});
