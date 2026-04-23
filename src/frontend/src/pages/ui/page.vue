<script setup lang="ts">
import { Coins, Files, LogOut as Logout, ScrollTextIcon as Scrolltext, Upload } from '@lucide/vue';
import { router, Routes } from '@/app/router';
import { useI18n } from 'vue-i18n';
import { AccountApi } from '@/entities/account/api/account-api.ts';
import { useAuth, useLock } from '@/shared/hooks';
import { onMounted } from 'vue';
import { UiSpinner } from '@/shared/ui';

const { t } = useI18n();
const { locked, lock, release } = useLock();
const { authorized, user, fetch } = useAuth();

const links = [
    {
        route: Routes.Upload,
        icon: Upload,
        label: t('Navbar.Upload'),
    },
    {
        route: Routes.Uploads,
        icon: Files,
        label: t('Navbar.Uploads'),
    },
    {
        route: Routes.Transactions,
        icon: Scrolltext,
        label: t('Navbar.Transactions'),
    },
];

async function handleLogout() {
    if (locked.value) {
        return;
    }

    lock();
    try {
        await router.replace({ name: Routes.Login });
        await AccountApi.logout();
    } finally {
        release();
    }
}

onMounted(() => {
    if (!authorized.value) {
        fetch();
    }
});
</script>

<template>
    <div :class="$cn()">
        <div :class="$cn('header')">
            <div :class="$cn('actions')">
                <span :class="$cn('logo')">
                    {{ $t('Common.ProjectName') }}
                </span>
                <nav :class="$cn('navigation')">
                    <router-link
                        v-for="link in links"
                        :key="link.route"
                        :class="$cn('link')"
                        :active-class="$cn('link', { active: true })[1]"
                        :to="link.route"
                    >
                        <component :is="link.icon" :size="12" />
                        {{ link.label }}
                    </router-link>
                </nav>
            </div>
            <div :class="$cn('actions')">
                <div :class="$cn('balance')">
                    {{ user?.balance }}
                    <ui-spinner v-if="!user" :size="12" :thickness="2" />
                    <coins :size="12" />
                </div>
                <a :class="$cn('link')" @click="handleLogout">
                    <logout :size="12" />
                    {{ $t('Common.SignOut') }}
                </a>
            </div>
        </div>
        <div :class="$cn('content')">
            <slot />
        </div>
    </div>
</template>

<style scoped lang="scss">
@use 'src/shared/ui/colors' as colors;
@use 'src/shared/ui/spacing' as spacing;
@use 'src/shared/ui/typography' as typography;
@use 'src/shared/ui/utils' as utils;
@use 'src/shared/ui/border-radius' as border-radius;
@use 'src/shared/ui/variables' as variables;

.page {
    display: flex;
    flex-flow: column nowrap;
    height: 100%;

    &__header {
        --ui-button-padding: #{spacing.$spacing-0-5};
        display: flex;
        flex-flow: row nowrap;
        padding: spacing.$spacing-2 spacing.$spacing-2-5;
        background: colors.$secondary-pane-0;
        justify-content: space-between;
    }

    &__content {
        height: 100%;
    }

    &__actions {
        display: inline-flex;
        flex-flow: row nowrap;
        align-items: center;
        gap: spacing.$spacing-7;
    }

    &__logo {
        color: colors.$cpt-peach;
        pointer-events: none;
        user-select: none;
    }

    &__navigation {
        display: inline-flex;
        flex-flow: row nowrap;
        gap: spacing.$spacing-2-5;

        @media (max-width: 544px) {
            display: none;
        }
    }

    &__link {
        display: inline-flex;
        align-items: center;
        gap: spacing.$spacing-1;
        font-size: typography.$font-size-sm;
        color: colors.$label-0;
        padding: spacing.$spacing-1;
        border-radius: border-radius.$border-radius-md;
        cursor: pointer;
        user-select: none;

        @include utils.transitions(background-color);

        &:hover {
            background-color: colors.$surface-element-2;
        }

        &--active {
            background-color: colors.$surface-element-1;
        }
    }

    &__balance {
        color: colors.$cpt-peach;
        display: inline-flex;
        align-items: center;
        gap: spacing.$spacing-1-5;
        border-radius: border-radius.$border-radius-md;
        user-select: none;
        pointer-events: none;
    }
}
</style>
