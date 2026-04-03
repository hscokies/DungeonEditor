import { UIButton } from '@/shared/ui';
import type { Meta, StoryObj } from '@storybook/vue3';

const meta: Meta<typeof UIButton> = {
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
};

export default meta;

export const Default: StoryObj = {
    args: {
        label: 'Button ',
        disabled: false,
        active: false,
    },
};
