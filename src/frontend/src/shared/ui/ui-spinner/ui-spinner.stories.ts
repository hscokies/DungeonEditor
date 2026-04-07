import preview from '#.storybook/preview';
import { UiSpinner } from '@/shared/ui';

const meta = preview.meta({
    title: 'Controls / ui-spinner',
    component: UiSpinner,
    render: args => ({
        components: { 'ui-spinner': UiSpinner },
        setup: () => ({
            args,
        }),
        template: `<ui-spinner v-bind='args' />`,
    }),
});

export const Default = meta.story({
    args: {},
});
