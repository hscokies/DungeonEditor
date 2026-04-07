import preview from '#.storybook/preview';
import { UiSelect } from '@/shared/ui';

const meta = preview.meta({
    title: 'Fields / ui-select',
    component: UiSelect<number>,
    render: args => ({
        components: { 'ui-select': UiSelect },
        setup: () => ({
            args,
            value: undefined,
        }),
        template: `<ui-select v-bind='args' v-model='value' style="width: 250px" />`,
    }),
});

const sharedOptions = {
    optionValue: 'value',
    optionLabel: 'label',
    placeholder: 'This is placeholder',
};

export const Default = meta.story({
    args: {
        ...sharedOptions,
        options: [
            {
                label: 'New York',
                value: 0,
            },
            {
                label: 'Tokyo',
                value: 1,
            },
            {
                label: 'Paris',
                value: 2,
            },
            {
                label: 'London',
                value: 3,
            },
            {
                label: 'Sydney',
                value: 4,
            },
            {
                label: 'Dubai',
                value: 5,
            },
            {
                label: 'Toronto',
                value: 6,
            },
        ],
    },
});

export const Grouped = meta.story({
    args: {
        ...sharedOptions,
        options: [
            {
                label: 'USA',
                children: [
                    { label: 'New York', value: 0 },
                    { label: 'Los Angeles', value: 1 },
                ],
            },
            {
                label: 'Japan',
                children: [
                    {
                        label: 'Tokyo',
                        value: 2,
                    },
                    { label: 'Osaka', value: 3 },
                ],
            },
            {
                label: 'France',
                children: [{ label: 'Paris', value: 4 }],
            },
            {
                label: 'UK',
                children: [{ label: 'London', value: 5 }],
            },
            {
                label: 'Australia',
                children: [{ label: 'Sydney', value: 6 }],
            },
        ],
        optionGroupLabel: 'label',
        optionGroupChildren: 'children',
    },
});
