import { ref } from 'vue';

export function useCustomFocus() {
    const focused = ref(false);

    function onFocus() {
        focused.value = true;
    }

    function onBlur() {
        focused.value = false;
    }

    return { focused, onFocus, onBlur };
}
