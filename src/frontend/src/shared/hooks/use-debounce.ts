import { onUnmounted } from 'vue';
import debounce from 'lodash-es/debounce';
import type { DebounceSettings } from 'lodash-es';

export function useDebounce(fn: (...args: any[]) => void, delay = 300, settings?: DebounceSettings) {
    const debounced = debounce(fn, delay, settings);

    onUnmounted(() => {
        debounced.cancel();
    });

    return debounced;
}
