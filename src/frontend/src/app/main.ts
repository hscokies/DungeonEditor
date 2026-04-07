import { router } from '@/app/router';
import { classNamePlugin } from '@/shared/plugins';
import { createApp } from 'vue';
import App from './ui/App.vue';
import { i18n } from '@/shared/i18n';
import '@/app/ui/global.scss';

createApp(App).use(i18n).use(router).use(classNamePlugin).mount('#app');
