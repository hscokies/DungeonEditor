import { createRouter, createWebHistory } from 'vue-router';
import { LoginPage, RegisterPage } from '@/pages/ui';
import { i18n } from '@/shared/i18n';

const DEFAULT_TITLE = 'Bloodborne dungeon editor';

export enum Routes {
    Upload = 'upload',
    Login = 'login',
    Register = 'register',
}

export const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: [
        {
            path: '/login',
            name: Routes.Login,
            component: LoginPage,
            meta: {
                title: i18n.global.t('Pages.Login.Title'),
            },
        },
        {
            path: '/register',
            name: Routes.Register,
            component: RegisterPage,
            meta: {
                title: i18n.global.t('Pages.Register.Title'),
            },
        },
        // {
        //   path: '/about',
        //   name: 'about',
        // route level code-splitting
        // this generates a separate chunk (About.[hash].js) for this route
        // which is lazy-loaded when the route is visited.
        // component: () => import('../views/AboutView.vue'),
        // },
    ],
});

router.beforeEach(to => {
    document.title = to.meta.title ?? DEFAULT_TITLE;
});
