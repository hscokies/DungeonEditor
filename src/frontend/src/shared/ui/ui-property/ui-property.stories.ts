import { UIInput, UIProperty } from '@/shared/ui';
import type { Meta, StoryObj } from '@storybook/vue3';
import { v4 as uuidv4 } from 'uuid';

const meta: Meta<typeof UIProperty> = {
    title: 'Fields / ui-property',
    component: UIProperty,
    render: args => ({
        components: { 'ui-property': UIProperty, 'ui-input': UIInput },
        data: () => ({
            args,
            value: '',
        }),
        template: `
            <ui-property id="ui-property__storybook" v-bind='args'>
                <template #default="{ id, invalid }">
                  <ui-input v-model='value' :id="id" :invalid="invalid" placeholder="This is placeholder"/>
                </template>
            </ui-property>`,
    }),
};

export default meta;

export const Default: StoryObj = {
    args: {
        id: uuidv4(),
        label: 'This is label',
        direction: 'horizontal',
        error: null,
    },
    argTypes: {
        direction: {
            control: {
                type: 'select',
            },
            options: ['horizontal', 'vertical'],
        },
    },
};
