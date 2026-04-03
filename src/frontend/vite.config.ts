import { fileURLToPath, URL } from 'node:url';

import { defineConfig } from 'vite';
import vue from '@vitejs/plugin-vue';
import vueDevTools from 'vite-plugin-vue-devtools';

// https://vite.dev/config/
const isStorybook = process.env.npm_lifecycle_event === 'storybook';
export default defineConfig({
    plugins: [vue(), !isStorybook && vueDevTools()],
    resolve: {
        alias: {
            '@': fileURLToPath(new URL('./src', import.meta.url)),
            src: fileURLToPath(new URL('./src', import.meta.url)),
        },
    },
    server: {
        cors: false,
        port: 3000,
        proxy: {
            '/api/': {
                target: 'https://localhost:5001',
                changeOrigin: true,
                secure: false,
            },
        },
    },
});
