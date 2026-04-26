<script setup lang="ts">
import { IconSize } from '@/shared/types/icon-size.ts';
import { Files, LogOut as Logout, ScrollTextIcon as Scrolltext, Upload, Users } from '@lucide/vue';
import { computed, onMounted, ref } from 'vue';
import { router, Routes } from '@/app/router';
import { AccountApi } from '@/entities/account/api/account-api.ts';
import { useAuth } from '@/shared/hooks';
import { useI18n } from 'vue-i18n';
import BurgerMenu from '@/widgets/menu/ui/burger-menu.vue';
import { Balance } from '@/widgets';

const { t } = useI18n();
const { authorized, user, fetch } = useAuth();

const burgerMenuVisible = ref(false);

const links = [
    {
        route: { name: Routes.Upload },
        icon: Upload,
        label: t('Navbar.Upload'),
        admin: false,
    },
    {
        route: { name: Routes.Uploads },
        icon: Files,
        label: t('Navbar.Uploads'),
        admin: false,
    },
    {
        route: { name: Routes.Transactions },
        icon: Scrolltext,
        label: t('Navbar.Transactions'),
        admin: false,
    },
    {
        route: { name: Routes.Users },
        icon: Users,
        label: t('Navbar.Users'),
        admin: true,
    },
];

const visibleLinks = computed(() => links.filter(x => (user.value && user.value.admin) || !x.admin));

async function handleLogout() {
    await router.replace({ name: Routes.Login });
    await AccountApi.logout();
}

function toggle() {
    burgerMenuVisible.value = !burgerMenuVisible.value;
}

onMounted(() => {
    if (!authorized.value) {
        fetch();
    }
});
</script>

<template>
    <div :class="$cn()">
        <div :class="$cn('section')">
            <span :class="$cn('logo')">
                {{ $t('Common.ProjectName') }}
            </span>
            <nav :class="$cn('navigation')">
                <router-link
                    v-for="link in visibleLinks"
                    :key="link.route.name"
                    :class="$cn('link')"
                    :active-class="$cn('link', { active: true })[1]"
                    :to="link.route"
                >
                    <component :is="link.icon" :size="IconSize.xss" />
                    {{ link.label }}
                </router-link>
            </nav>
        </div>

        <div :class="$cn('section')">
            <balance :class="$cn('balance')" :balance="user?.balance" />
            <a :class="[$cn('link'), $cn('signout')]" @click="handleLogout">
                <logout :size="IconSize.xss" />
                {{ $t('Common.SignOut') }}
            </a>
            <burger-menu :class="$cn('burger')" :links="visibleLinks" :balance="user?.balance" @logout="handleLogout" />
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
@use 'src/shared/ui/z-index' as z-index;

$accent: colors.$cpt-peach;

.menu {
    --ui-button-padding: #{spacing.$spacing-0-5};
    display: flex;
    flex-flow: row nowrap;
    padding: spacing.$spacing-2 spacing.$spacing-2-5;
    background: colors.$secondary-pane-0;
    justify-content: space-between;

    &__section {
        display: inline-flex;
        flex-flow: row nowrap;
        align-items: center;
        gap: spacing.$spacing-7;
    }

    &__logo {
        color: $accent;
        pointer-events: none;
        user-select: none;
    }

    &__navigation {
        display: inline-flex;
        flex-flow: row nowrap;
        gap: spacing.$spacing-2-5;
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

    &__burger {
        display: none;
    }

    @media (max-width: 768px) {
        &__navigation,
        &__balance,
        &__signout {
            display: none;
        }

        &__burger {
            display: block;
        }
    }
}
</style>
