import preview from '#.storybook/preview';
import { UIInput, UIProperty } from '@/shared/ui';
import { v4 as uuidv4 } from 'uuid';

const meta = preview.meta({
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
});

export const Default = meta.story({
    args: {
        id: uuidv4(),
        label: 'This is label',
        direction: 'horizontal',
        error: undefined,
    },
    argTypes: {
        direction: {
            control: {
                type: 'select',
            },
            options: ['horizontal', 'vertical'],
        },
    },
});
