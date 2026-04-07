import preview from '#.storybook/preview';
import { UiButton } from '@/shared/ui';

const meta = preview.meta({
    title: 'Fields / ui-button',
    component: UiButton,
    render: args => ({
        components: { 'ui-button': UiButton },
        setup: () => ({
            args,
            value: '',
        }),
        template: `
            <ui-button v-bind='args' v-model='value'>
            </ui-button>`,
    }),
});

export const Default = meta.story({
    args: {
        label: 'Button ',
        disabled: false,
        active: false,
    },
});
