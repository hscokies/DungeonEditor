<script setup lang="ts">
import { LogOut as Logout, Menu as MenuIcon, X } from '@lucide/vue';
import { UiIconButton } from '@/shared/ui';
import { IconSize } from '@/shared/types/icon-size.ts';
import { ref } from 'vue';
import type { Emits, PropTypes } from '@/widgets/menu/model/types.ts';
import { Balance as BalanceWidget } from '@/widgets';

const { links, balance } = defineProps<PropTypes>();
const emit = defineEmits<Emits>();

const active = ref(false);

function show() {
    active.value = true;
}

function hide() {
    active.value = false;
}
</script>

<template>
    <div :class="$cn()">
        <ui-icon-button :label="$t('Common.OpenMenu')" @click="show">
            <menu-icon :size="IconSize.md" />
        </ui-icon-button>
        <div :class="$cn('overlay', { active })">
            <div :class="$cn('header')">
                <balance-widget :class="$cn('balance')" label :balance="balance" />
                <ui-icon-button label="$t('Common.CloseMenu')" @click="hide">
                    <x :size="IconSize.lg" />
                </ui-icon-button>
            </div>
            <nav :class="$cn('navigation')">
                <router-link
                    v-for="link in links"
                    :key="link.route"
                    :class="$cn('link')"
                    :active-class="$cn('link', { active: true })[1]"
                    :to="link.route"
                >
                    <component :is="link.icon" :size="IconSize.lg" />
                    {{ link.label }}
                </router-link>
            </nav>
            <div :class="$cn('footer')">
                <a :class="[$cn('link'), $cn('signout')]" @click="emit('logout')">
                    <logout :size="IconSize.lg" />
                    {{ $t('Common.SignOut') }}
                </a>
            </div>
        </div>
    </div>
</template>

<style scoped lang="scss">
@use 'src/shared/ui/utils' as utils;
@use 'src/shared/ui/z-index' as z-index;
@use 'src/shared/ui/spacing' as spacing;
@use 'src/shared/ui/colors' as colors;
@use 'src/shared/ui/typography' as typography;

$accent: colors.$cpt-peach;

.burger-menu {
    &__overlay {
        display: flex;
        flex-flow: column nowrap;
        font-size: typography.$font-size-xl;

        position: fixed;
        top: 0;
        left: 200%;

        width: 100%;
        height: 100%;

        background: colors.$secondary-pane-0;
        z-index: z-index.$burger-menu;

        @include utils.transitions(left);

        &--active {
            left: 0;
        }
    }

    &__header {
        display: flex;
        justify-content: space-between;
        padding: spacing.$spacing-2;
    }

    &__navigation {
        margin-top: spacing.$spacing-4;
        display: flex;
        flex-flow: column nowrap;
        gap: spacing.$spacing-1-5;
    }

    &__footer {
        margin-top: auto;
    }

    &__link {
        cursor: pointer;
        display: inline-flex;
        width: 100%;
        align-items: center;
        gap: spacing.$spacing-2;
        padding: spacing.$spacing-2;
        color: colors.$body-copy;

        &:hover {
            background: colors.$surface-element-2;
        }

        &--active {
            color: $accent;
        }
    }

    &__balance {
        align-self: center;
    }
}
</style>
