import { createRouter, createWebHistory } from 'vue-router';
import {
    LoginPage,
    RegisterPage,
    SaveFilePage,
    TransactionsPage,
    UploadPage,
    UploadsPage,
    UsersPage,
} from '@/pages/ui';
import { i18n } from '@/shared/i18n';

export enum Routes {
    Login = 'login',
    Register = 'register',
    Upload = 'upload',
    Uploads = 'uploads',
    Transactions = 'transactions',
    Users = 'users',
    SaveFile = 'save-file',
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
        {
            path: '/upload',
            name: Routes.Upload,
            component: UploadPage,
            meta: {
                title: i18n.global.t('Pages.Upload.Title'),
            },
        },
        {
            path: '/uploads',
            name: Routes.Uploads,
            component: UploadsPage,
            meta: {
                title: i18n.global.t('Pages.Uploads.Title'),
            },
        },
        {
            path: '/transactions',
            name: Routes.Transactions,
            component: TransactionsPage,
            meta: {
                title: i18n.global.t('Pages.Transactions.Title'),
            },
        },
        {
            path: '/savefiles/:id',
            name: Routes.SaveFile,
            component: SaveFilePage,
            meta: {
                title: i18n.global.t('Pages.SaveFile.Title'),
            },
        },
        {
            path: '/users',
            name: Routes.Users,
            component: UsersPage,
            meta: {
                title: i18n.global.t('Pages.Users.Title'),
            },
        },
        {
            path: '/',
            redirect: {
                name: Routes.Uploads,
            },
        },
        {
            path: '/:pathMatch(.*)*',
            redirect: {
                name: Routes.Uploads,
            },
        },
    ],
});

router.beforeEach((to, _, next) => {
    document.title = to.meta.title ?? i18n.global.t('Common.ProjectName');
    next();
});
