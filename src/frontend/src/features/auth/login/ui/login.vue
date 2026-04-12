<script setup lang="ts">
import Auth from '@/features/auth/ui/auth.vue';
import { AccountApi } from '@/entities/account/api/account-api.ts';
import { Routes } from '@/app/router';
import { UiButton } from '@/shared/ui';
</script>

<template>
    <auth :title="$t('Pages.Login.Labels.Title')" :handle-auth="AccountApi.login">
        <template #default="{ loading }">
            <ui-button :label="$t('Common.SignIn')" :loading="loading" :disabled="loading" />
        </template>
        <template #footer>
            {{ $t('Pages.Login.Labels.DontHaveAccount') }}
            <router-link :to="Routes.Register">
                {{ $t('Common.SignUp') }}
            </router-link>
        </template>
    </auth>
</template>
