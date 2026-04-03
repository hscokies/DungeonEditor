import { router } from '@/app/router';
import { classNamePlugin } from '@/shared/plugins';
import { createApp } from 'vue';
import App from './ui/App.vue';
import { i18n } from '@/shared/i18n';
import VueFeather from 'vue-feather';
import '@/app/ui/global.scss';

const app = createApp(App);
app.component('feather', VueFeather);

app.use(i18n).use(router).use(classNamePlugin);

app.mount('#app');
