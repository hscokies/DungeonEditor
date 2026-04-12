import { ref } from 'vue';

export function useLock() {
    const locked = ref(false);

    function lock() {
        locked.value = true;
    }

    function release() {
        locked.value = false;
    }

    return {
        locked,
        lock,
        release,
    };
}
