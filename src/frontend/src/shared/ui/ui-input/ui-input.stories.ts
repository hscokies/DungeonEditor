import { UIInput } from '@/shared/ui';
import type { Meta, StoryObj } from '@storybook/vue3';

const meta: Meta<typeof UIInput> = {
    title: 'Fields / ui-input',
    component: UIInput,
    render: args => ({
        components: { 'ui-input': UIInput },
        data: () => ({
            args,
            value: '',
        }),
        template: `
            <ui-input v-bind='args' v-model='value'>
                <template #prefix>Prefix</template>
                <template #suffix>Suffix</template>
            </ui-input>`,
    }),
};

export default meta;

export const Default: StoryObj = {
    args: {
        type: 'text',
        placeholder: 'This is placeholder',
        disabled: false,
        invalid: false,
        readonly: false,
    },
};
