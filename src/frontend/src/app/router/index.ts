import { createRouter, createWebHistory } from 'vue-router';
import { LoginPage } from '@/pages';

const DEFAULT_TITLE = '%AppTitle%';

export const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: [
        {
            path: '/login',
            name: 'login',
            component: LoginPage,
            meta: {
                title: 'Sign in',
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
