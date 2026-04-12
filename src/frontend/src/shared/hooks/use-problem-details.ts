import { reactive, ref } from 'vue';
import { i18n } from '@/shared/i18n';
import type { ProblemDetails } from '@/shared/api/types.ts';

interface FormField {
    value: string;
    error: string;
}

export function useProblemDetails<TModel extends Record<string, FormField>>(initialModel: TModel) {
    const error = ref<string>('');
    const model = reactive(initialModel) as TModel;

    function getField(name: keyof TModel): FormField {
        return model[name]!;
    }

    function clearErrors() {
        error.value = '';
        Object.keys(model).forEach(field => {
            getField(field).error = '';
        });
    }

    function applyProblemDetails(details: ProblemDetails) {
        error.value = details.detail;
        if (details.errors) {
            Object.entries(details.errors).forEach(([field, errors]) => {
                if (field in model) {
                    getField(field).error = errors.map(e => i18n.global.t(`Errors.${e.code}`)).join('\n');
                }
            });
        }
    }

    return {
        model,
        error,
        clearErrors,
        applyProblemDetails,
    };
}
