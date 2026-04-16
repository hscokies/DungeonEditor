import { reactive, ref } from 'vue';
import { i18n } from '@/shared/i18n';
import type { ProblemDetails } from '@/shared/api/types.ts';

interface FormField {
    value: unknown;
    error: string;
}

export function useProblemDetails<TModel extends { [K in keyof TModel]: FormField }>(initialModel: TModel) {
    const error = ref<string>('');
    const model = reactive(initialModel) as TModel;

    function getField(name: keyof TModel): FormField {
        return model[name];
    }

    function clearErrors() {
        error.value = '';
        Object.keys(model).forEach(field => {
            getField(field as keyof TModel).error = '';
        });
    }

    function applyProblemDetails(details: ProblemDetails) {
        error.value = i18n.global.t(`Errors.${details.title}`);
        if (details.errors) {
            Object.entries(details.errors).forEach(([field, errors]) => {
                if (field in model) {
                    getField(field as keyof TModel).error = errors
                        .map(e => i18n.global.t(`Errors.${e.code}`))
                        .join('\n');
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
