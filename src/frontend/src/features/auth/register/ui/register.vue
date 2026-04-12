<script setup lang="ts">
import { Routes } from '@/app/router';
import { UiButton } from '@/shared/ui';
import { AccountApi } from '@/entities/account/api/account-api.ts';
import Auth from '@/features/auth/ui/auth.vue';
</script>

<template>
    <auth :title="$t('Pages.Register.Labels.Title')" :handle-auth="AccountApi.login">
        <template #default="{ loading }">
            <ui-button :label="$t('Common.SignIn')" :loading="loading" :disabled="loading" />
        </template>
        <template #footer>
            {{ $t('Pages.Register.Labels.AlreadyRegistered') }}
            <router-link :to="Routes.Login">
                {{ $t('Common.SignIn') }}
            </router-link>
        </template>
    </auth>
</template>
