import preview from '#.storybook/preview';
import { UIButton } from '@/shared/ui';

const meta = preview.meta({
    title: 'Fields / ui-button',
    component: UIButton,
    render: args => ({
        components: { 'ui-button': UIButton },
        data: () => ({
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
