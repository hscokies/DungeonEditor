<script setup lang="ts">
import { UiButton, UiForm, UiInput, UiProperty } from '@/shared/ui';
import { router, Routes } from '@/app/router';
import { useLock, useProblemDetails } from '@/shared/hooks';
import { AccountApi } from '@/entities/account/api/account-api.ts';
import type { ProblemDetails } from '@/shared/api/types.ts';
import { computed } from 'vue';
import { validPassword, validUserName } from '@/features/auth/shared/validators.ts';
import { useI18n } from 'vue-i18n';

const { t } = useI18n();
const { locked, lock, release } = useLock();
const { model, error, applyProblemDetails } = useProblemDetails({
    userName: {
        value: '',
        error: '',
    },
    password: {
        value: '',
        error: '',
    },
});

const hasErrors = computed(() => model.userName.error.length > 0 || model.password.error.length > 0);

function validateUsername() {
    const value = model.userName.value;
    if (!value.length) {
        model.userName.error = t('Errors.Common.FieldRequired');
    } else if (validUserName(value)) {
        model.userName.error = '';
    } else {
        model.userName.error = t('Errors.Registration.InvalidUserName');
    }
}

function validatePassword() {
    const value = model.password.value;
    if (!value.length) {
        model.password.error = t('Errors.Common.FieldRequired');
    } else if (validPassword(value)) {
        model.password.error = '';
    } else {
        model.password.error = t('Errors.Registration.InvalidPasswordFormat');
    }
}

async function onSubmit() {
    validateUsername();
    validatePassword();

    if (hasErrors.value || locked.value) {
        return;
    }

    const request = { userName: model.userName.value, password: model.password.value };

    lock();
    try {
        await AccountApi.login(request);
        await router.replace(Routes.Upload);
    } catch (error) {
        applyProblemDetails(error as ProblemDetails);
    } finally {
        release();
    }
}
</script>

<template>
    <div :class="$cn()">
        <h1 :class="$cn('title')">
            {{ $t('Pages.Login.Labels.Title') }}
        </h1>
        <ui-form @submit.prevent="onSubmit" :error="error">
            <ui-property
                id="username"
                :label="$t('Common.Username')"
                direction="vertical"
                :error="model.userName.error"
            >
                <template #default="{ id, invalid }">
                    <ui-input :id="id" :invalid="invalid" v-model="model.userName.value" @blur="validateUsername" />
                </template>
            </ui-property>
            <ui-property
                id="password"
                :label="$t('Common.Password')"
                direction="vertical"
                :error="model.password.error"
            >
                <template #default="{ id, invalid }">
                    <ui-input
                        :id="id"
                        :invalid="invalid"
                        type="password"
                        v-model="model.password.value"
                        @blur="validatePassword"
                    />
                </template>
            </ui-property>
            <ui-button :label="$t('Common.SignIn')" :loading="locked" :disabled="locked" />
            <p :class="$cn('footer')">
                {{ $t('Pages.Login.Labels.DontHaveAccount') }}
                <router-link :to="Routes.Register">
                    {{ $t('Common.SignUp') }}
                </router-link>
            </p>
        </ui-form>
    </div>
</template>

<style scoped lang="scss">
@use 'src/shared/ui/spacing' as spacing;

.login {
    display: flex;
    flex-flow: column nowrap;
    align-items: center;
    gap: spacing.$spacing-5;
    width: 24rem;

    &__form {
        display: flex;
        flex-flow: column nowrap;
        gap: spacing.$spacing-2-5;
        width: 100%;
    }

    &__footer {
        text-align: center;
    }
}
</style>
