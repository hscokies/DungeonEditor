import { type Preview, setup } from '@storybook/vue3-vite';
import { classNamePlugin } from '@/shared/plugins/class-name.ts';
import '@/app/ui/global.scss';

setup(app => {
    app.use(classNamePlugin);
});

const preview: Preview = {
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
};

export default preview;
