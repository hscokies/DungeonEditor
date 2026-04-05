import preview from '#.storybook/preview';
import { UIInput } from '@/shared/ui';

const meta = preview.meta({
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
});

export const Default = meta.story({
    args: {
        type: 'text',
        placeholder: 'This is placeholder',
        disabled: false,
        invalid: false,
        readonly: false,
    },
});
