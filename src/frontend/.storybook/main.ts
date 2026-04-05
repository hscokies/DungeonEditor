import { defineMain } from '@storybook/vue3-vite/node';
export default defineMain({
    stories: ['../src/**/*.stories.@(js|jsx|mjs|ts|tsx)'],
    addons: [],
    framework: '@storybook/vue3-vite',
});
