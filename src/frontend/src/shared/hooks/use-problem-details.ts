import { reactive } from 'vue';
import { i18n } from '@/shared/i18n';
import type { ProblemDetails } from '@/shared/api/types.ts';

interface FormField {
    value: string;
    error: string;
}

export function useProblemDetails<TModel extends Record<string, FormField>>(initialModel: TModel) {
    const model = reactive(initialModel) as TModel;

    function getField(name: keyof TModel): FormField {
        return model[name]!;
    }

    function clearErrors() {
        Object.keys(model).forEach(field => {
            getField(field).error = '';
        });
    }

    function applyProblemDetails(details: ProblemDetails) {
        if (!details.errors) {
            return;
        }

        for (const [field, errors] of Object.entries(details.errors)) {
            if (field in model) {
                getField(field).error = errors.map(e => i18n.global.t(`Errors.${e.code}`)).join('\n');
            }
        }
    }

    return {
        model,
        clearErrors,
        applyProblemDetails,
    };
}
